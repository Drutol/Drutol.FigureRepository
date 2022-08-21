using Drutol.FigureRepository.Api.Interfaces;
using Drutol.FigureRepository.Api.Logging;
using Drutol.FigureRepository.Api.Models;
using Drutol.FigureRepository.Api.Util;
using Drutol.FigureRepository.Shared.Admin;
using Drutol.FigureRepository.Shared.Models.Auth;
using Drutol.FigureRepository.Shared.Models.Orders;
using Drutol.FigureRepository.Shared.Orders;
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
    private readonly ICheckoutDatabase _checkoutDatabase;
    private readonly IAdminService _adminService;

    public AdminController(
        ILogger<AdminController> logger,
        ICheckoutDatabase checkoutDatabase,
        IAdminService adminService)
    {
        _logger = logger;
        _checkoutDatabase = checkoutDatabase;
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
                _logger.LogWarning(EventIds.AdminAuth.Ev(), "Failed admin login attempt.");
                return new LoginRequestResult(false);
            });
    }

    [HttpPost("orders")]
    public async Task<GetOrdersRequestResult> GetOrders(GetOrdersRequest request)
    {
        return await _checkoutDatabase.GetOrders(request);
    }
}