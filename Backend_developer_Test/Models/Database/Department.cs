namespace Backend_developer_Test.Models.Database
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }

    }

    public class SubDepartments
    {
        public int Id { get; set; }
        public int DeptId { get; set; }
        public int SubDepartmentId { get; set; }

    }

    public class SubDepDTO
    {
        public int DepId { get; set; }
        public int SubDeptId { get; set; }
    }

   public class ResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        public List<Department> SubDepartments { get; set; } = new List<Department>();
    }
}
