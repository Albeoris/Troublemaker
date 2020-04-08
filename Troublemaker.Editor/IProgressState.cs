using System;

namespace Troublemaker.Editor
{
    public interface IProgressState : IDisposable
    {
        void SetTotal(in Int64 total);
        void SetCurrent(in Int64 total);
        void IncrementCurrent();
        void Success();
    }
}