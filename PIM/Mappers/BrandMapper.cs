using PIM.Model;
using PIM.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIM.Mappers
{
    public static class BrandMapper
    {
        public static BrandDTOModel MapBrandModelToDTOModel(BrandModel brand)
        {
            return new BrandDTOModel()
            {
                BrandId = brand.BrandId,
                BrandName = brand.BrandName,
                Products = ProductMapper.MapProductListToDTOList(brand.Products)                
            };
        }
    }
}
