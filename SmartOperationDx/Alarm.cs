namespace SmartOperationDx
{
    public class Alarm
    {
        public string TagName { get; }
        public string AlarmType { get; }
        public string Severity { get; }
        public string Message { get; }
        public string OccurrenceRisk { get; }

        public Alarm(string tagName, string alarmType, string severity, string message, string occurrenceRisk)
        {
            TagName = tagName;
            AlarmType = alarmType;
            Severity = severity;
            Message = message;
            OccurrenceRisk = occurrenceRisk;
        }
    }
}