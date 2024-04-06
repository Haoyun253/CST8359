using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab6.Data;
using Lab6.Models;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // returned when we return list of Students successfully
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an error in processing the request
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // returned when we return a student successfully
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // returned when parameters are incorrect
        [ProducesResponseType(StatusCodes.Status404NotFound)] // returned when we cannot find the student with specified id
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an internal error
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // returned when we update the student successfully
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // returned when parameters are incorrect or the two parameters mismatch
        [ProducesResponseType(StatusCodes.Status404NotFound)] // returned when we cannot find the student with specified id
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an internal error
        public async Task<IActionResult> PutStudent(Guid id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest("Student ID in the URL does not match the ID in the request body.");
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                _context.Entry(student).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound("Student not found.");
                }
                else
                {
                    throw;
                }
            }

            return Ok(student);
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // returned when student is created successfully
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // returned when parameters are incorrect
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an internal error
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // returns when student is successfully deleted
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // returned when parameters are incorrect
        [ProducesResponseType(StatusCodes.Status404NotFound)] // returned when we cannot find the student with specified id
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // returned when there is an internal error
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(Guid id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
