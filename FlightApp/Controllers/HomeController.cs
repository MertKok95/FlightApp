using FlightApp.Dtos;
using FlightApp.Models;
using FlightApp.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace FlightApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITicketRepository _ticketRepository;

        public HomeController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IActionResult Index()
        {
            ServiceReference1.AirProviderClient airProviderClient= new ServiceReference1.AirProviderClient();
            var countryList = airProviderClient.GetCountries();

            if(countryList != null && countryList.Any())
            {
                List<CountryModel> countries = countryList.Select(x => new CountryModel { CountryId = x.CountryId, CountryName = x.CountryName }).ToList();
                return View(new FlightDto { Countries = countries});
            }
            return View(null);
        }

        [HttpGet]
        public IActionResult GetCitiesByCountryId(int countryId)
        {
            ServiceReference1.AirProviderClient airProviderClient = new ServiceReference1.AirProviderClient();
            var cityList = airProviderClient.GetCities(countryId);

            if (cityList != null && cityList.Any())
            {
                List<CityModel> countries = cityList.Select(x => new CityModel { CityId = x.CityId, CountryId = x.CountryId, CityName = x.CityName }).ToList();
                return Json(new { status = true, data = countries });
            }
            return View(null);
        }



        public IActionResult GetFlightOptions([FromBody] SearchRequest searchRequest)
        {
            if(searchRequest != null)
            {
                ServiceReference1.AirProviderClient providerClient = new ServiceReference1.AirProviderClient();
                ServiceReference1.SearchRequest model = new ServiceReference1.SearchRequest { OriginCountryId = searchRequest.OriginCountryId, OriginCityId = searchRequest.OriginCityId, DestinationCountryId = searchRequest.DestinationCountryId, DestinationCityId = searchRequest.DestinationCityId, DepartureDate = searchRequest.DepartureDate, ArrivalDate = searchRequest.ArrivalDate };

                List<ServiceReference1.FlightData> searchFlightList = providerClient.AvailabilitySearch(model).ToList();
                

                if (searchFlightList != null && searchFlightList.Any())
                {
                    if (searchFlightList != null)
                    {
                        List<FlightModel> flightModelList = new List<FlightModel>();
                        foreach (var flightData in searchFlightList)
                        {

                            // bütün bilgiler geçildi.
                            // Fakat doğru kullanımda dto halinde sadece gerekli bilgilerin geçilmesi daha doğru olacaktır.
                            flightModelList.Add(new FlightModel
                            {
                                FlightOption = new FlightOption
                                {
                                    FlightNumber = flightData.FlightOption.FlightNumber,
                                    RouteNumber = flightData.FlightOption.RouteNumber,
                                    Price = flightData.FlightOption.Price,
                                    SeatNumber = flightData.FlightOption.SeatNumber,
                                    OriginAirport = flightData.FlightOption.OriginAirport,
                                },
                                PlaneRoute = new PlaneRoute
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
                                    DestinationDate = flightData.PlaneRoute.ArrivalDateTime,
                                }

                            });
                        }
                        return PartialView("_TicketList", flightModelList);
                    }
                }
            }
            return PartialView("_TicketList");
        }

        [HttpPost]
        public IActionResult GetTicket([FromBody] string flightNumber)
        {
            var ticketInformation = _ticketRepository.GetTicket(flightNumber);
            if (ticketInformation != null)
            {
                return Json(new { status = true, data = ticketInformation });
            }
            return Json(new { status = false });
        }
    }


}
