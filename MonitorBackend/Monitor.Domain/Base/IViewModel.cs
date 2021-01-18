namespace Monitor.Domain.Base
{
    public interface IViewModel
    { }

    public interface IBaseViewModel : IViewModel
    {
        int Id { get; set; }
    }
}
