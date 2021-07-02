using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.IRepository;
using HotelListing.Models;
using System.Collections.Generic;
using HotelListing.Domains;

namespace HotelListing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public HotelsController(IUnitOfWork unitOfWork,
                                IMapper mapper
        )
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync();
            var models = _mapper.Map<IList<HotelModel>>(hotels);
            return Ok(models);
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetAsync(h => h.Id == id);
            var model = _mapper.Map<HotelModel>(hotel);
            return Ok(model);
        }

        [HttpPost]
        [Route("CreateHotel")]
        public async Task<IActionResult> CreateHotel(CreateHotelModel model)
        {
            var hotel = _mapper.Map<Hotel>(model);
            await _unitOfWork.Hotels.InsertAsync(hotel);
            await _unitOfWork.SaveAsync();

            return Ok(new { Status = "Ok", Message = "Insert completed successfully !" });
        }
    }
}