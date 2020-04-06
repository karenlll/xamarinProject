namespace xamarinProject.ViewModels
{
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;
    using xamarinProject.Helpers;
    using xamarinProject.Views;

    public class ProductItemViewModel : Product
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public ProductItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands
        public ICommand DeleteProductCommand
        {
            get
            {
                return new RelayCommand(DeleteProduct);
            }
        }

        private async void DeleteProduct()
        {
            var answer = await Application.Current.MainPage.DisplayAlert
                (Languages.Delete,
                Languages.DeleteConfirmation,
                Languages.Yes,
                Languages.No);

            if (!answer)
            {
                return;
            }
            else
            {
                var connection = await this.apiService.CheckConnection();

                if (!connection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                    return;
                }

                var url = Application.Current.Resources["UrlApi"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlProductsController"].ToString();

                var response = await this.apiService.Delete(url, prefix, controller, this.ProductId);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                    return;
                }

                var viewProducts = ProductsViewModel.GetInstance();
                viewProducts.Products.Remove(this);

            }
        }

        public ICommand EditProductCommand
        {
            get
            {
                return new RelayCommand(GoToEditProduct);
            }
        }

        private async void GoToEditProduct()
        {
            MainViewModel.GetInstance().ManageProduct = new ManageProductsViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new ManageProductsPage());
        }
        #endregion

    }
}
