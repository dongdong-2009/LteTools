using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lte.Domain.Geo.Abstract;

namespace Lte.Parameters.Region.Entities
{
    public class Town : ITown
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string CityName { get; set; }

        [MaxLength(20)]
        public string DistrictName { get; set; }

        [MaxLength(20)]
        public string TownName { get; set; }
    }

    public class OptimizeRegion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string District { get; set; }
    }
}
