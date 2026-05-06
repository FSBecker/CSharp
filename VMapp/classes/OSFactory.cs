namespace OsF;

internal static class OSFactory
{
    public static IPlatformServiceClass Create()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return new WindowsService();
        }
        else if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return new LinuxService();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return new OSXService();
        }
        else
        {
            throw new PlatformNotSupportedException("OS understøttes ikke");
        }
    }
}