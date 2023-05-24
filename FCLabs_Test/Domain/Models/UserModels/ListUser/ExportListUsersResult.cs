namespace Domain.Models.UserModels.ListUser
{
    public class ExportListUsersResult
    {
        public MemoryStream Stream { get; set; }
        public string Headers { get; set; }
        public string FileName { get; set; }
    }
}