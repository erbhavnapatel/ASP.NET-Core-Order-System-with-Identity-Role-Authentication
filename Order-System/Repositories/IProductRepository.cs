using Order_System.Entities;
using Order_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Repositories
{
    public interface IProductRepository
    {
        Task<Product> FindProductById(int id);
        Task<List<ProductModel>> GetProducts();
        Task<Product> DeleteProduct(int productId);
    }
}