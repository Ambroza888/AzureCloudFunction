using System.ComponentModel.DataAnnotations;


namespace Valence.Models
{
    public class LocationParams
    {
        [Required]
        public string Location { get; set; }
        [Required]
        public string Categories { get; set; }
    }
}