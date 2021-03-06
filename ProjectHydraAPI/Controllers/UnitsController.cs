﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectHydraAPI.DataAccess;
using ProjectHydraAPI.Models;
using ProjectHydraAPI.ViewModels;

namespace ProjectHydraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly HydraDbContext _context;
        private readonly IMapper _mapper;

        public UnitsController(HydraDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<List<UnitVM>>> GetUnits()
        {
            var units = await _context.Units
                .Include(u => u.Commander)
                .ThenInclude(c => c.Rank)
                .Include(u => u.DeputyCommander)
                .ThenInclude(dc => dc.Rank)
                .ToListAsync();

            return _mapper.Map<List<Unit>, List<UnitVM>>(units);
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnitDetails>> GetUnit(int id)
        {
            var unit = await _context.Units
                .Include(u => u.SoldiersInUnit).ThenInclude(u=>u.Rank)
                .Include(u => u.Commander).ThenInclude(user => user.Rank)
                .Include(u => u.DeputyCommander).ThenInclude(user => user.Rank)
                .Include(u => u.ChildernUnits)
                .Include(u => u.Parent).ThenInclude(u => u.Commander).ThenInclude(u=>u.Rank)
                .Include(u => u.Parent).ThenInclude(u => u.DeputyCommander).ThenInclude(u => u.Rank)
                .Include(u => u.Parent).ThenInclude(u => u.Parent)
                .Where(u => u.Id == id).FirstOrDefaultAsync();

            if (unit == null)
            {
                return NotFound();
            }

            return _mapper.Map<Unit, UnitDetails>(unit);
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUnit(int id, [FromBody] Unit unit)
        {
            if (id != unit.Id)
            {
                return BadRequest();
            }

            _context.Entry(unit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
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

        // POST: api/Units
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnit", new { id = unit.Id }, unit);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteUnit(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit == null)
            {
                return NotFound();
            }

            _context.Units.Remove(unit);
            await _context.SaveChangesAsync();

            return unit;
        }

        [HttpGet("{id}/soldiers")]
        public async Task<ActionResult<List<UserViewModel>>> GetSoldiersInUnit(int id)
        {
            var unit = await _context
                .Units
                .Include(u => u.SoldiersInUnit)
                .ThenInclude(u => u.Rank)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (unit == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<AppUser>, List<UserViewModel>>(unit.SoldiersInUnit);
        }

        private bool UnitExists(int id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
