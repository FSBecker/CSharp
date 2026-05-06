using System.Diagnostics;

namespace LinuxServices;

internal class LinuxService : IPlatformServiceClass
{
    public void Create()
    {
        Process.Start("nano");
    }
}