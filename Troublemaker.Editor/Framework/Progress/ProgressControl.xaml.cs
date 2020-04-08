using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;
using Timer = System.Timers.Timer;

namespace Troublemaker.Editor.Framework
{
    public partial class ProgressControl : UserControl
    {
        private readonly ConcurrentDictionary<object, ProgressEntry> _entries;
        private readonly Timer _timer;

        private long _processedSize, _totalSize;
        private DateTime _begin;

        public ProgressControl()
        {
            _entries = new ConcurrentDictionary<object, ProgressEntry>();
            _timer = new Timer(500);

            DataContext = this;
            InitializeComponent();
        }

        public void Begin()
        {
            _begin = DateTime.Now;
            _timer.Elapsed += OnTimer;
            _timer.Start();

            Visibility = System.Windows.Visibility.Visible;
        }

        public void End()
        {
            _timer.Stop();
            _timer.Elapsed -= OnTimer;

            Reset();
        }

        private void Reset()
        {
            Visibility = System.Windows.Visibility.Collapsed;

            _entries.Clear();
            _processedSize = 0;
            _totalSize = 1;

            _progressBar.Maximum = 0;
            _progressBar.Value = 0;
            _progressText.Text = "0 %";
            _beginTimeText.Text = "Elapsed: 00:00";
            _speedText.Text = "0 KB / sec";
            _endTimeText.Text = "Remaining: 00:00";
        }

        public void UpdateSize(object key, long processedSize, long totalSize)
        {
            ProgressEntry entry = _entries.GetOrAdd(key, k => new ProgressEntry(k));
            Interlocked.Add(ref _processedSize, entry.UpdateProcessedSize(processedSize));
            Interlocked.Add(ref _totalSize, entry.UpdateTotalSize(totalSize));
        }

        private void OnTimer(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Dispatcher.Invoke(DispatcherPriority.DataBind, (Action) (UpdateProgress));
        }

        private void UpdateProgress()
        {
            _timer.Elapsed -= OnTimer;

            _progressBar.Maximum = _totalSize;
            _progressBar.Value = _processedSize;

            double percents = (_totalSize == 0) ? 0.0 : 100 * _processedSize / (double) _totalSize;
            TimeSpan elapsed = DateTime.Now - _begin;
            double speed = _processedSize / Math.Max(elapsed.TotalSeconds, 1);
            if (speed < 1) speed = 1;
            TimeSpan left = TimeSpan.FromSeconds((_totalSize - _processedSize) / speed);

            _progressText.Text = $"{percents:F2}%";
            _beginTimeText.Text = $"Elapsed: {elapsed:mm\\:ss}";
            _speedText.Text = FormatHelper.BytesFormat(speed) + " / sec";
            _endTimeText.Text = $"Remaining: {left:mm\\:ss}";

            _timer.Elapsed += OnTimer;
        }
    }
}