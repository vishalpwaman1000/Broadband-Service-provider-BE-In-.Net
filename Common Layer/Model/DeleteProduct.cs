using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class DeleteProductRequest
    {
        [Required]
        public int ProductID { get; set; }
    }

    public class DeleteProductResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
