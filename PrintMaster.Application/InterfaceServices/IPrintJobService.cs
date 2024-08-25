using PrintMaster.Application.Payloads.RequestModels.PrintJobRequests;
using PrintMaster.Application.Payloads.ResponseModels.DataPrintJob;
using PrintMaster.Application.Payloads.Responses;

namespace PrintMaster.Application.InterfaceServices
{
    public interface IPrintJobService
    {
        Task<ResponseObject<DataResponsePrintJob>> CreatePrintJob(Request_CreatePrintJob request);
        Task<ResponseObject<DataResponsePrintJob>> ConfirmDonePrintJob(Guid printJobId);
        Task<IQueryable<DataResponsePrintJob>> GetAllPrintJobs();
        Task<ResponseObject<DataResponsePrintJob>> GetPrintJobById(Guid printJobId);
    }
}
