using FreeCource.API.Order.Application.Commands;
using FreeCource.API.Order.Application.Dtos;
using FreeCource.API.Order.Application.Mapping;
using FreeCource.API.Order.Application.Queries;
using FreeCourse.Shared.BaseController;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCource.API.Order.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrdersController : CustomBaseController
  {
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
      _mediator = mediator;
      _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
      var orders = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

      if (!orders.Any())
      {
        return CreateResponse(Response<List<OrderDto>>.Success(new List<OrderDto>(), 200));
      }

      var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
      return CreateResponse(Response<List<OrderDto>>.Success(ordersDto, 200));
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
    {
      var response = await _mediator.Send(createOrderCommand);

      return CreateResponse(Response<CreatedOrderDto>.Success(response, 200));
    }
  }
}
