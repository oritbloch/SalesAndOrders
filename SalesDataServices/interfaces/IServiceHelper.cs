using System.Collections.Generic;

namespace SalesDataServices
{
    public interface IServiceHelper
    {
        public List<SaleDetails> GetNumOfSales(string orderBy, string orderDir);
        public List<OrderTimes> GetOrderTimes(string beforeOrAfterTime);
    }
}
