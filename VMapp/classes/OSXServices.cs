using System.Diagnostics;

namespace OSXServices;

internal class OSXService : IPlatformServiceClass
{
    public void Create()
    {
        Process.Start("open", "-a TextEdit");
    }
}