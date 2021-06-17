using System.Threading.Tasks;

namespace WpfApp
{
    public interface ISomeService
    {
        Task Action();

        Task ReadData();
    }
}