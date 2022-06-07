using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAPI.Models.Client
{
    public class Payment
    {
        public Payment() { }

        public int Id { get; set; }
        //[Required(ErrorMessage = "Client is require")]
        //public Client Client { get; set; }
        [Required(ErrorMessage = "Provider is require"),
            MaxLength(50, ErrorMessage = "So long name of provider")]
        public string Provider { get; set; }
        [Required(ErrorMessage = "Number is require"),
            RegularExpression(@"^\d{4}([-]|)\d{4}([-]|)\d{4}([-]|)\d{4}$",
            ErrorMessage = "Not right card number form"),
            MaxLength(50, ErrorMessage = "So long number")]
        public string Number { get; set; }
    }
}
