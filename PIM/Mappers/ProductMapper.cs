
using PIM.Model;
using PIM.Model.DTOs;
using PIM.Model.RequestModels;
using System;
using System.Collections.Generic;

namespace PIM.Mappers
{
    public static class ProductMapper
    {
        internal static IEnumerable<ProductDTOModel> MapProductListToDTOList(IEnumerable<ProductModel> products)
        {
            List<ProductDTOModel> productDTOs = new List<ProductDTOModel>();
            foreach (var item in products)
            {
                ProductDTOModel productDTO = new ProductDTOModel()
                {
                    BrandId = item.BrandId,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName
                };
                productDTOs.Add(productDTO);
            }
            return productDTOs;
        }
    }
}
