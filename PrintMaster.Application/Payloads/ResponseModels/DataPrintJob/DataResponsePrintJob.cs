namespace PrintMaster.Application.Payloads.ResponseModels.DataPrintJob
{
    public class DataResponsePrintJob : DataResponseBase
    {
        public Guid DesignId { get; set; }
        public string DesignImage { get; set; } = string.Empty;
        public string PrintJobStatus { get; set; } = string.Empty;
        public IQueryable<string> ResourceForPrints { get; set; }   
    }
}
