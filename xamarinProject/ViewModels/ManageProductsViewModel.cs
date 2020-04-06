namespace xamarinProject.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using Xamarin.Forms;
    using xamarinProject.Common.Models;

    public class ManageProductsViewModel : BaseViewModel
    {
        #region Atributes
        private bool isRunning;
        private bool isEnable;
        private ApiService apiService;
        private ImageSource imageSource;
        private MediaFile file;
        private bool addAction;
        private ProductItemViewModel product;
        #endregion

        #region Properties
        public bool AddAction
        {
            get { return this.addAction; }
            set { this.SetValue(ref this.addAction, value); }
        }

        public string Description { get; set; }

        public ProductItemViewModel Product
        {
            get { return this.product; }
            set { this.SetValue(ref this.product, value); }
        }

        public string Price { get; set; }

        public string Remarks { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set { this.SetValue(ref this.isEnable, value); }
        }
        #endregion

        #region Constructors
        public ManageProductsViewModel()
        {
            this.IsEnable = true;
            this.apiService = new ApiService();
            this.imageSource = "noProduct";
            this.AddAction = true;
        }

        public ManageProductsViewModel(ProductItemViewModel productItem)
        {
            this.IsEnable = true;
            this.apiService = new ApiService();
            this.imageSource = productItem.ImageFullPath;
            this.Description = productItem.Description;
            this.Remarks = productItem.Remarks;
            this.Price = productItem.Price.ToString();
            this.AddAction = false;
            this.Product = productItem;
        }
        #endregion

        #region Commands

        public ICommand SaveProductCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.NewPicture,
                Languages.FromGallery);

            if(source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if(source== Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "testPhoto.jpg",
                        PhotoSize = PhotoSize.Small,
                    });
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if(this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }

        private async void Save()
        {
            try
            {
                if (string.IsNullOrEmpty(Description))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.DescriptionError,
                        Languages.Accept);
                    return;
                }

                if (string.IsNullOrEmpty(Price))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PriceError,
                        Languages.Accept);
                    return;
                }

                decimal price = -1;
                bool isDecimal = decimal.TryParse(Price, out price);

                if (!isDecimal || price < 0)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        Languages.PriceError,
                        Languages.Accept);
                    return;
                }

                this.IsRunning = true;
                this.IsEnable = false;

               
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnable = true;

                    await Application.Current.MainPage.DisplayAlert(
                        Languages.Error,
                        connection.Message,
                        Languages.Accept);
                    return;
                }

                var url = Application.Current.Resources["UrlApi"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlProductsController"].ToString();

                byte[] imageArray = null;
                // IFormFile picture = null;

                if (this.file != null)
                {
                    imageArray = FileHelper.ReadFully(file.GetStream());
                    /* var image = new MemoryStream(imageArray);
                     picture = new FormFile(
                         image,
                         0,
                         image.Length,
                         "myPicture",
                         "picture"+DateTime.Now.ToUniversalTime().ToString());*/
                }

                var viewModel = ProductsViewModel.GetInstance();

                Response response = null;

                if (AddAction)
                {
                    var myProduct = new Product
                    {
                        Description = this.Description,
                        Price = price,
                        Remarks = this.Remarks,
                        ImageArray = imageArray,
                        FromWeb = false,
                    };

                    response = await this.apiService.Post(url, prefix, controller, myProduct);

                }
                else
                {
                   var myProduct = new Product
                    {
                        ProductId = this.Product.ProductId,
                        Description = this.Description,
                        Price = price,
                        Remarks = this.Remarks,
                        ImageArray = imageArray,
                        FromWeb = false,
                        ImagePath = this.Product.ImagePath,
                    };
                   viewModel.Products.Remove(this.Product);

                   response = await this.apiService.Put(url, prefix, controller, myProduct, this.Product.ProductId);
                }

                if (!response.IsSuccess)
                {
                    this.IsRunning = false;
                    this.IsEnable = true;

                    await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                    return;
                }

                var newProduct = (Product)response.Result;


                var productItem = new ProductItemViewModel
                {
                    Description = newProduct.Description,
                    FromWeb = newProduct.FromWeb,
                    ImageArray = newProduct.ImageArray,
                    ImagePath = newProduct.ImagePath,
                    IsAvailable = newProduct.IsAvailable,
                    Price = newProduct.Price,
                    ProductId = newProduct.ProductId,
                    PublishOn = newProduct.PublishOn,
                    Remarks = newProduct.Remarks,
                };

                viewModel.Products.Add(productItem);

                viewModel.Products = new ObservableCollection<ProductItemViewModel>(viewModel.Products.OrderBy(p => p.Description));

                this.IsRunning = false;
                this.IsEnable = true;
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error, e.Message, Languages.Accept);

            }
            finally
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }

        }
        #endregion

    }
}
