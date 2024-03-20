using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Hastane
{
    public class LoginContext
    {
        private ILoginStrategy _loginStrategy;

        public LoginContext(ILoginStrategy loginStrategy)
        {
            _loginStrategy = loginStrategy;
        }

        public bool ExecuteLogin(string username, string password)
        {
            return _loginStrategy.Login(username, password);
        }

        public void SetStrategy(ILoginStrategy loginStrategy)
        {
            _loginStrategy = loginStrategy;
        }
    }

}
