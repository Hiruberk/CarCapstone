using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarCapStoneAPI.Models;

namespace CarCapStoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarDbContext _context;

        public CarsController(CarDbContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //if(_context.Cars.Count(x => x.Model.Contains(search)) == 0)
        //{
        //    if(_context.Cars.Count(x => x.Color.Contains(search)) == 0)
        //    {
        //        if(_context.Cars.Count(x => x.Make.Contains(search)) == 0)
        //        {
        //            if(_context.Cars.Count(x => x.Year.ToString().Contains(search)) == 0)
        //            {
        //                if(_context.Cars.Count(x => x.Id.ToString().Contains(search)) == 0)
        //                {
        //                    return NotFound();
        //                }
        //            }
        //        }
        //    }
        //}            


        //[HttpGet("Search")]
        //public async Task<ActionResult<IEnumerable<Car>>> Search([FromQuery] string make = null, [FromQuery] string model = null, [FromQuery] string color = null, [FromQuery] int year = 0)
        //{
        //    Task<List<Car>> query;
        //    if (make != null)
        //    {
        //        query = query.Where(x => make == null || x.Make == make).Where(x => x.Model == model).Where(x => x.Color == color).Where(x => x.Year.ToString() == year.ToString()).ToListAsync();
        //    }




        //if (make == null && model == null && color == null && year == 0)//all not filled returns full list
        //{
        //    return await _context.Cars.ToListAsync();
        //}
        //string yr = null;
        //if (year != 0)
        //{
        //    yr = year.ToString();
        //}

        //Task<List<Car>> inventory = null;


        //if (make != null && model != null && color != null && year != 0)//All are filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Make.Contains(make) && x.Color.Contains(color) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make == null && model != null && color != null && year != 0)//make is not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Make.Contains(make) && x.Color.Contains(color) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make != null && model == null && color != null && year != 0)//model is not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Color.Contains(color) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make != null && model != null && color == null && year != 0)//color is not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Make.Contains(make) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make != null && model != null && color != null && year == 0)//year is not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Make.Contains(make) && x.Color.Contains(color)).ToListAsync();
        //}
        //else if (make == null && model == null && color != null && year != 0)//Make and model not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Color.Contains(color) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make == null && model != null && color == null && year != 0)//make and color not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make == null && model != null && color != null && year == 0)//make and year not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Color.Contains(color)).ToListAsync();
        //}
        //else if (make != null && model == null && color == null && year != 0)//model and color not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Make.Contains(make) && x.Year.ToString().Contains(yr)).ToListAsync();
        //}
        //else if (make != null && model == null && color != null && year == 0)//model and year not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Make.Contains(make) && x.Color.Contains(color)).ToListAsync();
        //}
        //else if (make != null && model != null && color == null && year == 0)//year and color not filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model) && x.Make.Contains(make)).ToListAsync();
        //}
        //else if (make != null && model == null && color == null && year == 0)//make filled
        //{
        //    inventory = _context.Cars.Where(x => x.Make.Contains(make)).ToListAsync();
        //}
        //else if (make == null && model != null && color == null && year == 0)//model filled
        //{
        //    inventory = _context.Cars.Where(x => x.Model.Contains(model)).ToListAsync();
        //}
        //else if (make == null && model == null && color != null && year == 0)//color filled
        //{
        //    inventory = _context.Cars.Where(x => x.Color.Contains(color)).ToListAsync();
        //}
        //else //Year filled
        //{
        //    inventory =  _context.Cars.Where(x => x.Year.ToString().Contains(yr)).ToListAsync();
        //}

        //if(inventory.ToString() ==  null)
        //{
        //    return NotFound();
        //}
        //else
        //{
        //    return await inventory;
        //}


        //}



        [HttpGet("model/{model}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetModel(string model)
        {

            int matches = _context.Cars.Count(x => x.Model.Contains(model));

            if (matches > 0)
            {
                return await _context.Cars.Where(x => x.Model.Contains(model)).ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("make/{make}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetMake(string make)
        {

            int matches = _context.Cars.Count(x => x.Make.Contains(make));

            if (matches > 0)
            {
                return await _context.Cars.Where(x => x.Make.Contains(make)).ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("color/{color}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetColor(string color)
        {

            int matches = _context.Cars.Count(x => x.Color.Contains(color));

            if (matches > 0)
            {
                return await _context.Cars.Where(x => x.Color.Contains(color)).ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("year/{year}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetYear(string year)
        {

            int matches = _context.Cars.Count(x => x.Year.ToString().Contains(year));

            if (matches > 0)
            {
                return await _context.Cars.Where(x => x.Year.ToString().Contains(year)).ToListAsync();
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpGet("Search/{model}&&{make}&&{color}&&{year}")]
        //public async Task<ActionResult<IEnumerable<Car>>> SearchCombo(string model = null, string make = null, string color = null, string year = null)
        //{

        //    int matches = _context.Cars.Count(x => x.Model.Contains(model) && x.Make.Contains(make) && x.Color.Contains(color) && x.Year.ToString().Contains(year));

        //    if (matches > 0)
        //    {
        //        return await _context.Cars.Where(x => x.Year.ToString().Contains(year)).ToListAsync();
        //    }
        //    else /*if (_context.Cars.Count(x => x.Model.Contains(model) && x.Make.Contains(make) && x.Color.Contains(color) && x.Year.ToString().Contains(year)) == 0)*/
        //    {
        //        return NotFound();
        //    }
        //}


        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
