using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentaAppication.Models.Client
{
    public class Payment
    {
        public Payment() { }

        public int Id { get; set; }
        //[Required(ErrorMessage = "Client is require")]
        //public Client Client { get; set; }
        [Required(ErrorMessage = "ProviderRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Provider { get; set; }
        [Required(ErrorMessage = "NumberRequire"),
            RegularExpression(@"^\d{4}([-]|)\d{4}([-]|)\d{4}([-]|)\d{4}$",
            ErrorMessage = "NumberForm"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Number { get; set; }
    }
}
