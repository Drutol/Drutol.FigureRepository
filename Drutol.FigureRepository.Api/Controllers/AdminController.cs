using Drutol.FigureRepository.Api.DataAccess;
using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Models;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Admin;
using Drutol.FigureRepository.Shared.Logs;
using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Orders;
using Drutol.FigureRepository.Shared.Orders;
using Drutol.FigureRepository.Shared.Statistics;
using Functional.Maybe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Drutol.FigureRepository.Api.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = AuthPolicies.AdminPolicy)]
public class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IFigureSeedManager _figureSeedManager;
    private readonly IOrderDatabase _orderDatabase;
    private readonly ILogDatabase _logDatabase;
    private readonly IAdminService _adminService;

    public AdminController(
        ILogger<AdminController> logger,
        IFigureSeedManager figureSeedManager,
        IOrderDatabase orderDatabase,
        ILogDatabase logDatabase,
        IAdminService adminService)
    {
        _logger = logger;
        _figureSeedManager = figureSeedManager;
        _orderDatabase = orderDatabase;
        _logDatabase = logDatabase;
        _adminService = adminService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public LoginRequestResult Login(LoginRequest request)
    {
        var result = _adminService.Authenticate(request.Key);

        return result.SelectOrElse(
            response => new LoginRequestResult(true, response),
            () =>
            {
                _logger.LogWarning(DruEventId.AdminAuth.Ev(), "Failed admin login attempt.");
                return new LoginRequestResult(false);
            });
    }

    [HttpPost("orders")]
    public async Task<GetOrdersRequestResult> GetOrders(GetOrdersRequest request)
    {
        return await _orderDatabase.GetOrders(request);
    }  
    
    [HttpPost("logs")]
    public async Task<GetLogsRequestResult> GetLogs(GetLogsRequest request)
    {
        return await _logDatabase.GetLogs(request);
    }  
    
    [HttpPost("statistics")]
    public async Task<GetStatisticsRequestResult> GetLogs(GetStatisticsRequest request)
    {
        var logStatistics = await _logDatabase.GetLogStatistics(request);
        var figureStatistics = new Dictionary<Guid, FigureStatistics>();

        foreach (var figure in _figureSeedManager.Figures)
        {
            figureStatistics[figure.Guid] = await _orderDatabase.GetFigureStatistics(figure.Guid, request);
        }

        return new(figureStatistics, logStatistics);
    }
}