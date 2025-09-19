using AutoMapper;
using Kanban.Business.Exceptions;
using Kanban.Business.Services.Interfaces;
using Kanban.Contracts.Constants;
using Kanban.Contracts.Dto.Response;
using Kanban.Contracts.Results;
using Kanban.Contracts.Results.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace technical_tests_backend_ssr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BoardController(IBoardService boardService, ILogger<BoardController> logger, IMapper mapper) : BaseController(logger, mapper)
    {
        private readonly IBoardService _boardService = boardService;

        [HttpGet]
        public async Task<ActionResult<IApiResult<List<BoardResponse>>>> GetUserBoards()
        {
            IApiResult<List<BoardResponse>> result = new ApiResult<List<BoardResponse>>();

            _log.LogInformation(Messages.BOARD_GETALL);

            try
            {
                var ownerId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var boards = await _boardService.GetByOwnerAsync(ownerId);

                result.Data = _mapper.Map<List<BoardResponse>>(boards);

                if (result.Data.Count == 0)
                    result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.BOARD_NOFOUND_OK, ownerId));
                else
                    result.Set(HandleSuccess(Messages.BOARD_GETALL_OK, ownerId));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }
    }
}
