using System.Threading;
using System.Threading.Tasks;

namespace Monitor.HostedServices
{
    public class TaskEntry
    {
        public TaskEntry(Task task)
        {
            Task = task;
            SetStatus(TaskStatus.INITIALIZED);
            CreateNewToken();
        }

        public Task Task { get; private set; }

        public int? RecordId { get; private set; }

        public CancellationTokenSource CancellationTokenSource { get; private set; }

        public TaskStatus Status { get; private set; }

        public void SetStatus(TaskStatus status)
        {
            Status = status;
        }

        public void CreateNewToken()
        {
            CancellationTokenSource = new CancellationTokenSource();
        }

        public void SetRecordId(int? recordId)
        {
            RecordId = recordId;
        }
    }
}
