using System;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp
{
    public class SomeService : ISomeService
    {
        public Task Action()
        {
            return Task.Run(() =>
                Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Hello!")));
        }

        public Task ReadData()
        {
            return Task.Delay(TimeSpan.FromSeconds(120));
        }
    }
}