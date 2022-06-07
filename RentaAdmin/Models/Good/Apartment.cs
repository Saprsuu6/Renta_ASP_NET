using System;
using System.ComponentModel.DataAnnotations;

namespace RentaAdmin.Models.Good
{
    public class Apartment
    {
        public Apartment() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "Town is require"),
            MaxLength(50, ErrorMessage = "So long name of town")]
        public string Town { get; set; }
        [Required(ErrorMessage = "Street is require"),
            MaxLength(50, ErrorMessage = "So long name of street")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Type is require"),
            MaxLength(50, ErrorMessage = "So long name of type")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Amount floors is require"),
            Range(1, 100, ErrorMessage = "Range of floors from 1 to 100")]
        public uint Floors { get; set; }
        [Required(ErrorMessage = "Amount rooms is require"),
            Range(1, 10, ErrorMessage = "Range of rooms from 1 to 10")]
        public uint Rooms { get; set; }
        [Required(ErrorMessage = "Layout is require"),
            MaxLength(50, ErrorMessage = "So long name of layout")]
        public string Layout { get; set; }
        [Required(ErrorMessage = "Condition is require"),
            MaxLength(50, ErrorMessage = "So long name of condition")]
        public string Condition { get; set; }
        [Required(ErrorMessage = "Length is require"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "Not right length format")]
        public float Length { get; set; }
        [Required(ErrorMessage = "Width is require"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "Not right width format")]
        public float Width { get; set; }
        [Required(ErrorMessage = "Height is require"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "Not right height format")]
        public float Height { get; set; }
        [Required(ErrorMessage = "GoodId is require")]
        public int GoodId { get; set; }
        //[Required(ErrorMessage = "Good is require")]
        //public virtual Good Good { get; set; }
    }
}
