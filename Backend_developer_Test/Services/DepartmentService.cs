using Azure;
using Backend_developer_Test.Helper;
using Backend_developer_Test.Models;
using Backend_developer_Test.Models.Database;

namespace Backend_developer_Test.Services
{
    public interface IDepartmentService 
    {
        Task<APIResponse> Create(Department data);
        Task<APIResponse> Get();
        Task<APIResponse> GetDeptById(int id);
        Task<APIResponse> UpdateDeptById(Department data, int id);
        Task<APIResponse> DeleteDeptById(Department data, int id);

    }
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDBContext _context;
        public DepartmentService(AppDBContext context)
        {
            _context = context;
        }


        public async Task<APIResponse> Create(Department data)
        {
            var resp = new APIResponse();
            try
            {

                data.Name = AesEncryption.EncryptString(data.Name);
                _context.Departments.Add(data);
                await _context.SaveChangesAsync();

                resp.HttpResponseCode = System.Net.HttpStatusCode.OK;
                resp.CustomResponseCode = "200 OK";
                resp.Message = "Insert in Department Successfully";
                resp.Result = data.Id;
                resp.Success = true;
                resp.Count = 0;
                return resp;
            }
            catch (Exception ex)
            {
                resp.HttpResponseCode = System.Net.HttpStatusCode.BadRequest;
                resp.CustomResponseCode = "400 BadRequest";
                resp.Message = "Exception" + ex;
                resp.Result = null;
                resp.Success = false;
                return resp;
            }
        }

        public async Task<APIResponse> CreateSubDepartment(SubDepDTO data)
        {
            var resp = new APIResponse();
            try
            {

                var model = new SubDepartments
                {
                    DeptId = data.DepId,
                    SubDepartmentId = data.SubDeptId
                };
               // data.Name = AesEncryption.EncryptString(data.Name);
                 
                _context.SubDeprtments.Add(model);
                await _context.SaveChangesAsync();

                resp.HttpResponseCode = System.Net.HttpStatusCode.OK;
                resp.CustomResponseCode = "200 OK";
                resp.Message = "Insert in Department Successfully";
                resp.Result = model.Id;
                resp.Success = true;
                resp.Count = 0;
                return resp;
            }
            catch (Exception ex)
            {
                resp.HttpResponseCode = System.Net.HttpStatusCode.BadRequest;
                resp.CustomResponseCode = "400 BadRequest";
                resp.Message = "Exception" + ex;
                resp.Result = null;
                resp.Success = false;
                return resp;
            }
        }


        public async Task<APIResponse> Get()
        {
            var resp = new APIResponse();
            try
            {
                var dept = _context.Departments.ToList();
                var subDept = _context.SubDeprtments.ToList();
                var responses = new List<ResponseDTO>();
                foreach (var item in dept)
                {
                    var response = new ResponseDTO();
                    response.Name = item.Name;
                    response.Id = item.Id;
                    response.ParentDepartmentId = item.ParentDepartmentId;
                    var subDepts = dept.Where(d => subDept.Where(s => s.DeptId == item.Id).Select(x => x.SubDepartmentId).Contains(d.Id));
                    foreach (var sub in subDepts)
                    {
                        response.SubDepartments.Add(sub);

                    }
                    responses.Add(response);
                   
                }
                resp.HttpResponseCode = System.Net.HttpStatusCode.OK;
                resp.CustomResponseCode = "200 OK";
                resp.Message = "Get  Successfully";
                resp.Result = responses;
                resp.Success = true;
                resp.Count = 0;
                return resp;
            }
            catch (Exception ex)
            {
                resp.HttpResponseCode = System.Net.HttpStatusCode.BadRequest;
                resp.CustomResponseCode = "400 BadRequest";
                resp.Message = "Exception" + ex;
                resp.Result = null;
                resp.Success = false;
                return resp;
            }
        }

        public async Task<APIResponse> UpdateDeptById(Department data, int id)
        {
            var resp = new APIResponse();
            try
            {
                var dept = _context.Departments.Where(s => s.Id == id).FirstOrDefault();
                dept.Name = data.Name;
                dept.ParentDepartmentId = data.ParentDepartmentId;

                _context.Update(dept);
                _context.SaveChanges();

                resp.HttpResponseCode = System.Net.HttpStatusCode.OK;
                resp.CustomResponseCode = "200 OK";
                resp.Message = "Get  Successfully";
                resp.Result = dept;
                resp.Success = true;
                resp.Count = 0;
                return resp;
            }
            catch (Exception ex)
            {
                resp.HttpResponseCode = System.Net.HttpStatusCode.BadRequest;
                resp.CustomResponseCode = "400 BadRequest";
                resp.Message = "Exception" + ex;
                resp.Result = null;
                resp.Success = false;
                return resp;
            }
        }

        public async Task<APIResponse> DeleteDeptById(Department data, int id)
        {
            var resp = new APIResponse();
            try
            {
                var dept = _context.Departments.Where(s => s.Id == id).FirstOrDefault();
               

                _context.Remove(dept);
                _context.SaveChanges();

                resp.HttpResponseCode = System.Net.HttpStatusCode.OK;
                resp.CustomResponseCode = "200 OK";
                resp.Message = "Get  Successfully";
                resp.Result = dept;
                resp.Success = true;
                resp.Count = 0;
                return resp;
            }
            catch (Exception ex)
            {
                resp.HttpResponseCode = System.Net.HttpStatusCode.BadRequest;
                resp.CustomResponseCode = "400 BadRequest";
                resp.Message = "Exception" + ex;
                resp.Result = null;
                resp.Success = false;
                return resp;
            }
        }

        public async Task<APIResponse> GetDeptById(int id)
        {
            var resp = new APIResponse();
            try
            {
                var dept = _context.Departments.Where(x => x.Id == id).FirstOrDefault();
                var subDept = _context.Departments.Where(x => _context.SubDeprtments.Where(y => y.DeptId == dept.Id).Select(s => s.SubDepartmentId).Contains(x.Id));
                //var responses = new List<ResponseDTO>();
                
                    var response = new ResponseDTO();
                    response.Name = dept.Name;
                    response.Id = dept.Id;
                    response.ParentDepartmentId = dept.ParentDepartmentId;
                    //var subDepts = dept.Where(d => subDept.Where(s => s.DeptId == item.Id).Select(x => x.SubDepartmentId).Contains(d.Id));
                    
                    foreach (var sub in subDept)
                    {
                        response.SubDepartments.Add(sub);

                    }
                resp.HttpResponseCode = System.Net.HttpStatusCode.OK;
                resp.CustomResponseCode = "200 OK";
                resp.Message = "Get  Successfully";
                resp.Result = response;
                resp.Success = true;
                resp.Count = 0;
                return resp;
            }
            catch (Exception ex)
            {
                resp.HttpResponseCode = System.Net.HttpStatusCode.BadRequest;
                resp.CustomResponseCode = "400 BadRequest";
                resp.Message = "Exception" + ex;
                resp.Result = null;
                resp.Success = false;
                return resp;
            }
        }
    }
}
