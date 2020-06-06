using Grpc.Net.Client;
using NewBankServer.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClient.Data
{
  public class UserCRUDService
  {
    public User GetByFilter(UserFilter userFilter)
    {
      AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.HttpUnencryptedSupport", true);
      var httpClient = new HttpClient(new HttpClientHandler
      {
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      });
      var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions 
      {
        HttpClient = httpClient
      });
      var client = new UserCRUD.UserCRUDClient(channel);
      return client.GetByFilter(userFilter).Items.First();
    }
  }
}
