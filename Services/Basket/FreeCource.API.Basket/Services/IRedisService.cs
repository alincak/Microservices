using StackExchange.Redis;

namespace FreeCource.API.Basket.Services
{
  public interface IRedisService
  {
    public IDatabase GetDb(int db = 1);
  }
}
