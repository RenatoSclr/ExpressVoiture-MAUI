using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressVoiture.MAUI.Services.Interface
{
    public interface IUserStateService
    {
        bool IsLoggedIn { get; }
        event Action<bool>? LoginStateChanged;
        void SetLoginState(bool isLoggedIn);
    }
}
