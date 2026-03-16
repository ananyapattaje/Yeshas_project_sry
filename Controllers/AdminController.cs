using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [HttpPut("activate-farmer/{id}")]
    public async Task<IActionResult> ActivateFarmer(int id)
    {
        await _service.ActivateFarmer(id);
        return Ok("Farmer Activated");
    }

    [HttpPut("deactivate-farmer/{id}")]
    public async Task<IActionResult> DeactivateFarmer(int id)
    {
        await _service.DeactivateFarmer(id);
        return Ok("Farmer Deactivated");
    }

    [HttpGet("farmer-performance-report")]
    public async Task<IActionResult> ExportFarmerPerformance()
    {
        var data = await _service.GetFarmerPerformance();

        var excelFile = _service.GenerateExcel(data);

        return File(
            excelFile,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "FarmerPerformance.xlsx"
        );
    }
}