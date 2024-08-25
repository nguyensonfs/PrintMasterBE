namespace PrintMaster.Application.Payloads.ResponseModels.DataNotification
{
    public class DataResponseNotification : DataResponseBase
    {
        public string Content { get; set; }
        public string Link { get; set; }
        public bool? IsSeen { get; set; }
    }
}
