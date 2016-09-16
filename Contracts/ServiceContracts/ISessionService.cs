using System.ServiceModel;
using QIQO.Business.Client.Entities;
using System.Threading.Tasks;
using Core;
using System;

namespace QIQO.Business.Client.Contracts
{
    [ServiceContract]
    public interface ISessionService : IServiceContract, IDisposable
    {
        [OperationContract]
        void RegisterSession(int process_id, string host_name, string user_domain, string user_name, int company_key);

        [OperationContract]
        void UnregisterSession(int process_id, string host_name, string user_domain, string user_name, int company_key);

        [OperationContract]
        UserSession GetSessionObject(int process_id, string host_name, string user_domain, string user_name);



        [OperationContract]
        void RegisterSessionAsync(int process_id, string host_name, string user_domain, string user_name, int company_key);

        [OperationContract]
        void UnregisterSessionAsync(int process_id, string host_name, string user_domain, string user_name, int company_key);

        [OperationContract]
        Task<UserSession> GetSessionObjectAsync(int process_id, string host_name, string user_domain, string user_name);
    }
}