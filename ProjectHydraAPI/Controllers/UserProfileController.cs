using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.ViewModels;

namespace ProjectHydraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly HydraDbContext _context;
        public UserProfileController(UserManager<AppUser> userManager, HydraDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return Ok(new
            {
                user.FirstName,
                user.LastName,
                user.Email
            });
        }
        [HttpGet("unit")]
        [Authorize]
        public async Task<IActionResult> GetUserUnit()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return Ok(new
                {
                    user.UnitId
                });
        }
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutUser(string id, [FromBody] UserDetailsModel user)
        {
            if(id != User.Claims.First(c => c.Type == "UserID").Value){
                return Forbid();
            }
            if (id != user.Id)
            {
                return BadRequest();
            }

            var userDb = await _context.Users.FindAsync(id);

            if (userDb == null)
            {
                return NotFound();
            }

            userDb.Birthday = DateTime.Parse(user.Birthday);
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.PhoneNumber = user.PhoneNumber;
            userDb.UserName = user.Username;
            userDb.NormalizedUserName = user.Username.ToUpper();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost("changepass/{id}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePassModel pass)
        {
            if (id != User.Claims.First(c => c.Type == "UserID").Value)
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDb = _context.Users.Find(pass.UserID);
            if(userDb == null)
            {
                return BadRequest();
            }

            userDb.PasswordHash = _userManager.PasswordHasher.HashPassword(userDb, pass.NewPassword);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

    }
}
