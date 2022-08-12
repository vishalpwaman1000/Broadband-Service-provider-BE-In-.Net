using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
   public class GetAllProductRequest
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int NumberOfRecordPerPage { get; set; }
    }

    public class GetAllProductResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int CurrentPage { get; set; }
        public double TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public List<Product> data { get; set; }
    }
}
