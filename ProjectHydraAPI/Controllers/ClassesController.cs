using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.Models;

namespace ProjectHydraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly HydraDbContext _context;
        private readonly IMapper _mapper;
        public ClassesController(HydraDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassVM>> GetClass(int id)
        {
            var lesson = await _context.Classes.FindAsync(id);

            if (lesson == null)
            {
                return NotFound();
            }

            return _mapper.Map<Class, ClassVM>(lesson);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, [FromBody] Class lesson)
        {
            if (id != lesson.Id)
            {
                return BadRequest();
            }

            _context.Entry(lesson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ClassExist(int id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult<Class>> PostUnit([FromBody]Class lesson)
        {
            _context.Classes.Add(lesson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClass", new { id = lesson.Id }, lesson);
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassVM>>> GetClasses()
        {
            var classes = await _context.Classes.Include(c=>c.Teacher).ThenInclude(t=>t.Rank).Include(c=>c.Unit).ToListAsync();
            return _mapper.Map<List<Class>, List<ClassVM>>(classes);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Class>>> GetUserClasses(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            var unit = await _context.Units.Include(u => u.Parent).Where(u => u.Id == user.UnitId).FirstOrDefaultAsync();
            if(unit == null)
            {
                return NotFound();
            }
            IList<int> ParentUnitsIds = new List<int>();

            do
            {
                ParentUnitsIds.Add(unit.Id);
                unit = await _context.Units.FindAsync(unit.ParentId);
            } while (unit != null);

            try
            {
                var classes = await _context.Classes.Include(c => c.Teacher).ThenInclude(t => t.Rank).Where(c => ParentUnitsIds.Contains(c.UnitId.GetValueOrDefault())).ToListAsync();
                return Ok(_mapper.Map<List<Class>, List<ClassVM>>(classes));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
