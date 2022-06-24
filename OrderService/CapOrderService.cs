using OrderRespository.Respository;
using System;

namespace OrderService
{
    public class CapOrderService : ICapOrderService
    {
        private readonly ICapOrderRespository _capOrderRespository;

        public CapOrderService(ICapOrderRespository capOrderRespository)
        {
            _capOrderRespository = capOrderRespository;
        }

        public int GenerateOrder()
        {
            // 可以做其他业务判断

            // 生成订单记录
            return _capOrderRespository.AddOrder();
        }
    }
}
