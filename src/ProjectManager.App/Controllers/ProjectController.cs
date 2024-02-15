using Microsoft.AspNetCore.Mvc;
using ProjectManager.Core.Models;
using ProjectManager.Core.Services;

namespace ProjectManager.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService) : ControllerBase
    {
        readonly IProjectService projectService = projectService;

        CancellationToken CancellationToken => HttpContext.RequestAborted;

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectModel>>> GetProjectsAsync(int skip, int take, ProjectOrderProperty orderProperty = ProjectOrderProperty.None, Order order = Order.Ascending)
        {
            return Ok(await projectService.GetProjectsAsync(skip, take, orderProperty, order, CancellationToken));
        }

        // GET: api/Projects/00000000-0000-0000-0000-000000000000
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectModel>> GetProjectAsync(Guid id)
        {
            var project = await projectService.FindByIdAsync(id, CancellationToken);

            if (project == null)
                return NotFound();

            return project;
        }

        // GET: api/Projects/00000000-0000-0000-0000-000000000000/Employees
        [HttpGet("{projectId}/Employees")]
        public async Task<ActionResult<IEnumerable<EmployeeModel>>> GetProjectEmployeesAsync(Guid projectId)
        {
            var employees = await projectService.GetProjectsEmployeesAsync(projectId, CancellationToken);

            if (employees == null)
                return NotFound();

            return Ok(employees);
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<ProjectModel>> PostProjectAsync(ProjectModel project)
        {
            try
            {
                await projectService.CreateAsync(project, CancellationToken);
            }
            catch
            {
                return BadRequest();
            }


            return project;
        }

        // POST: api/Projects
        [HttpPost("{projectId}/Employees/Add/{employeeId}")]
        public async Task<ActionResult> AddToProjectAsync(Guid projectId, Guid employeeId)
        {
            try
            {
                await projectService.AddEmployeeAsync(projectId, employeeId, CancellationToken);
            }
            catch
            {
                return BadRequest();
            }


            return Ok();
        }

        // POST: api/Projects
        [HttpPost("{projectId}/Employees/Remove/{employeeId}")]
        public async Task<ActionResult> RemoveFromProjectAsync(Guid projectId, Guid employeeId)
        {
            try
            {
                await projectService.RemoveEmployeeAsync(projectId, employeeId, CancellationToken);
            }
            catch
            {
                return BadRequest();
            }


            return Ok();
        }

        // PUT: api/Projects/00000000-0000-0000-0000-000000000000
        [HttpPut]
        public async Task<IActionResult> PutProjectAsync(ProjectModel project)
        {
            try
            {
                await projectService.UpdateProjectAsync(project, CancellationToken);
            }
            catch
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Projects/00000000-0000-0000-0000-000000000000
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjectAsync(Guid id)
        {
            try
            {
                await projectService.DeleteAsync(id, CancellationToken);
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}