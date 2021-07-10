using System;
using AutoMapper;
using System.Linq;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(UserManager<ApiUser> userManager,
                                 ILogger<AccountController> logger,
                                 IMapper mapper,
                                 IAuthManager authManager
        )
        {
            this._userManager = userManager;
            this._logger = logger;
            this._mapper = mapper;
            this._authManager = authManager;
        }


        private IList<T> ReturnModelErrors<T>(IEnumerable<T> errors)
        {
            var errorList = new List<T>();
            foreach (var error in errors)
            {
                errorList.Add(error);
            }
            return errorList;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
                ModelState.AddModelError(nameof(model.Email), "Email is not correct");
            if (string.IsNullOrWhiteSpace(model.Passwrod))
                ModelState.AddModelError(nameof(model.Passwrod), "passwod is not correct");
            if (ModelState.ErrorCount <= 0)
            {
                var userVerified = await _authManager.ValidateUserAsync(model);
                if (userVerified)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var token = await _authManager.GenerateTokenAsync(user);
                    return Ok(new { Status = "Ok", Token = $"{token}" });
                }
                ModelState.AddModelError("", "No user found with this email and password");
            }
            var errorCollection = ModelState.Values.SelectMany(val => val.Errors,
              (exception, errorMessage) => new { errorMessage });

            var errors = ReturnModelErrors(errorCollection);
            var jsonResponse = new JsonResult(errors);
            jsonResponse.StatusCode = 400;
            jsonResponse.ContentType = "Json";
            return BadRequest(jsonResponse);
        }

        [HttpPost]
        [Route("Signin")]
        public async Task<IActionResult> Register([FromBody] SignInModel model)
        {
            _logger.LogInformation($"Registration attempt for user {model.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var user = _mapper.Map<ApiUser>(model);
                if (model.UseDefaultUserName)
                {
                    user.UserName = model.FirstName + "_" + user.LastName;
                }
                var result = await _userManager.CreateAsync(user, model.Passwrod);

                if (!result.Succeeded)
                {
                    var errors = new List<object>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(new { errcode = error.Code, errorDesc = error.Description });
                    }
                    return BadRequest(new JsonResult(errors));
                }
                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong during register new user in {nameof(Register)}");
                return Problem();
            }
        }
    }
}
