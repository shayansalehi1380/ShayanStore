using Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UI.Controllers;

public class UploadFileController(IWebHostEnvironment environment) : Controller
{
    private Upload _upload = new Upload(environment);

    public async Task<ActionResult> UploadFromFile(IFormFile file, string patch)
    {
        var fileName = _upload.UploadFile(file, "TestImage");
        return RedirectToAction("Index", "Home");
    }

}