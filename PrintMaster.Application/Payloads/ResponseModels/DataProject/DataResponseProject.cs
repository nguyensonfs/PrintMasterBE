using PrintMaster.Application.Payloads.ResponseModels.DataDesign;
using PrintMaster.Commons.Enumerates;

namespace PrintMaster.Application.Payloads.ResponseModels.DataProject
{
    public class DataResponseProject : DataResponseBase
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string RequestDescriptionFromCustomer { get; set; }
        public DateTime StartDate { get; set; }
        public string Leader { get; set; }
        public string PhoneLeader { get; set; }
        public string EmailLeader { get; set; }
        public Guid PrintJobId { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public string Customer { get; set; }
        public Guid CustomerId { get; set; }
        public string PhoneCustomer { get; set; }
        public string EmailCustomer { get; set; }
        public string AddressCustomer { get; set; }
        public decimal CommissionPercentage { get; set; }
        public string EmployeeCreateName { get; set; }
        public string ImageDescription { get; set; }
        public decimal StartingPrice { get; set; }
        public double? Progress { get; set; }
        public string ProjectStatus { get; set; }
        public IQueryable<DataResponseDesign> Designs { get; set; }
    }
}
