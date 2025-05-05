using System.Net;

namespace Gacha_Game.Core.Dtos
{
    public class ApiResponse
    {
        public string Message { get; set; } = default!;
        public bool IsSuccess { get; set; } = default!;
        public HttpStatusCode StatusCode { get; set; } = default!;
        public object Result { get; set; } = default!;
    }
}
