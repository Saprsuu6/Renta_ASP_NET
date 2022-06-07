using System;
using System.ComponentModel.DataAnnotations;

namespace RentaAppication.Models.Good
{
    public class Good
    {
        public Good() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "DateAddRequire")]
        public DateTime DateOfAdd { get; set; }
        [Required(ErrorMessage = "DateUpdateRequire")]
        public DateTime DateOfUpdate { get; set; }
        [Required(ErrorMessage = "WatchingsRequire")]
        public uint Watchings { get; set; }
        [Required(ErrorMessage = "LinkRequire"),
            RegularExpression(@"^(https?:\/\/)?([\w-]{1,32}\.[\w-]{1,32})[^\s@]*$",
            ErrorMessage = "LinkFormat")]
        public string Link { get; set; }
        [Required(ErrorMessage = "DescribeRequire"),
            MaxLength(1000, ErrorMessage = "WordthLength")]
        public string Describe { get; set; }
        [Required(ErrorMessage = "PriceRequire"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "PriceFormat")]
        public float Price { get; set; }
        [Required(ErrorMessage = "StatusRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Status { get; set; }
        [Required(ErrorMessage = "ApartmentRequire")]
        public virtual Apartment Apartment { get; set; }
        //[Required(ErrorMessage = "Rieltor is require")]
        //public virtual Client.Client Rieltor { get; set; }
    }
}
