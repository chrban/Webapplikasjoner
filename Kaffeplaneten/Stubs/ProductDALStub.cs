using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kaffeplaneten.DAL;
using Kaffeplaneten.Models;

namespace Stubs
{
    class ProductDALStub : IProductDAL
    {
        public bool add(ProductModel productModel)
        {
            if (productModel.productName != "")
                return true;
            return false;
        }

        public bool Delete(int id)
        {
            if (id > 0)
                return true;
            return false;
        }

        public ProductModel find(int id)
        {
            var productModel = new ProductModel();
            productModel.category = "Kaffe";
            productModel.description = "God kaffe";
            productModel.imageURL = "kaffe.kaffebilde.jpg";
            productModel.price = 100;
            productModel.productID = 1;
            productModel.productName = "Svart kaffe";
            productModel.quantity = 10;
            productModel.stock = 100;
            return productModel;
        }

        public List<ProductModel> getAllProducts()
        {
            var list = new List<ProductModel>();
            list.Add(find(1));
            list.Add(find(1));
            list.Add(find(1));
            list.Add(find(1));
            return list;
        }

        public bool update(ProductModel productModel)
        {
            if (productModel.productName != "")
                return true;
            return false;
        }

        public bool updateQuantity(ProductModel productModel)
        {
            if (productModel.productName != "")
                return true;
            return false;
        }
    }
}
