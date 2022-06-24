using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using OrderRespository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderRespository.Respository
{
    public class CapOrderRespository : ICapOrderRespository
    {
        private readonly OrderDbContext _context;
        private readonly ICapPublisher _capPublisher;

        public CapOrderRespository(OrderDbContext context, ICapPublisher capPublisher = null)
        {
            this._context = context;
            _capPublisher = capPublisher;
        }

        public int AddOrder()
        {
            OrderEntity orderEntity = new OrderEntity
            {
                Id = Guid.NewGuid(),
                OrderNo = "test" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                ProductName = "商品",
                ProductNo = "Product001",
                Count = 90,
                CreateDate = DateTime.Now
            };
            // 开启本地事务
            using var trans = _context.Database.BeginTransaction(this._capPublisher, autoCommit: false);
            try
            {
                // 新增订单
                _context.Orders.Add(orderEntity);
                // 发布消息，这个时候同时也保存了本地消息信息
                _capPublisher.Publish("Order.Create.Success", orderEntity);
                _context.SaveChanges();
                // 提交事务
                trans.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                return 0;
            }
        }
    }
}
