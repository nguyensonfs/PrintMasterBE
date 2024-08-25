using PrintMaster.Application.Payloads.ResponseModels.DataUser;

namespace PrintMaster.Application.Payloads.ResponseModels.DataLogin
{
    public class DataResponseLogin : DataResponseBase
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DataResponseUser User { get; set; }
    }
}
