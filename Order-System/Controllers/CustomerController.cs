using Order_System.Entities;
using Order_System.Models;
using Order_System.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Customer_Supplier_Authentication.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CustomerController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this._orderRepository = orderRepository;
            this._productRepository = productRepository;
        }
        
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _productRepository.GetProducts();
        }

        // GET api/<CustomerController>
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            var result = await _orderRepository.FindOrderById(orderId);
            if (result != null)
            {
                return result;
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{orderId}")]
        public async Task<ActionResult<Order>> DeleteOrder(int orderId)
        {
            var result = await _orderRepository.DeleteOrder(orderId);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }
    }
}
