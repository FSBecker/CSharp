namespace VM

{
    class VMfunktioner
    {
        public void DisplayInfo()
        {
            string platform = "Platform: " + Environment.OSVersion.Platform.ToString();
            string folder = "Folder: " + Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).ToString();
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Console.Write(platform);
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2 - 1);
            Console.Write(folder);
            Console.ReadKey();


        }
        static double GetCpuUsagePercent()
        {
            string[] first = File.ReadAllLines("/proc/stat")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long idle1 = long.Parse(first[4]);
            long total1 = first.Skip(1).Select(long.Parse).Sum();

            Thread.Sleep(1000);

            string[] second = File.ReadAllLines("/proc/stat")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long idle2 = long.Parse(second[4]);
            long total2 = second.Skip(1).Select(long.Parse).Sum();

            long idleDiff = idle2 - idle1;
            long totalDiff = total2 - total1;

            return 100.0 * (1.0 - (double)idleDiff / totalDiff);
        }
        static double GetRamUsagePercent()
        {
            var lines = File.ReadAllLines("/proc/meminfo");

            long GetValue(string name)
            {
                string? line = lines.FirstOrDefault(l => l.StartsWith(name + ":"));

                if (line == null)
                    return 0;

                return long.Parse(line.Split(':')[1].Trim().Split(' ')[0]);
            }

            long total = GetValue("MemTotal");
            long available = GetValue("MemAvailable");

            if (available == 0)
                available = GetValue("MemFree");

            if (total == 0)
                return 0;

            long used = total - available;

            return 100.0 * used / total;
        }
        public void SenderReciever()
        {
            string path;

            if (OperatingSystem.IsWindows())
            {
                path = @"C:\SharedLog\cpu-log.txt";

                string[] storedLines = [];

                while (true)
                {
                    if (File.Exists(path))
                    {
                        string[] currentLines = File.ReadAllLines(path);

                        if (!currentLines.SequenceEqual(storedLines))
                        {
                            storedLines = currentLines;
                            Console.Clear();
                            foreach (string line in storedLines)
                            {
                                Console.WriteLine(line);
                            }
                        }
                    }

                    Thread.Sleep(2000);
                }
            }
            else if (OperatingSystem.IsLinux())
            {
                path = "/mnt/c/SharedLog/cpu-log.txt";

                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

                while (true)
                {
                    double cpu = GetCpuUsagePercent();
                    double ram = GetRamUsagePercent();

                    string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | CPU: {cpu:F2}% | RAM: {ram:F2}%";
                    File.AppendAllText(path, line + Environment.NewLine);

                    Thread.Sleep(2000);
                }
            }
        }
    }
}

