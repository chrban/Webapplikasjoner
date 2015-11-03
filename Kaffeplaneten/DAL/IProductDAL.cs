using System.Collections.Generic;
using Kaffeplaneten.Models;

namespace Kaffeplaneten.DAL
{
    public interface IProductDAL
    {
        bool add(ProductModel productModel);
        bool Delete(int id);
        ProductModel find(int id);
        List<ProductModel> getAllProducts();
        bool update(ProductModel _productModel);
        bool updateQuantity(ProductModel productModel);
    }
}