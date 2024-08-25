using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PrintMaster.Application.Payloads.RequestModels.DesignRequests
{
    public class Request_UpdateDesign
    {
        [DataType(DataType.Upload)]
        public IFormFile DesignImage { get; set; }
    }
}
