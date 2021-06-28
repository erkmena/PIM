using PIM.Model;
using PIM.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task AddProductAsync(ProductModel product);
        Task<ProductModel> GetProductAsync(int productId);
        Task<bool> UpdateProductAsync(ProductModel product);
        Task<bool> ArchiveProductAsync(int productId);
    }
}
