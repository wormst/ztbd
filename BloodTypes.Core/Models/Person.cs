using System;

namespace BloodTypes.Core.Models
{
    public class Person
    {
        public string Id { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Birthdate { get; set; }
        public string Telephone { get; set; }
        public string BloodType { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
