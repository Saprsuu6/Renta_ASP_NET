using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Models.Client
{
    public class Client
    {
        public Client() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "FirstnameRequire"),
            RegularExpression(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$",
            ErrorMessage = "FirstnameForm"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "LastnameRequire"),
            RegularExpression(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$",
            ErrorMessage = "LastnameForm"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "PhoneRequire"),
            RegularExpression(@"^\+380\d{3}\d{2}\d{2}\d{2}$",
            ErrorMessage = "PhoneNumberForm"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "PasswordRequire"),
            RegularExpression(@"^([a-z]+[A-Z]+[0-9]+|[a-z]+[0-9]+[A-Z]+|[A-Z]+[a-z]+[0-9]+|[A-Z]+[0-9]+[a-z]+|[0-9]+[a-z]+[A-Z]+|[0-9]+[A-Z]+[a-z]+)$",
            ErrorMessage = "Length"),]
        public string Password { get; set; }
        [Required(ErrorMessage = "EmailRequire"), 
            RegularExpression(@"^((([0-9A-Za-z]{1}[-0-9A-z\.]{1,}[0-9A-Za-z]{1})|([0-9А-Яа-я]{1}[-0-9А-я\.]{1,}[0-9А-Яа-я]{1}))@([-A-Za-z]{1,}\.){1,2}[-A-Za-z]{2,})$",
            ErrorMessage = "EmailForm"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Email { get; set; }
        public virtual IEnumerable<Payment> Payments { get; set; }
        public virtual IEnumerable<Good.Good> Goods { get; set; }
    }
}
