namespace xamarinProject.Backend.Models
{
    using Common.Models;
    using Microsoft.AspNetCore.Http;

    public class ProductView : Product
    {
        public IFormFile ImageFile { get; set; }
    }
}
