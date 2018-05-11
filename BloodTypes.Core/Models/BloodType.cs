using System.ComponentModel.DataAnnotations;

namespace BloodTypes.Core.Models
{
    public class BloodType
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }


        public int Quantity { get; set; }
    }
}
