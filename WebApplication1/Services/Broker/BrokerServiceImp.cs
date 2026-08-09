using WebApplication1.Data.DTO;
using WebApplication1.Data.DTO.Broker;
using WebApplication1.Repositories.broker;
using WebApplication1.Util;

namespace WebApplication1.Services.Broker;


public class BrokerServiceImp : IBrokerService
{
    private readonly IBrokerRepository _brokerRepository;
    public BrokerServiceImp(IBrokerRepository brokerRepository)
    {
        _brokerRepository = brokerRepository;
    }
    public async Task<ResponseDto<ResponseBroker>> GetBroker()
    {
        try
        {
            var response = (await _brokerRepository.GetBrokerAsync());
            if (response is null)
            {
                return new ResponseDto<ResponseBroker>
                {
                    responseCode = Main.code400,
                    responseMessage = "No broker found!",
                    data = null
                };
            }
        
            var lsitBroker = response.Select(res => new ResponseBroker
            {
                brokerId = res.Id.ToString(),
                brokerName = res.Name ?? string.Empty,
                brokerDescription = res.Note ?? string.Empty,
                brokerImage = res.ImageUrl ?? string.Empty
            }).ToList();

            return new ResponseDto<ResponseBroker>()
            {
                responseCode = Main.code200,
                responseMessage = "Get broker successfully!",
                data = null,
                dataList = lsitBroker
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<ResponseBroker>()
            {
                responseCode = Main.code500,
                responseMessage = $"An error occurred while retrieving broker: {ex.Message}",
                data = null
            };
        }
    }
}