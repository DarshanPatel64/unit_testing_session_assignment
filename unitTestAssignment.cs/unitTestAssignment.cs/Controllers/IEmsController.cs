using Microsoft.AspNetCore.Mvc;
using unitTestAssignment.cs.Models;

namespace unitTestAssignment.cs.Controllers
{
    public interface IEmsController
    {
        IEnumerable<Employee> Get();
        IActionResult GetEmployeeById(string id);
        int GetEmployeeCount();
        IActionResult Post([FromBody] Employee emp);
    }
}