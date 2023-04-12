using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MvcWebUI.Controllers
{
    [Route("[controller]")] 
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [Route("[action]/{countryId?}")] 
        public IActionResult Index(int? countryId)
        {
            var cities = _cityService.GetCities(countryId ?? 0);
            return Json(cities);
        }
    }
}
