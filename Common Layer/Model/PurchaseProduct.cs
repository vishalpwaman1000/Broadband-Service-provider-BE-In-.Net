using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class PurchaseProductRequest
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string ProductType { get; set; }
    }

    public class PurchaseProductResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
