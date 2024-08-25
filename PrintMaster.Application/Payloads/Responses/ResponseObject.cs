namespace PrintMaster.Application.Payloads.Responses
{
    public class ResponseObject<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
