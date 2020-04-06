namespace xamarinProject.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;
    using xamarinProject.Helpers;

    public class ProductsViewModel : BaseViewModel
    {
        private ObservableCollection<ProductItemViewModel> products;

        private ApiService apiService;

    /*    private bool isRefreshing;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
*/
        public ObservableCollection<ProductItemViewModel> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }
/*
        public ICommand RefreshCommand
        {
             get
              {
                  return new RelayCommand(LoadProducts);
              }
        }
*/
        private ProductsViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            LoadProducts();

        }

        #region Singleton

        private static ProductsViewModel instance;

        public static ProductsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new ProductsViewModel(); 
            }

            return instance;
        }

        #endregion

        public async Task LoadProducts()
        {
            var connection = await apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                //this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }
           // this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            //await Task.Delay(1000);  //this task delay has fixed my issue. 

            var response = await this.apiService.GetList<Product>(url, prefix, controller);

            if (!response.IsSuccess)
            {
            //    this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Product>)response.Result;
            var myList = list.Select(p => new ProductItemViewModel
            {
                Description = p.Description,
                FromWeb = p.FromWeb,
                ImageArray = p.ImageArray,
                ImagePath = p.ImagePath,
                IsAvailable = p.IsAvailable,
                Price = p.Price,
                ProductId = p.ProductId,
                PublishOn = p.PublishOn,
                Remarks = p.Remarks,
            });

            this.Products = new ObservableCollection<ProductItemViewModel>(myList);

           // this.IsRefreshing = false;
        }
    }
}
