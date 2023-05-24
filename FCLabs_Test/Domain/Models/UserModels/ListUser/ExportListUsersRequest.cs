using Entities.Enums;

namespace Domain.Models.UserModels.ListUser
{
    public class ExportListUsersRequest : ListUsersRequest
    {
        public ExportEnum ExportFormat { get; set; }
    }
}