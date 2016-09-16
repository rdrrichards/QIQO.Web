using System.ServiceModel;
using QIQO.Business.Client.Entities;
using Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IProductService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Product> GetAllProducts();

        [OperationContract]
        List<Product> GetProducts(Company company);

        [OperationContract]
        int CreateProduct(Product product);

        [OperationContract]
        bool DeleteProduct(Product product);

        [OperationContract]
        [FaultContract(typeof(NotFoundException))]
        Product GetProduct(int product_key);



        [OperationContract]
        Task<List<Product>> GetAllProductsAsync();

        [OperationContract]
        Task<List<Product>> GetProductsAsync(Company company);

        [OperationContract]
        Task<int> CreateProductAsync(Product product);

        [OperationContract]
        Task<bool> DeleteProductAsync(Product product);

        [OperationContract]
        Task<Product> GetProductAsync(int product_key);
    }
}