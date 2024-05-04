using FlightApp.Dtos;
using FlightApp.Models;
using ServiceReference1;

namespace FlightApp.Repositories
{
    public interface ITicketRepository
    {
        List<Countries> GetCountries();
        List<Cities> GetCities(int countryId);
        List<FlightModel> GetAvailableFlightList(ServiceReference1.SearchRequest model);
        FlightDetailDto GetTicket(string flightNumber);
    }
}
