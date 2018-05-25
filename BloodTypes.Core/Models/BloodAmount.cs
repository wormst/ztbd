using System.ComponentModel.DataAnnotations;

namespace BloodTypes.Core.Models
{
    public class BloodAmount
    {
        public string Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Display(Name = "Amount in liters")]
        public double Amount { get; set; }
    }
}
