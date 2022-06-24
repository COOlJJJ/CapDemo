using CapStockRespository;
using CapStockRespository.Models;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStockDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private StockDbContext _stockDbContext;
        private readonly ICapPublisher _capPublisher;

        public StockController(StockDbContext stockDbContext, ICapPublisher capPublisher)
        {
            _stockDbContext = stockDbContext;
            _capPublisher = capPublisher;
        }

        [NonAction]
        
        [CapSubscribe("Order.Create.Success")]
        public void UpdateStock(OrderEntity order)
        {
            //throw new Exception("扣减库存异常了~~~");
            // 为了测试，库存里面没有数据的话，先模拟一条数据
            bool bHaveData = _stockDbContext.Stock.Any();
            if(!bHaveData)
            {
                StockEntity stock = new StockEntity
                {
                    Id = Guid.NewGuid(),
                    ProductNo = "Product001",
                    StockCount = 100,
                    UpdateDate = DateTime.Now
                };
                _stockDbContext.Stock.Add(stock);
                _stockDbContext.SaveChanges();
            }
            // 模拟扣减库存
            using var trans = _stockDbContext.Database.BeginTransaction(_capPublisher, autoCommit: false);
            try
            {
                var product = _stockDbContext.Stock.Where(s => s.ProductNo == order.ProductNo).FirstOrDefault();
                product.StockCount = product.StockCount - order.Count;

                _stockDbContext.Update(product);
                _stockDbContext.SaveChanges();
                // 可以继续向下发布流程，比如库存扣减成功，下一步到物流服务进行相关处理，可以继续发布消息
                // _capPublisher.Publish();
                trans.Commit();

                Console.WriteLine(order.OrderNo);
            }
            catch (Exception ex)
            {
                trans.Rollback();
            }

        }
    }
}
