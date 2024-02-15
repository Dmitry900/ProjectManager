using Microsoft.AspNetCore.Mvc;
using ProjectManager.Core.Models;
using ProjectManager.Core.Services;

namespace ProjectManager.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        readonly IEmployeeService employeeService = employeeService;

        CancellationToken CancellationToken => HttpContext.RequestAborted;

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetEmployeesAsync(int skip, int take)
        {
            return Ok(await employeeService.GetEmployeesAsync(skip, take, CancellationToken));
        }

        // GET: api/Employees/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployeeAsync(Guid id)
        {
            var employee = await employeeService.FindByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> CreateEmployeeAsync(EmployeeModel employee)
        {
            try
            {
                await employeeService.CreateAsync(employee, CancellationToken);
            }
            catch
            {
                return BadRequest();
            }


            return employee;
        }

        // PUT: api/Employees
        [HttpPut]
        public async Task<IActionResult> PutEmployeeAsync(EmployeeModel employee)
        {
            try
            {
                await employeeService.UpdateEmployeeAsync(employee, CancellationToken);
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Employees/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync(Guid id)
        {
            try
            {
                await employeeService.DeleteAsync(id, CancellationToken);
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}