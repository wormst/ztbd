using System.ComponentModel.DataAnnotations;

namespace BloodTypes.Core.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "A+")]
        public int Aplus { get; set; }

        [Display(Name = "A-")]
        public int Aminus { get; set; }

        [Display(Name = "B+")]
        public int Bplus { get; set; }

        [Display(Name = "B-")]
        public int Bminus { get; set; }

        [Display(Name = "AB+")]
        public int ABplus { get; set; }

        [Display(Name = "AB-")]
        public int ABminus { get; set; }

        [Display(Name = "0+")]
        public int Oplus { get; set; }

        [Display(Name = "0-")]
        public int Ominus { get; set; }
    }
}
