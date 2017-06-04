using IBalance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBalance.Domain.Abstract
{
    public interface IProductRepository
    {
        /// <summary>
        /// gets all products from database
        /// </summary>
        List<Product> GetProducts();

        /// <summary>
        /// get product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProduct(int id);

        /// <summary>
        /// saves product in database
        /// </summary>
        /// <param name="product"></param>
        void SaveProduct(Product product);

        /// <summary>
        /// deletes product which id = productId from database
        /// </summary>
        /// <param name="productId"></param>
        void DeleteProduct(int productId);
    }
}
