using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;

namespace Shared
{
    public static class Debug
    {
        static Action<string> debugLogAction;

        public static void SetDebugAction(Action<string> onDebugLog)
        {
            debugLogAction = onDebugLog;
        }

        public static void Log(string message)
        {
            if (debugLogAction != null)
                debugLogAction(message);
            else
                Console.WriteLine(message);
        }

        public static double CheckExecutionTime(Action action)
        {
            Stopwatch watch = Stopwatch.StartNew();
            action?.Invoke();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public static void LogError(string file, Exception ex)
        {
            List<string> lines = GetExceptionLines(ex);
            if (lines.Count == 0)
                return;


            if (File.Exists(file))
                File.Delete(file);

            using (StreamWriter writer = new StreamWriter(File.Create(file)))
            {
                foreach (var line in lines)
                    writer.WriteLine(line);
            }
        }

        private static List<string> GetExceptionLines(Exception ex, List<string> currlines = null)
        {
            List<string> lines = new List<string>();
            if (currlines != null)
                lines.AddRange(currlines);

            if (ex != null)
            {
                lines.Add($"------------------------");
                lines.Add(ex.Message);
                lines.Add($"Stack trace: {ex.StackTrace}");
                lines.Add($"------------------------");
                lines.Add($"Source: {ex.Source}");
                lines.Add($"------------------------");
                lines.Add($"Stack trace: {ex.StackTrace}");
                lines.Add($"------------------------");
                var data = ex.Data;
                if (data != null && data.Keys.Count > 0)
                {
                    lines.Add("Data: #########");
                    var keys = data.Keys;
                    foreach (var key in keys)
                    {
                        lines.Add($"Key: {key.ToString()}");
                        lines.Add($"Value: {data[key].ToString()}");
                    }
                }
                lines.Add($"------------------------");
                lines.Add(" ");
                lines.Add(" ");
                lines.Add(" ");

                if (ex.InnerException != null)
                    lines.AddRange(GetExceptionLines(ex.InnerException, lines));
            }

            return lines;
        }
    }
}
