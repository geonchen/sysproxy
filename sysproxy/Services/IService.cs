
using sysproxy.Models;

namespace sysproxy.Services
{
    public interface IService
    {
        void Start();
        void Refresh(Setting setting);
        void Stop();
    }
}
