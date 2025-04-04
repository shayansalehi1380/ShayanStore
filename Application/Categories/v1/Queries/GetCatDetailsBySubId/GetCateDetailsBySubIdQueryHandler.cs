using Application.Interface;
using Domain.Entity.Products.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.v1.Queries.GetCatDetailsBySubId;

public class GetCateDetailsBySubIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCateDetailsBySubIdQuery, List<CategoryDetail>>
{
    public async Task<List<CategoryDetail>> Handle(GetCateDetailsBySubIdQuery request,
        CancellationToken cancellationToken)
    {
        var catDetails = new List<CategoryDetail>();
        catDetails = await unitOfWork.GenericRepository<CategoryDetail>().TableNoTracking
            .Where(x => x.SubCategoryId == request.Id).ToListAsync(cancellationToken);
        return catDetails;
    }
}