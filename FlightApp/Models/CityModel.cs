using System.Runtime.Serialization;

namespace FlightApp.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}
