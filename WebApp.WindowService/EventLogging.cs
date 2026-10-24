using System.Diagnostics;

namespace WebApp.WindowService
{
    public class EventLogging
    {
        private const string Source = "SimpleWindowService";
        private const string LogName = "Simple Window Service";

        public static void LogInformation(string message)
        {
            LogNotifyServiceEvent(message, EventLogEntryType.Information);
        }

        public static void LogWarning(string message)
        {
            LogNotifyServiceEvent(message, EventLogEntryType.Warning);
        }

        public static void LogError(string message)
        {
            LogNotifyServiceEvent(message, EventLogEntryType.Error);
        }

        private static void LogNotifyServiceEvent(string message, EventLogEntryType type)
        {
            if (!EventLog.SourceExists(Source))
                EventLog.CreateEventSource(Source, LogName);

            var eLog = new EventLog()
            {
                Source = Source
            };

            eLog.WriteEntry(message, type);
        }
    }
}
