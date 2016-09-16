using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface IInvoiceService : IServiceContract, IDisposable
    {
        [OperationContract]
        List<Invoice> GetInvoicesByAccount(Account account);

        [OperationContract]
        List<Invoice> GetInvoicesByCompany(Company company);

        [OperationContract]
        int CreateInvoice(Invoice invoice);

        [OperationContract]
        bool DeleteInvoice(Invoice invoice);

        [OperationContract]
        Invoice GetInvoice(int invoice);

        [OperationContract]
        List<Invoice> FindInvoicesByCompany(Company company, string search_pattern);

        [OperationContract]
        InvoiceItem GetInvoiceItemByOrderItemKey(int order_item_key);



        [OperationContract]
        Task<List<Invoice>> GetInvoicesByAccountAsync(Account account);

        [OperationContract]
        Task<List<Invoice>> GetInvoicesByCompanyAsync(Company company);

        [OperationContract]
        Task<int> CreateInvoiceAsync(Invoice invoice);

        [OperationContract]
        Task<bool> DeleteInvoiceAsync(Invoice invoice);

        [OperationContract]
        Task<Invoice> GetInvoiceAsync(int invoice);

        [OperationContract]
        Task<List<Invoice>> FindInvoicesByCompanyAsync(Company company, string search_pattern);

        [OperationContract]
        Task<InvoiceItem> GetInvoiceItemByOrderItemKeyAsync(int order_item_key);
    }
}