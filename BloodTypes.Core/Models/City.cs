using System.ComponentModel.DataAnnotations;

namespace BloodTypes.Core.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
