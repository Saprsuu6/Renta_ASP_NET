using System;
using System.ComponentModel.DataAnnotations;

namespace RentaAppication.Models
{
    public class Order
    {
        public Order() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Client is require")]
        public virtual Client.Client Client { get; set; }
        [Required(ErrorMessage = "Good is require")]
        public virtual Good.Good Good { get; set; }
        [Required(ErrorMessage = "Date is require")]
        public DateTime Date { get; set; }
    }
}
