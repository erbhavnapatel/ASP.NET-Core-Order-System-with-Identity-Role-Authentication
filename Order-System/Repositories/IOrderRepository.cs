using Order_System.Entities;
using Order_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> FindOrderById(int orderId);
        Task<List<OrderModel>> GetOrders();
        Task<Order> DeleteOrder(int orderId);
    }
}