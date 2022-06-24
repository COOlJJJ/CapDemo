using System;
using System.Collections.Generic;
using System.Text;

namespace CapStockRespository.Respository
{
    public interface ICapStockRespository
    {
        int UpdateStock(string productNo, int count);
    }
}
