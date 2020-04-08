using System;
using System.IO;
using System.Threading.Tasks;

namespace Troublemaker.Unpacker
{
    public sealed class DependentStream : ProxyStream
    {
        private readonly DisposableStack _beforeStreamClose = new DisposableStack();
        private readonly DisposableStack _afterStreamClose = new DisposableStack();

        public DependentStream(Stream stream)
            : base(stream)
        {
        }

        public T AddStreamDependency<T>(T dependency) where T : IDisposable
        {
            return _beforeStreamClose.Add(dependency);
        }

        public override void Close()
        {
            Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
                return;

            _beforeStreamClose.Dispose();
            _stream.Dispose();
            _afterStreamClose.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }
    }
}