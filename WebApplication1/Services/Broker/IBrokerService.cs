using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Broker;

namespace WebApplication1.Services.Broker;

public interface IBrokerService
{
    public Task<ResponseDto<ResponseBroker>> GetBroker();
}