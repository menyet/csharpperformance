using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace WpfApp
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel(ISomeService service)
        {
            Command = new RelayCommand(() => service.Action().Wait());

            Command2 = new RelayCommand(() =>
            {
                

            });
        }

        public ICommand Command2 { get; }

        public ICommand Command { get; }

    }

    public class ActionDisposable : IDisposable
    {
        private readonly Action _disposeAction;

        public ActionDisposable(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }

        public void Dispose()
        {
            _disposeAction();
        }
    }

    
}