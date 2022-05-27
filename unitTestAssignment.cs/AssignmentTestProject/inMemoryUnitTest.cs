using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using unitTestAssignment.cs.Controllers;
using unitTestAssignment.cs.Models;

namespace AssignmentTestProject
{
    [TestFixture]
    public class Tests
    {
        private AppDataDbContext _context;
        private AppDataDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDataDbContext>();
            options.UseInMemoryDatabase("DatabaseName: Employees");
            var context = new AppDataDbContext(options.Options);
            context.Departments.Add(new Department { DepartmentId = 1, DepartmentName = "Hr" });
            context.Departments.Add(new Department { DepartmentId = 2, DepartmentName = "Devs" });
            context.Employees.Add(new Employee { EmployeeId = 1, Age = 22, Name = "Darshan",departmentId=1 });
            context.Employees.Add(new Employee { EmployeeId = 2, Age = 20, Name = "Abhishek", departmentId = 2 });
            context.Employees.Add(new Employee { EmployeeId = 3, Age = 23, Name = "Abhi", departmentId = 1 });
            context.SaveChanges();
            return context;
            
        }

        [SetUp]
        public void Setup()
        {
            _context = GetContext();
            _context.Database.EnsureCreated();
        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }
        [Test]
        public async Task Get_whenCalled_ShouldReturnTwoRecords()
        {
            EmsController ems = new EmsController(_context);
            var data = await ems.Get();
            Assert.IsInstanceOf<OkObjectResult>(data);
        }
        [Test]
        public async Task GetEmployeeById_whenValidIdPassed_ShouldReturnRecord()
        {
            EmsController ems = new EmsController(_context);
            var data = await ems.GetEmployeeById("1");
            Assert.IsInstanceOf < OkObjectResult>(data);
        }

        [Test]
        public async Task GetEmployeeById_whenInvalidIdPassed_ShouldReturnBadeRequest()
        {
            EmsController ems = new EmsController(_context);
            var data = await ems.GetEmployeeById("100");
            Assert.IsInstanceOf<NotFoundResult>(data);
        }
        [Test]
        public async Task Post_whenNewEmployeeWithValidDepartmentSend_ShouldAddEmployee()
        {
            EmsController ems = new EmsController(_context);
            var employee = new Employee
            {
                EmployeeId = 4,
                Age = 21,
                Name = "Gaurang",
                departmentId = 1
            };
            var data = await ems.Post(employee);
            Assert.IsInstanceOf<CreatedResult>(data);
        }

        [Test]
        public async Task Post_whenExsistingEmployeeWithValidDepartmentSend_ShouldThrowError()
        {
            EmsController ems = new EmsController(_context);
            var employee = new Employee
            {
                EmployeeId = 1,
                Age = 22,
                Name = "Darshan",
                departmentId = 1
            };
            var data = await ems.Post(employee);
            Assert.IsInstanceOf<BadRequestResult>(data);
        }
        [Test]
        public async Task Post_whenNewEmployeeWithInvalidDepartmentSend_ShouldThrowError()
        {
            EmsController ems = new EmsController(_context);
            var employee = new Employee
            {
                EmployeeId = 6,
                Age = 22,
                Name = "Dhaval",
                departmentId = 11
            };
            var data = await ems.Post(employee);
            Assert.IsInstanceOf<BadRequestResult>(data);
        }
        [Test]

        public void GetEmployeeCountByDepartment_whenCalled_ShouldReturn2Value()
        {
            EmsController ems = new EmsController(_context);
            var data = ems.GetEmployeeCountByDepartment(1);
            Assert.That(data, Is.EqualTo(2));

        }
    }
}