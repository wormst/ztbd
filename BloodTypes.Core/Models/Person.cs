using System;
using System.ComponentModel.DataAnnotations;

namespace BloodTypes.Core.Models
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public class Person
    {
        public string Id { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }

        public string Country { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        public string BloodType { get; set; }

        [Range(20, 200)]
        public double? Weight { get; set; }

        [Range(80, 250)]
        public double? Height { get; set; }
    }
}
