using System;
using System.Collections.Generic;

namespace Troublemaker.Unpacker
{
    public sealed class DisposableStack : IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new Stack<IDisposable>();

        public T Add<T>(T disposable) where T : IDisposable
        {
            _disposables.Push(disposable);
            return disposable;
        }

        public void Clear()
        {
            _disposables.Clear();
        }

        public void Dispose()
        {
            while (_disposables.Count > 0)
                _disposables.Pop()?.Dispose();
        }
    }
}