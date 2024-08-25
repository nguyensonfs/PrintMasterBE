namespace PrintMaster.Application.Payloads.ResponseModels.DataResource
{
    public class DataResponseResourceType : DataResponseBase
    {
        public string NameOfResourceType { get; set; }
        public IQueryable<DataResponseResource> Resources { get; set; }
    }
}
