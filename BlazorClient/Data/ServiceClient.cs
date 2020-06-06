using Grpc.Core;
using Grpc.Net.Client;
using NewBankServer.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClient.Data
{
  public interface IServiceClient
  {
    void CreateClients();
    void DisposeClients();
  }

  public class ServiceClient : IServiceClient
  {
    private static readonly Lazy<ServiceClient> instance = new Lazy<ServiceClient>(() => new ServiceClient());
    public static ServiceClient Instance => instance.Value;

    private readonly GrpcChannel channel;

    public ServiceClient()
    {
      AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.HttpUnencryptedSupport", true);

      var httpClient = new HttpClient(new HttpClientHandler
      {
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      });
      string uri = string.Empty;
#if DEBUG
      uri = "https://localhost:5001";
#else
      uri = "https://67.191.204.48:443";
#endif
      channel = GrpcChannel.ForAddress(uri, new GrpcChannelOptions//"https://192.168.44.128:443", new GrpcChannelOptions
      {
        HttpClient = httpClient
      });
    }

    public UserCRUD.UserCRUDClient UserCRUDClient { get; internal set; }
    public AccountCRUD.AccountCRUDClient AccountCRUDClient { get; internal set; }
    public Authentication.AuthenticationClient AuthenticationClient { get; internal set; }
    public SessionCRUD.SessionCRUDClient SessionCRUDClient { get; internal set; }
    public Creation.CreationClient CreationClient { get; internal set; }
    public TransactionCRUD.TransactionCRUDClient TransactionCRUDClient { get; internal set; }

    public void CreateClients()
    {
      UserCRUDClient = new UserCRUD.UserCRUDClient(channel);
      AccountCRUDClient = new AccountCRUD.AccountCRUDClient(channel);
      AuthenticationClient = new Authentication.AuthenticationClient(channel);
      SessionCRUDClient = new SessionCRUD.SessionCRUDClient(channel);
      CreationClient = new Creation.CreationClient(channel);
      TransactionCRUDClient = new TransactionCRUD.TransactionCRUDClient(channel);
    }

    public void DisposeClients()
    {
      channel.Dispose();
    }
  }
}
