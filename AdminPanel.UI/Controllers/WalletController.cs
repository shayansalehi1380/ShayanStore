using Application.Common.ApiResult;
using Application.Interface;
using Domain.Entity.Products.Guaranties;
using Domain.Entity.Users;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.UI.Controllers
{
    public class WalletController(IUnitOfWork unitOfWork) : Controller
    {
        public async Task<ActionResult<List<Wallet>>> GetAllWallet(string searchWallet, int tabs = 1)
        {

            ViewBag.selectTab = tabs;

            IQueryable<Wallet> queryhWallet = unitOfWork.GenericRepository<Wallet>()
                .TableNoTracking
                .Include(w => w.User);

            if (!string.IsNullOrEmpty(searchWallet))
            {
                queryhWallet = queryhWallet.Where(x =>
                    x.User.UserName.Contains(searchWallet) ||
                    x.User.Name.Contains(searchWallet) ||
                    x.User.Family.Contains(searchWallet));
            }


            ViewBag.Wallet = await queryhWallet.ToListAsync();
            return RedirectToAction("ManageWallet", "Admin");
        }

        public async Task<ActionResult<Wallet>> Update(int id, int name)
        {
            var wallet = await unitOfWork.GenericRepository<Wallet>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (wallet == null)
            {
                return NotFound();
            }

            wallet.WalletBalance = name;

            await unitOfWork.GenericRepository<Wallet>().UpdateAsync(wallet, CancellationToken.None);
            return RedirectToAction("ManageWallet", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }

        public async Task<ActionResult<Wallet>> SoftDelete(int id)
        {
            var wallet = await unitOfWork.GenericRepository<Wallet>().Table.FirstOrDefaultAsync(x => x.Id == id, CancellationToken.None);
            if (wallet == null)
            {
                return NotFound();
            }

            wallet.IsDelete = true;

            await unitOfWork.GenericRepository<Wallet>().UpdateAsync(wallet, CancellationToken.None);
            return RedirectToAction("ManageGuarantee", "Admin", new { tabs = 1, status = FunctionStatus.Success });
        }
    }
}

