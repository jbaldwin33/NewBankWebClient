using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace BlazorClient.ViewModels
{
  public enum AccountEnum
  {
    Checking,
    Saving
  }

  public class SignUpViewModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public SecureString SecurePassword { get; set; }
    public string Password { get; set; }
    public AccountEnum AccountType { get; set; }
    public ObservableCollection<string> AccountTypes { get; set; }

    public SignUpViewModel()
    {
      AccountTypes = new ObservableCollection<string> { AccountEnum.Checking.ToString(), AccountEnum.Saving.ToString() };
    }
  }
}
