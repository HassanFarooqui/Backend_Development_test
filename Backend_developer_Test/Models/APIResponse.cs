namespace Backend_developer_Test.Models
{
    public class APIResponse
    {
        public object Result { get; set; }
        public System.Net.HttpStatusCode HttpResponseCode { get; set; }
        public string CustomResponseCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int Count { get; set; }
    }
}
