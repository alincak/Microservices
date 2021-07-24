using FreeCource.API.Basket.Dtos;
using StackExchange.Redis;
using System.Text.Json;
using System.Threading.Tasks;

namespace FreeCource.API.Basket.Services
{
  public class BasketService : IBasketService
  {
    private readonly IRedisService _redisService;
    private readonly IDatabase _db;

    public BasketService(IRedisService redisService)
    {
      _redisService = redisService;
      _db = _redisService.GetDb();
    }

    public async Task<bool> Delete(string userId)
    {
      return await _db.KeyDeleteAsync(userId);
    }

    public async Task<BasketDto> Get(string userId)
    {
      var basket = await _db.StringGetAsync(userId);
      if (string.IsNullOrEmpty(basket))
      {
        return null;
      }

      return JsonSerializer.Deserialize<BasketDto>(basket);
    }

    public async Task<bool> SaveOrUpdate(BasketDto basketDto)
    {
      return await _db
        .StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
    }
  }
}
