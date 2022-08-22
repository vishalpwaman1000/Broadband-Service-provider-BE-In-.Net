using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_Layer.Model
{
    public class CreateTicketRequest
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string Reportor { get; set; }

        [Required]
        public string PlanType { get; set; }

        [Required]
        public string RaiseType { get; set; }

        [Required]
        public string Summary { get; set; }


        public string Description { get; set; }
    }

    public class CreateTicketResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
