using System;

namespace Troublemaker.Editor
{
    public sealed class ProgressHandler : IProgressHandler
    {
        public IProgressState Start(String label)
        {
            return new ProgressState(label);
        }
    }
}