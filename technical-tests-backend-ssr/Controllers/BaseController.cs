using AutoMapper;
using Kanban.Business.Enums;
using Kanban.Business.Exceptions;
using Kanban.Contracts.Results;
using Kanban.Contracts.Results.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace technical_tests_backend_ssr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ILogger _log;
        protected IMapper _mapper;
        public BaseController(ILogger log, IMapper mapper)
        {
            _log = log;
            _mapper = mapper;
        }

        //protected IApiResult HandleException(ResultException ex, string message = null)
        //{
        //    message = $"{message ?? "Error"} : {ex.StatusCode} . Exception: {ex.Message}";
        //    _log.LogError(message, ex);

        //    return new ApiResult().SetError(message, ex.StatusCode);
        //}

        protected IApiResult HandleException(Exception ex, HttpStatusCode statusCode, string? message = null)
        {
            message = $"{message ?? "Error"} : {statusCode} . Exception: {ex.Message}";
            _log.LogError(message, ex);

            return new ApiResult().SetError(message, statusCode);
        }

        protected IApiResult HandleSuccess(string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            message = $"{message ?? "Operacion Realizada con exito."} - {statusCode}";
            _log.LogInformation(message);

            return new ApiResult(message, statusCode);
        }

        protected IApiResult HandleSuccess(HttpStatusCode statusCode, string template, params object[] args)
        {
            var message = MessageFormatter.Format(template, args);

            return HandleSuccess(message, HttpStatusCode.OK);
        }

        protected IApiResult HandleSuccess(string template, params object[] args)
        {
            var message = MessageFormatter.Format(template, args);

            return HandleSuccess(message, HttpStatusCode.OK);
        }

        protected IApiResult HandleServiceException(BusinessException ex)
        {
            var httpCode = ex.ErrorCode switch
            {
                BusinessErrorCode.None => HttpStatusCode.OK,
                BusinessErrorCode.UserAlreadyExists => HttpStatusCode.Conflict,
                BusinessErrorCode.UserNotFound => HttpStatusCode.NotFound,
                BusinessErrorCode.InvalidPassword => HttpStatusCode.Unauthorized,
                BusinessErrorCode.InvalidRole => HttpStatusCode.BadRequest,
                BusinessErrorCode.Unauthorized => HttpStatusCode.Unauthorized,
                BusinessErrorCode.BoardNotFound => HttpStatusCode.NotFound,
                BusinessErrorCode.ColumnNotFound => HttpStatusCode.NotFound,
                BusinessErrorCode.TaskNotFound => HttpStatusCode.NotFound,
                BusinessErrorCode.ValidationError => HttpStatusCode.BadRequest,
                BusinessErrorCode.Forbidden => HttpStatusCode.Forbidden,
                BusinessErrorCode.Conflict => HttpStatusCode.Conflict,
                BusinessErrorCode.InternalError => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.BadRequest
            };
            return new ApiResult().SetError($"{ex.Message} ({ex.Reason})", httpCode);
        }

        protected ObjectResult ResponseApi(IApiResult apiResult)
        {
            return StatusCode((int) apiResult.StatusCode, apiResult);
        }

        private string GetMessage(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;

            return ex.Message;
        }

        public static class MessageFormatter
        {
            public static string Format(string template, params object[] args)
            {
                return string.Format(template, args);
            }
        }
    }
}
