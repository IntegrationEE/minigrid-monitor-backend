namespace Monitor.HostedServices
{
    public enum TaskStatus
    {
        INITIALIZED = 1,
        EXECUTING,
        RESTARTED,
        REMOVED,
    }
}
