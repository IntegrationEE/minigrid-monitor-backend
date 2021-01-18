namespace Monitor.Common.Models
{
    public class PointViewModel<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}
