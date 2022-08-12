using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class UpdateProductRequest
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string ProductPrice { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public string PublicID { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }
    }

    public class UpdateProductResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
