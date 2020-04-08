using System;
using System.Threading;

namespace Troublemaker.Editor
{
    public sealed class ProgressState : IProgressState
    {
        private readonly String _label;
        private Int64 _current;
        private Int64 _total;
        private State _state;

        public ProgressState(String label)
        {
            _label = label;
        }

        public String GetLabel()
        {
            switch (_state)
            {
                case State.Initial:
                    return $"{_label}";
                case State.Processing:
                    return $"{_label} [{_current} / {_total}]";
                case State.Success:
                    return $"Completed: {_label}";
                case State.Error:
                    return $"Error: {_label}";
                default:
                    throw new NotSupportedException(_state.ToString());
            }
        }

        public void SetTotal(in Int64 total)
        {
            Interlocked.Exchange(ref _total, total);
        }

        public void SetCurrent(in Int64 total)
        {
            Interlocked.Exchange(ref _current, total);
        }

        public void IncrementCurrent()
        {
            Interlocked.Increment(ref _current);
        }

        public void Success()
        {
            _state = State.Success;
        }

        public void Dispose()
        {
            if (_state < State.Success)
                _state = State.Error;
        }

        private enum State
        {
            Initial = 0,
            Processing,
            Success,
            Error
        }
    }
}