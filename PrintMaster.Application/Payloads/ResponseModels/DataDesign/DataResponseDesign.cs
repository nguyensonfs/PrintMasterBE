namespace PrintMaster.Application.Payloads.ResponseModels.DataDesign
{
    public class DataResponseDesign : DataResponseBase
    {
        public string Designer { get; set; }
        public string DesignImage { get; set; }
        public DateTime DesignTime { get; set; }
        public string DesignStatus { get; set; }
        public string? Approver { get; set; } // Người duyệt thiết kế
    }
}
