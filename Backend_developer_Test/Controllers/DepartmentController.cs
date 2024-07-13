using Backend_developer_Test.Models;
using Backend_developer_Test.Models.Database;
using Backend_developer_Test.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Backend_developer_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("insert")]
        public async Task<APIResponse> Insert(Department data) => await _departmentService.Create(data);

        [HttpPost("GetAllDepartment")]
        public async Task<APIResponse> Get() => await _departmentService.Get();

        [HttpGet("GetDepartmentByID")]
        public async Task<APIResponse> GetDeptByID(int id) => await _departmentService.GetDeptById(id);

        [HttpPut("UpdateDepartmentByID")]
        public async Task<APIResponse> UpdateDeptByID(Department data, int id) => await _departmentService.UpdateDeptById(data, id);

        [HttpDelete("DeleteDepartmentByID")]
        public async Task<APIResponse> DeleteDeptByID(Department data, int id) => await _departmentService.DeleteDeptById(data, id);


    }
}
