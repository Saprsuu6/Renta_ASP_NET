using System;
using System.ComponentModel.DataAnnotations;

namespace RentaAppication.Models.Good
{
    public class Apartment
    {
        public Apartment() { }

        public int Id { get; set; }
        [Required(ErrorMessage = "TownRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Town { get; set; }
        [Required(ErrorMessage = "StreetRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Street { get; set; }
        [Required(ErrorMessage = "TypeRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Type { get; set; }
        [Required(ErrorMessage = "FloorsRequire"),
            Range(1, 100, ErrorMessage = "RangeFloors")]
        public uint Floors { get; set; }
        [Required(ErrorMessage = "RoomsRequire"),
            Range(1, 10, ErrorMessage = "RangeRooms")]
        public uint Rooms { get; set; }
        [Required(ErrorMessage = "LayoutRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Layout { get; set; }
        [Required(ErrorMessage = "ConditionRequire"),
            MaxLength(50, ErrorMessage = "Length")]
        public string Condition { get; set; }
        [Required(ErrorMessage = "LengthRequire"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "LengthFormat")]
        public float Length { get; set; }
        [Required(ErrorMessage = "WidthRequire"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "WidthFormat")]
        public float Width { get; set; }
        [Required(ErrorMessage = "HeightRequire"),
            RegularExpression(@"^(0|[1-9]\d*)([.,]\d+)?",
            ErrorMessage = "HeightFormat")]
        public float Height { get; set; }
        [Required(ErrorMessage = "GoodIdRequire")]
        public int GoodId { get; set; }
        //[Required(ErrorMessage = "Good is require")]
        //public virtual Good Good { get; set; }
    }
}
