using Application.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers;

public class StateController(IUnitOfWork unitOfWork) : Controller
{
    public async Task<ActionResult> Create(string title)
    {
        await unitOfWork.GenericRepository<State>().AddAsync(new State
        {
            Name = title,
        }, CancellationToken.None);
        return Ok();
    }

    public async Task<ActionResult<List<State>>> GetAll()
    {
        return await unitOfWork.GenericRepository<State>().TableNoTracking.ToListAsync();
    }
}