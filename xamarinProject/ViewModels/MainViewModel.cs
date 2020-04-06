namespace xamarinProject.ViewModels
{
    using System;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using xamarinProject.Views;

    public class MainViewModel
    {

        public ProductsViewModel Products { get; set; }

        public ManageProductsViewModel ManageProduct { get; set; }

        private MainViewModel()
        {
            instance = this;
            Products = ProductsViewModel.GetInstance();
        }

        #region Singleton

        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }

        #endregion

        public ICommand AddProductCommand
        {
            get
            {
                return new RelayCommand(GoToAddProduct);
            }
        }

        private async void GoToAddProduct()
        {
            this.ManageProduct = new ManageProductsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new ManageProductsPage());
        }
    }
}
