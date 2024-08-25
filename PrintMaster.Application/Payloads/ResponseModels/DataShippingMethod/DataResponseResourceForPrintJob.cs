using PrintMaster.Application.Payloads.ResponseModels.DataResource;

namespace PrintMaster.Application.Payloads.ResponseModels.DataShippingMethod
{
    public class DataResponseResourceForPrintJob : DataResponseBase
    {
        public DataResponseResourcePropertyDetail Resource { get; set; }
        public int Quantity { get; set; }
    }
}
