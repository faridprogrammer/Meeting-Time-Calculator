using Microsoft.Extensions.Configuration;
using Ical.Net;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: meeting-time-spend-calculator.exe <path_to_ics_file> <time_period_in_days>");
            return;
        }

        var filePath = args[0];
        if (!int.TryParse(args[1], out int days))
        {
            Console.WriteLine("The time period must be a valid integer (number of days).");
            return;
        }

        if (!File.Exists(filePath))
        {
            Console.WriteLine("ICS file not found. Please check the file path.");
            return;
        }

        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var excludedKeywords = config.GetSection("ExcludedKeywords").Get<List<string>>() ?? [];

        var calendarContent = File.ReadAllText(filePath);
        var calendar = Calendar.Load(calendarContent);

        DateTime now = DateTime.Now;
        DateTime startDate = now.AddDays(-days);

        double totalMeetingMinutes = 0;

        foreach (var eventItem in calendar.Events)
        {
            DateTime eventStart = eventItem.DtStart.AsSystemLocal;
            DateTime eventEnd = eventItem.DtEnd.AsSystemLocal;
            var summary = eventItem.Summary ?? string.Empty;

            if (eventStart >= startDate && eventStart <= now && !excludedKeywords.Exists(keyword => summary.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
            {
                double duration = (eventEnd - eventStart).TotalMinutes;
                totalMeetingMinutes += duration;
            }
        }

        var totalMeetingHours = totalMeetingMinutes / 60;

        Console.WriteLine($"Total time spent in meetings over the last {days} days (excluding specified keywords): {totalMeetingHours:F2} hours");
    }
}
