namespace Monitor.Domain.ViewModels
{
    public class EnumViewModel<T>
        where T : struct
    {
        public T Value { get; set; }

        public string Label { get; set; }
    }
}
