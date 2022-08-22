using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
        public bool IsActive { get; set; }

    }
    
    public class UserPersonalDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonalDetailID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string HomePhone { get; set; }
        public string PersonalNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Occupation { get; set; }
        public string CompanyName { get; set; }
        public string Position { get; set; }
    }

    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public DateTime InsertionDate { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPrice { get; set; }
        public string ProductType { get; set; }
        public string ProductImageUrl { get; set; }
        public string PublicID { get; set; }
        public bool IsActive { get; set; }
    }

    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }
        public DateTime InsertionDate { get; set; }
        public int UserID { get; set; }
        public string Reportor { get; set; }
        public string Assigner { get; set; }
        public string PlanType { get; set; }
        public string RaiseType { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } //pending, Inprogress, Closed, ReadyToTesting
        //InsertionDate, UserID, Reportor, Assigner, PlanType, RaiseType, Summary, Description, Status
    }

    public class PurchaseProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PurchaseId { get; set; }
        public DateTime InsertionDate { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductType { get; set; } // Device, Plan
    }
}
