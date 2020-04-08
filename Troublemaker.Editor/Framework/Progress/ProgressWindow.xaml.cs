using System;
using System.ComponentModel;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using Troublemaker.Editor.Pages;
using Timer = System.Timers.Timer;

namespace Troublemaker.Editor.Framework
{
    public partial class ProgressWindow : Window
    {
        private readonly Timer _timer;

        private Int64 _processedCount, _totalCount;
        private DateTime _begin;

        public ProgressWindow(string title)
        {
            Loaded += OnLoaded;
            Closing += OnClosing;

            _timer = new Timer(500);
            _timer.Elapsed += OnTimer;

            DataContext = this;
            InitializeComponent();

            _titleText.Text = title;
        }

        public static ProgressWindow ShowBackground(String title)
        {
            Object sync = new Object();
            ProgressWindow result = null;
            
            Thread thread = new Thread(() =>
            {
                result = new ProgressWindow(title);
                result.Topmost = true;
                lock (sync)
                    Monitor.Pulse(sync);
                result.ShowDialog();
            });
            
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            lock (sync)
                Monitor.Wait(sync);

            return result ?? throw new InvalidOperationException();
        }

        public new void Close()
        {
            Dispatcher.Invoke(base.Close);
        }
        
        public void SetTotal(long totalCount)
        {
            Interlocked.Add(ref _totalCount, totalCount);
        }

        public void Increment(long processedCount)
        {
            Interlocked.Add(ref _processedCount, processedCount);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _begin = DateTime.Now;
            _timer.Start();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            _timer.Stop();
            _timer.Elapsed -= OnTimer;
        }

        private void OnTimer(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(UpdateProgress));
        }

        private void UpdateProgress()
        {
            _timer.Elapsed -= OnTimer;

            _progressBar.Maximum = _totalCount;
            _progressBar.Value = _processedCount;

            double percents = (_totalCount == 0) ? 0.0 : 100 * _processedCount / (double)_totalCount;
            TimeSpan elapsed = DateTime.Now - _begin;
            double speed = _processedCount / Math.Max(elapsed.TotalSeconds, 1);
            if (speed < 1) speed = 1;
            TimeSpan left = TimeSpan.FromSeconds((_totalCount - _processedCount) / speed);

            _progressText.Text = $"{percents:F2}%";
            _beginTimeText.Text = $"Elapsed: {elapsed:mm\\:ss}";
            _processedText.Text = $"{_processedCount} / {_totalCount}";
            _endTimeText.Text = $"Remaining: {left:mm\\:ss}";

            _timer.Elapsed += OnTimer;
        }
    }
}