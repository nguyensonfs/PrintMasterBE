using PrintMaster.Application.Payloads.ResponseModels.DataUser;

namespace PrintMaster.Application.Payloads.ResponseModels.DataTeam
{
    public class DataResponseTeam : DataResponseBase
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? NumberOfMember { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string ManagerName { get; set; } = string.Empty;
        public IQueryable<DataResponseUser>? Users { get; set; }
    }
}
