namespace xamarinProject.Infraestructure
{
    using System;
    using xamarinProject.ViewModels;

    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
