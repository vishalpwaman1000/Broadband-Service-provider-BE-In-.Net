using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class AddProductRequest
    {
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDescription { get; set; }

        [Required]
        public string ProductPrice { get; set; }

        [Required]
        public string ProductType { get; set; }
        public IFormFile File { get; set; }
    }

    public class AddProductResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
