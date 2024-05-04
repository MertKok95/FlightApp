using FlightApp.Dtos;
using FlightApp.Models;
using ServiceReference1;

namespace FlightApp.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        AirProviderClient providerClient;

        public TicketRepository() {
            providerClient = new ServiceReference1.AirProviderClient();
        }


        public List<Countries> GetCountries() {
            return providerClient.GetCountries().ToList();
        }

        public List<Cities> GetCities(int countryId)
        {
            return providerClient.GetCities(countryId).ToList();
        }

        public List<FlightModel> GetAvailableFlightList(ServiceReference1.SearchRequest model)
        {
            List<FlightModel> flightModelList = new List<FlightModel>();
            List<ServiceReference1.FlightData> searchFlightList = providerClient.AvailabilitySearch(model).ToList();

            if (searchFlightList != null && searchFlightList.Any())
            {
                if (searchFlightList != null)
                {
                    foreach (var flightData in searchFlightList)
                    {
                        // bütün bilgiler geçildi.
                        // Fakat doğru kullanımda dto halinde sadece gerekli bilgilerin geçilmesi daha doğru olacaktır.
                        // koltuk senaryosu için seat alanı açıldı fakat aktif edilmedi.

                        flightModelList.Add(new FlightModel
                        {
                            FlightOption = new Models.FlightOption
                            {
                                FlightNumber = flightData.FlightOption.FlightNumber,
                                RouteNumber = flightData.FlightOption.RouteNumber,
                                Price = flightData.FlightOption.Price,
                                SeatNumber = flightData.FlightOption.SeatNumber,
                                OriginAirport = flightData.FlightOption.OriginAirport,
                                DestinationAirport = flightData.FlightOption.DestinationAirport,
                            },
                            PlaneRoute = new Models.PlaneRoute
                            {
                                RouteNumber = flightData.PlaneRoute.RouteNumber,
                                OriginCountryId = flightData.PlaneRoute.OriginCountryId,
                                OriginCityId = flightData.PlaneRoute.OriginCityId,
                                DestinationCountryId = flightData.PlaneRoute.DestinationCountryId,
                                DestionationCityId = flightData.PlaneRoute.DestionationCityId,
                                DepartureDateTime = flightData.PlaneRoute.DepartureDateTime,
                                ArrivalDateTime = flightData.PlaneRoute.ArrivalDateTime,
                            },
                            FlightDetailDto = new FlightDetailDto
                            {
                                FlightNumber = flightData.FlightOption.FlightNumber,
                                OriginCountry = providerClient.GetCountryById(flightData.PlaneRoute.OriginCountryId),
                                OriginCity = providerClient.GetCityById(flightData.PlaneRoute.OriginCountryId),
                                DestinationCountry = providerClient.GetCityById(flightData.PlaneRoute.DestinationCountryId),
                                DestinationCity = providerClient.GetCityById(flightData.PlaneRoute.DestionationCityId),
                                DepartureDate = flightData.PlaneRoute.DepartureDateTime,
                                DestinationDate = flightData.PlaneRoute.ArrivalDateTime
                            }
                        });
                    }
                }
            }
            return flightModelList;
        }

        public FlightDetailDto GetTicket(string flightNumber)
        {
            var model = providerClient.GetTicket(flightNumber);

            return new FlightDetailDto
            {
                FlightNumber = model.FlightNumber,
                OriginCountry = model.OriginCountry,
                OriginCity = model.OriginCity,
                OriginAirport = model.OriginAirport,
                DepartureDate = model.DepartureDate,
                DestinationCountry = model.DestinationCountry,
                DestinationCity = model.DestinationCity,
                DestinationAirport = model.DestinationAirport,
                DestinationDate = model.DestinationDate,
                Price = model.Price.ToString("#.##")
            };
        }

    }
}
