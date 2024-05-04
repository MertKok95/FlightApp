using FlightApp.Models;

namespace FlightApp.Dtos
{
    public class FlightDto
    {
        public List<CountryModel> Countries { get; set; }
        public FlightDetailDto FlightDetails { get; set; }

    }
}
