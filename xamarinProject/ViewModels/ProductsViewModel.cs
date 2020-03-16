namespace xamarinProject.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        private ObservableCollection<Product> products;

        private bool isRefreshing;


        private ApiService apiService;

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set { this.SetValue(ref this.products, value); }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }

        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error ", connection.Message, "Accept");
                return;
            }
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlApi"].ToString();
            //await Task.Delay(1000);  //this task delay has fixed my issue. 

            var response = await this.apiService.GetList<Product>(url, "/api", "/Product");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error ", response.Message, "Accept");
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);

            this.IsRefreshing = false;
        }
    }
}
