using PrintMaster.Application.Payloads.ResponseModels.DataResource;
using PrintMaster.Domain.Entities;

namespace PrintMaster.Application.Payloads.Mappers
{
    public class ResourcePropertyDetailConverter
    {
        public DataResponseResourcePropertyDetail EntityToDTO(ResourcePropertyDetail detail)
        {
            return new DataResponseResourcePropertyDetail
            {
                Id = detail.Id,
                Name = detail.Name,
                Price = detail.Price,
                Image = detail.Image,
                Quantity = detail.Quantity
            };
        }
    }
}
