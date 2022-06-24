using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CapStockRespository.Respository
{
    public class CapStockRespository : ICapStockRespository
    {
        private readonly StockDbContext stockDbContext;

        public CapStockRespository(StockDbContext stockDbContext)
        {
            this.stockDbContext = stockDbContext;
        }

        public int UpdateStock(string productNo,int count)
        {
            // 更新库存
            var pro = stockDbContext.Stock.Where(s => s.ProductNo == productNo).FirstOrDefault();
            pro.StockCount = pro.StockCount - count;
            stockDbContext.Update(pro);
            int res = stockDbContext.SaveChanges();
            return res;
        }
    }
}
