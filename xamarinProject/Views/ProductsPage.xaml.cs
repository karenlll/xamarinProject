using System;
using System.Collections.Generic;

using Xamarin.Forms;
using xamarinProject.ViewModels;

namespace xamarinProject.Views
{
    public partial class ProductsPage : ContentPage
    {
        List<string> listItems = new List<string>();

        public ProductsPage()
        {
            InitializeComponent();

            listProducts.RefreshCommand = new Command(() => {
                //Do your stuff.    
                ProductsViewModel.GetInstance().LoadProducts();
                listProducts.IsRefreshing = false;
            });
        }
    }
}
