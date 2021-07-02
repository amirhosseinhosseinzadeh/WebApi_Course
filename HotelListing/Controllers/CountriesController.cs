using HotelListing.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelListing.IRepository;
using AutoMapper;
using System.Collections.Generic;
using HotelListing.Domains;
using System;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CountriesController> _logger;
        public CountriesController(IUnitOfWork unitOfWork,
                                   IMapper mapper,
                                   ILogger<CountriesController> logger
                                )
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var countries = await _unitOfWork.Countries.GetAllAsync();
                var model = _mapper.Map<IList<CountryModel>>(countries);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
            }
            return StatusCode(400,"Request was not valid");
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> GetCountry(int id)
        {
            var country = await _unitOfWork.Countries.GetAsync(c => c.Id == id, new List<string> { "HotelList" });
            var result = _mapper.Map<CountryModel>(country);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateCountry")]
        public async Task<IActionResult> CreateCountry(CreateCountryModel model)
        {
            var country = _mapper.Map<Country>(model);
            await _unitOfWork.Countries.InsertAsync(country);
            await _unitOfWork.SaveAsync();
            return Ok(new { Status = "Ok", Message = "Insert completed successfully !" });
        }
    }
}

