using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class GetPurchaseProductRequest
    {
        [Required]
        public int PageNumber { get; set; }
        [Required]
        public int NumberOfRecordPerPage { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string ProductType { get; set; } // all, device, plan

    }

    public class GetPurchaseProductResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int CurrentPage { get; set; }
        public double TotalRecords { get; set; }
        public int TotalPage { get; set; }
        public List<PurchaseProductDetail> data { get; set; }
    }

    public class PurchaseProductDetail
    {
        //PurchaseId,InsertionDate,UserID,ProductID,ProductType,ProductName
        //ProductDescription,ProductPrice,ProductImageUrl,PublicID,IsActive
        public int PurchaseId { get; set; }
        public DateTime InsertionDate { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string PublicID { get; set; }
        public bool IsActive { get; set; }
    }
}
