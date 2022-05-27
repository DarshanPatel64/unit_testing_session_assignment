using Microsoft.AspNetCore.Mvc;
using Moq;
using unitTestAssignment.cs.Models;


namespace unitTestAssignment.cs.Controllers
{
    public class EmsController : Controller
    {
        private AppDataDbContext _context;


        public EmsController(AppDataDbContext context)
        {
            _context = context;
        }
        private bool validateEmployee(Employee emp)
        {
            var data = _context.Employees.Find(emp.EmployeeId);
            var dep = _context.Departments.Find(emp.departmentId);

            return (data == null && dep != null) ? true : false;

        }
        public int GetEmployeeCountByDepartment(int id)
        {
            return _context.Employees.Where(e => e.departmentId == id).ToList().Count;
        }

        [Route("api/employee")]
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var data = _context.Employees;
            return await Task.FromResult<OkObjectResult>(Ok(data));
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            int _id = int.Parse(id);
            var employee = await _context.Employees.FindAsync(_id);
            if (employee == null) return await Task.FromResult<NotFoundResult>(NotFound());
            else return await Task.FromResult<OkObjectResult>(Ok(employee));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee emp)
        {
            if (validateEmployee(emp))
            {
                await _context.Employees.AddAsync(emp);
                await _context.SaveChangesAsync();
                return await Task.FromResult<CreatedResult>(Created("api/Employees", emp));

            }
            else
            {
                return await Task.FromResult<BadRequestResult> (BadRequest());
            }
        }
    }
}
