using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCource.API.Discount.Services
{
  public class DiscountService : IDiscountService
  {
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _dbConnection;

    public DiscountService(IConfiguration configuration)
    {
      _configuration = configuration;

      _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
    }

    public async Task<bool> Delete(int id)
    {
      var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

      return status > 0;
    }

    public async Task<IList<Models.Discount>> GetAll()
    {
      var list = await _dbConnection.QueryAsync<Models.Discount>("Select * from discount");
      return list?.ToList();
    }

    public async Task<Models.Discount> GetByCodeAndUserId(string code, string userId)
    {
      var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userid=@UserId and code=@Code", new { UserId = userId, Code = code });

      return discounts?.FirstOrDefault();
    }

    public async Task<Models.Discount> GetById(int id)
    {
      return (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id = id }))
        .SingleOrDefault();
    }

    public async Task<bool> Save(Models.Discount discount)
    {
      var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);

      return saveStatus > 0;
    }

    public async Task<bool> Update(Models.Discount discount)
    {
      var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });

      return status > 0;
    }
  }
}
