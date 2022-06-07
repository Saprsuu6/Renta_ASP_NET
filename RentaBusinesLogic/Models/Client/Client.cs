using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaBusinesLogic.Models.Client
{
    public class Client
    {
        public Client() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Firstname is require"),
            RegularExpression(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$",
            ErrorMessage = "Not right firstname form"),
            MaxLength(50, ErrorMessage = "So long fistname")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is require"),
            RegularExpression(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$",
            ErrorMessage = "Not right lastname form"),
            MaxLength(50, ErrorMessage = "So long lastname")]
        public string Lastname { get; set; }
        [RegularExpression(@"^\+380\d{3}\d{2}\d{2}\d{2}$",
            ErrorMessage = "Not right phone number form"),
            MaxLength(50, ErrorMessage = "So long phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Password is require")]
        public string Password { get; set; }
        [RegularExpression(@"^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$",
            ErrorMessage = "Not right email form"),
            MaxLength(50, ErrorMessage = "So long email")]
        public string Email { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
        public virtual IEnumerable<Good.Good> Goods { get; set; }
    }
}
