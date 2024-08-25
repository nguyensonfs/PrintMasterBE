namespace PrintMaster.Application.Payloads.ResponseModels.DataResource
{
    public class DataResponseResource : DataResponseBase
    {
        public string ResourceName { get; set; }
        public string ResourceTypeName { get; set; }
        public int AvailableQuantity { get; set; }
        public string Image { get; set; }
        public string ResourceStatus { get; set; }
        public IQueryable<DataResponseResourceProperty>? ResourceProperties { get; set; }
    }
}
