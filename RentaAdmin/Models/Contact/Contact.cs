using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAdmin.Models.Contact
{
    public class Contact
    {
        public Contact() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Email is require"), 
            RegularExpression(@"^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$",
            ErrorMessage = "Not right email form"),
            MaxLength(50, ErrorMessage = "So long email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Location is require"),
            MaxLength(50, ErrorMessage = "So long name of loaction")]
        public string CountryTownStreet { get; set; }
        [Required(ErrorMessage = "Phones is require")]
        public virtual IEnumerable<Phone> Phones { get; set; }
    }
}
