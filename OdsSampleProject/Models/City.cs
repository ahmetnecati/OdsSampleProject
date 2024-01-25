using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdsSampleProject.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string CityName { get; set; }
        [ForeignKey("Region")]
        public int RegionId { get; set; }
        public Region Region { get; set; }
    }
}
