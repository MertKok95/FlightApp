using System.Runtime.Serialization;

namespace FlightApp.Models
{
    public class SearchRequest
    {
        public int OriginCountryId { get; set; }
        public int DestinationCountryId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
    }
}
