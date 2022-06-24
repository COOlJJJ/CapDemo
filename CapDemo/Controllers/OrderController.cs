using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using OrderRespository;
using OrderRespository.Models;
using OrderService;
using System.Threading.Tasks;

namespace CapOderDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext dbContext;

        public OrderController(OrderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// 可以用外部传参的数据
        /// [FromForm] OrderEntity orderEntity
        /// </summary>
        /// <param name="capOrderService"></param>
        /// <param name="capPublisher"></param>
        /// <returns></returns>
        [HttpPost("GenerateOrder")]
        public async Task<IActionResult> GenerateOrder([FromServices] ICapOrderService capOrderService,
            [FromServices] ICapPublisher capPublisher)
        {
            capOrderService.GenerateOrder();
            return Ok("订单生成成功");
        }
    }
}
