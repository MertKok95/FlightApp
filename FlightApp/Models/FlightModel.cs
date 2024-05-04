using FlightApp.Dtos;
using System.Runtime.Serialization;

namespace FlightApp.Models
{
    public class FlightModel
    {
        public FlightOption FlightOption { get; set; }
        public PlaneRoute PlaneRoute { get; set; }
        public FlightDetailDto FlightDetailDto { get; set; }
    }

    public class FlightOption
    {
        public string FlightNumber { get; set; } = string.Empty;
        public string RouteNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string SeatNumber { get; set; }
        public string OriginAirport { get; set; } = string.Empty;
        public string DestinationAirport { get; set; } = string.Empty;
    }

    public class PlaneRoute
    {
        public string RouteNumber { get; set; } = string.Empty;
        public int OriginCountryId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCountryId { get; set; }
        public int DestionationCityId { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
    }
}
