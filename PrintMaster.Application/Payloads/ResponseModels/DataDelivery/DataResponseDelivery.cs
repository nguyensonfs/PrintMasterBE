using PrintMaster.Application.Payloads.ResponseModels.DataCustomer;
using PrintMaster.Application.Payloads.ResponseModels.DataProject;
using PrintMaster.Application.Payloads.ResponseModels.DataUser;

namespace PrintMaster.Application.Payloads.ResponseModels.DataDelivery
{
    public class DataResponseDelivery : DataResponseBase
    {
        public string ShippingMethodName { get; set; }
        public DataResponseCustomer Customer { get; set; }
        public DataResponseUser Deliver { get; set; }
        public DataResponseProject Project { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime EstimateDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public string DeliveryStatus { get; set; }
    }
}
