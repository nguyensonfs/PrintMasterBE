namespace PrintMaster.Application.Payloads.ResponseModels.DataCustomer
{
    public class DataResponseCustomer : DataResponseBase
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
