namespace xamarinProject.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }

        public bool FromWeb { get; set; }

        [NotMapped]
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noProduct";
                }

                if(FromWeb)
                    return $"http://10.0.0.14:5001/{this.ImagePath}";
                else
                    return $"http://10.0.0.14:5000/{this.ImagePath}";
            }
        }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }

        public override string ToString()
        {
            return this.Description;
        }
    }
}
