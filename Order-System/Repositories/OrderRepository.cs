﻿using Order_System.Entities;
using Order_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDatabaseContext _context;

        public OrderRepository(OrderDatabaseContext context)
        {
            this._context = context;
        }

        public async Task<Order> DeleteOrder(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> FindOrderById(int orderId)
        {
            var order = await _context.Order.FindAsync(orderId);
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<List<OrderModel>> GetOrders()
        {
            var orders = new List<OrderModel>();
            var allOrders = await _context.Order.ToListAsync();
            if (allOrders.Any() == true)
            {
                foreach (var o in allOrders)
                {
                    orders.Add(new OrderModel()
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        OrderNumber = o.OrderNumber,
                        CustomerId = o.CustomerId,
                        TotalAmount = o.TotalAmount
                    });
                }
            }
            return orders;
        }
    }
}
