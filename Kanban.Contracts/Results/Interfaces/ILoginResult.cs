namespace Kanban.Contracts.Results.Interfaces
{
    public interface ILoginResult : IApiResult
    {
        string? Token { get; set; }
    }

    public interface ILoginResult<T> : ILoginResult
    {
        T UserData { get; set; }
    }
}
