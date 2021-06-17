using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace WpfApp
{
    public class ReactiveViewModel : ObservableObject
    {
        private IDisposable _subscription;
        private IObservable<int> _observable;
        private bool _isSubscribed;
        private ObservableCollection<int> _values;

        public ReactiveViewModel()
        {
            var s = new Subject<int>();
            var o = s.Replay();
            _observable = o;

            o.Connect();
            

            int i = 0;
            GenerateValue = new RelayCommand(() => s.OnNext(i++));
        }

        public bool IsSubscribed
        {
            get => _isSubscribed;
            set
            {
                if (SetProperty(ref _isSubscribed, value))
                {
                    
                    if (_isSubscribed)
                    {
                        Values = new ObservableCollection<int>();
                        _subscription = _observable.Subscribe(v => Values.Add(v));
                    }
                    else
                    {
                        Values.Clear();
                        _subscription.Dispose();
                    }
                }
            }
        }

        public ObservableCollection<int> Values
        {
            get => _values;
            private set => SetProperty(ref _values, value);
        }

        public ICommand GenerateValue { get; }
    }
}