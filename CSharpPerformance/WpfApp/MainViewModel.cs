using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace WpfApp
{
    public class MainViewModel : ObservableObject
    {
        private readonly ISomeService _service;

        public MainViewModel(ISomeService service)
        {
            ReactiveViewModel = new ReactiveViewModel();
            MemoryLeakViewModel = new MemoryLeakViewModel();

            _service = service;
            DeadlockCommand = new RelayCommand(LockingOperation);

            CpuBoundCommand = new RelayCommand(CpuBoundOperation);

            IoBoundCommand = new RelayCommand(IoBoundOperation);

            Command2 = new RelayCommand(() =>
            {


            });
        }

        public ReactiveViewModel ReactiveViewModel { get; }

        public MemoryLeakViewModel MemoryLeakViewModel { get; set; }

        public ICommand Command2 { get; }

        public ICommand DeadlockCommand { get; }

        public ICommand CpuBoundCommand { get; }

        public ICommand IoBoundCommand { get; }

        private void LockingOperation()
        {
            _service.Action().Wait();
        }

        private void CpuBoundOperation()
        {
            static int Foo(int l)
            {
                if (l == 0)
                {
                    return 1;
                }
                return Math.Max(Foo(l - 1), Foo(l - 1)) + 1;
            }

            Stopwatch w = new Stopwatch();
            w.Start();
            var result = Foo(30);
            w.Stop();
            MessageBox.Show($"Foo returned {result} in {w.ElapsedMilliseconds} ms");
        }

        private void IoBoundOperation()
        {
            _service.ReadData().Wait();
        }

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