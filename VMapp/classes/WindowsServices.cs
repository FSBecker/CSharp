using System.Diagnostics;

namespace WindowsServices;

internal class WindowsService : IPlatformServiceClass
{
    public void Create()
    {
        Process.Start("mspaint.exe");
    }
}