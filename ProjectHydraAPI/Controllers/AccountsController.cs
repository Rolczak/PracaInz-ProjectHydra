using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.Helpers;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.Validators;
using ProjectHydraAPI.ViewModels;

namespace ProjectHydraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly HydraDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AccountsController(HydraDbContext dbContext, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdentity = _mapper.Map<AppUser>(model);
            try
            {
                var result = await _userManager.CreateAsync(userIdentity, model.Password);
                await _userManager.AddToRoleAsync(userIdentity, RolesStrings.User);
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult("Account created");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
