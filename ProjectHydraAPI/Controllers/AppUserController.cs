using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.ViewModels;

namespace ProjectHydraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    { 
        private readonly UserManager<AppUser> _userManager;
        private readonly HydraDbContext _db;
        private readonly IMapper _mapper;

        public AppUserController(UserManager<AppUser> userManager, HydraDbContext db, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetUsers()
        {
            var users = await _db.Users.Include(x => x.Rank).ToListAsync();
            var userVM = _mapper.Map<List<AppUser>, List<UserViewModel>>(users);
            return userVM;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsModel>> GetUser(string id)
        {
            var user = await _db.Users.Include(u => u.Rank)
                                      .Include(u => u.Unit)
                                      .FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return _mapper.Map<AppUser, UserDetailsModel>(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, [FromBody] UserDetailsModel user)
        {
            if( id != user.Id)
            {
                return BadRequest();
            }

            var userDb = await _db.Users.FindAsync(id);

            if(userDb == null)
            {
                return NotFound();
            }

            var rank = await _db.Ranks.FindAsync(user.RankId);

            if(rank == null)
            {
                return NotFound();
            }

            userDb.Birthday = DateTime.Parse(user.Birthday);
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.PhoneNumber = user.PhoneNumber;
            userDb.RankId = rank.Id;
            userDb.UserName = user.Username;
            userDb.NormalizedUserName = user.Username.ToUpper();
            userDb.UnitId = user.UnitId;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch
            {
                throw;
            }

            return NoContent();
        }

        [HttpPut("{id}/rank")]
        public async Task<IActionResult> PutUserRank(string id, [FromBody] int rankId)
        {
            
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            var rankDb =_db.Ranks.Find(rankId);

            if(rankDb == null)
            {
                return NotFound();
            }

            user.Rank = rankDb;
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("selectList")]
        public async Task<ActionResult<List<UserViewModel>>> GetUsersForSelectList()
        {
            var ranks = await _db.Users.Include(u => u.Rank).ToListAsync();
            var listOfRanks = _mapper.Map<List<AppUser>, List<UserViewModel>>(ranks);
            return listOfRanks;
        }
    }
}
