namespace xamarinProject.ViewModels
{
    using System;
    public class MainViewModel
    {

        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            Products = new ProductsViewModel();
        }
    }
}
