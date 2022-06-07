using System;
using System.ComponentModel.DataAnnotations;

namespace RentaBusinesLogic.Models.Good
{
    public class Good
    {
        public Good() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Date of add is require")]
        public DateTime DateOfAdd { get; set; }
        [Required(ErrorMessage = "Date of update is require")]
        public DateTime DateOfUpdate { get; set; }
        [Required(ErrorMessage = "Watchings is require"),
            Range(0, 10000, ErrorMessage = "Range of watchings from 1 to 10000")]
        public uint Watchings { get; set; }
        [Required(ErrorMessage = "Link is require"),
            RegularExpression(@"^(https?:\/\/)?([\w-]{1,32}\.[\w-]{1,32})[^\s@]*$",
            ErrorMessage = "Not right link format"),
            MaxLength(1000, ErrorMessage = "So much words")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Describe is require"),
            MaxLength(1000, ErrorMessage = "So much words")]
        public string Describe { get; set; }
        [Required(ErrorMessage = "Price is require"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "Not right price format")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Status is require"),
            MaxLength(50, ErrorMessage = "So much words")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Apartment is require")]
        public virtual Apartment Apartment { get; set; }
        //[Required(ErrorMessage = "Rieltor is require")]
        //public virtual Client.Client Rieltor { get; set; }
    }
}
