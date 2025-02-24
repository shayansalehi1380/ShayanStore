using Application.Interface;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers;

public class StateController(IUnitOfWork unitOfWork) : Controller
{
    public async Task<ActionResult> Create(string name)
    {
        await unitOfWork.GenericRepository<State>().AddAsync(new State
        {
            Name = name,
        }, CancellationToken.None);
        return Ok();
    }

    public async Task<ActionResult<List<State>>> GetAll()
    {
        return await unitOfWork.GenericRepository<State>().TableNoTracking.ToListAsync();
    }

    public async Task<ActionResult> Update(int id, string name)
    {
        var state = await unitOfWork.GenericRepository<State>().GetByIdAsync(id, CancellationToken.None);
        if (state == null)
        {
            return NotFound();
        }

        state.Name = name; 

        await unitOfWork.GenericRepository<State>().UpdateAsync(state, CancellationToken.None);
        return Ok(state);
    }

    public async Task<ActionResult> Delete(int id)
    {
        var state = await unitOfWork.GenericRepository<State>().GetByIdAsync(id, CancellationToken.None);
        if (state == null)
        {
            return NotFound();
        }

        await unitOfWork.GenericRepository<State>().DeleteAsync(state, CancellationToken.None);
        return Ok(state);
    }
}