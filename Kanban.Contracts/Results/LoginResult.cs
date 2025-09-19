using Kanban.Contracts.Results.Interfaces;
using System.Text.Json.Serialization;

namespace Kanban.Contracts.Results
{
    public class LoginResult : ApiResult, ILoginResult
    {
        [JsonPropertyOrder(7)]

        public string? Token { get; set; }
    }

    public class LoginResult<T> : LoginResult, ILoginResult<T>
    {
        public LoginResult(): base()
        {
            this.Message = "Login successful";
        }
        [JsonPropertyOrder(6)]
        public T UserData { get; set; } = default!;
    }
}
