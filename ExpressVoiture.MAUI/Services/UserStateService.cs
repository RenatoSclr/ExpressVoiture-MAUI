using ExpressVoiture.MAUI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.MAUI.Services
{
    public class UserStateService : IUserStateService
    {
        private bool _isLoggedIn = false;

        public bool IsLoggedIn => _isLoggedIn;
        public event Action<bool>? LoginStateChanged;

        public void SetLoginState(bool isLoggedIn)
        {
            if (_isLoggedIn != isLoggedIn)
            {
                _isLoggedIn = isLoggedIn;
                LoginStateChanged?.Invoke(_isLoggedIn);
            }
        }
    }
}
