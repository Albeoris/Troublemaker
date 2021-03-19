using System;

namespace Troublemaker.Editor
{
    public interface IProgressHandler
    {
        IProgressState Start(String label);
    }
}