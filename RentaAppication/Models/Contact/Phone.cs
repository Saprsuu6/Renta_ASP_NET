using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Models.Contact
{
    public class Phone
    {
        public Phone() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Number is require"),
            RegularExpression(@"^\+380\d{3}\d{2}\d{2}\d{2}$",
            ErrorMessage = "Not right phone number form"),
            MaxLength(50, ErrorMessage = "So long number")]
        public string Number { get; set; }
        //[Required(ErrorMessage = "Contact is require")]
        //public virtual Contact Contact { get; set; }
    }
}
