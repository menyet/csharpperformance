using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Threading;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WpfApp
{
    public class TickViewModel : ObservableObject
    {
        private string _value;

        public TickViewModel(IObservable<string> valueObservable)
        {

            valueObservable.ObserveOn(DispatcherSynchronizationContext.Current).Subscribe(_ => UpdateValue(_));

        }

        private void UpdateValue(string s)
        {
            Thread.Sleep(10);
            Value = s;
        }

        public string Value
        {
            get => _value;
            private set => SetProperty(ref _value, value);
        }
    }


    public class MemoryLeakViewModel : ObservableObject
    {
        private IObservable<int> _time;
        private IReadOnlyCollection<TickViewModel> _ticks;
        private int _selectedChannel;


        public MemoryLeakViewModel()
        {
            int t = 0;
            var o = Observable.Interval(TimeSpan.FromSeconds(1)).Select(_ => ++t).Publish();

            o.Connect();

            _time = o;
        }

        private void SelectSet(int set)
        {
            Ticks = Enumerable.Range(1, 20).Select(_ => new TickViewModel(GetObservable(set, _))).ToList();
        }

        private IObservable<string> GetObservable(int set, int channel)
        {
            return _time.Select(_ => set + " " + channel + " " + set * 1000000 + channel * channel * 10000 + _);
        }

        public IReadOnlyCollection<int> Channels { get; } = Enumerable.Range(1, 10).ToList();

        public int SelectedChannel
        {
            get => _selectedChannel;
            set
            {
                SetProperty(ref _selectedChannel, value);
                SelectSet(_selectedChannel);
            }
        }

        public IReadOnlyCollection<TickViewModel> Ticks
        {
            get => _ticks;
            private set => SetProperty(ref _ticks, value);
        }
    }
}