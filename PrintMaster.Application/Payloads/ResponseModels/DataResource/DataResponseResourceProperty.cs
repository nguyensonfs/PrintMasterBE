namespace PrintMaster.Application.Payloads.ResponseModels.DataResource
{
    public class DataResponseResourceProperty : DataResponseBase
    {
        public string ResourcePropertyName { get; set; }
        public int Quantity { get; set; }
        public IQueryable<DataResponseResourcePropertyDetail>? ResourcePropertyDetails { get; set; }
    }
}
