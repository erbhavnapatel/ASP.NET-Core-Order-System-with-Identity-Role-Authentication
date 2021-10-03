using Order_System.Entities;
using Order_System.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order_System.Repositories
{
    public interface IOrderRepository
    {
        public Task<Order> DeleteOrder(int orderId);

        public Task<Order> FindOrderById(int orderId);

        public Task<List<OrderModel>> GetOrders();
    }
}