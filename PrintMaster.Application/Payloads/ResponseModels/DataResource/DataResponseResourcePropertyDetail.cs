namespace PrintMaster.Application.Payloads.ResponseModels.DataResource
{
    public class DataResponseResourcePropertyDetail : DataResponseBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}
