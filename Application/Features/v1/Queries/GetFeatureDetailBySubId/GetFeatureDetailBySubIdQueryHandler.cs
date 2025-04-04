using Application.Dtos.Features;
using Application.Interface;
using Domain.Entity.Products.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.v1.Queries.GetFeatureDetailBySubId;

public class GetFeatureDetailBySubIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetFeatureDetailBySubIdQuery, List<FeatureDto>>
{
    public async Task<List<FeatureDto>> Handle(GetFeatureDetailBySubIdQuery request,
        CancellationToken cancellationToken)
    {
        var feature = await unitOfWork.GenericRepository<FeatureDetails>().TableNoTracking
            .Include(x => x.Feature)
            .Where(x => x.SubCategoryId == request.Id)
            .ToListAsync(cancellationToken);

        List<FeatureDto> featureDtos = new List<FeatureDto>();

        foreach (var i in feature)
        {
            var existingFeatureDto = featureDtos.FirstOrDefault(x => x.Id == i.FeatureId);

            if (existingFeatureDto != null)
            {
                existingFeatureDto.Details.Add(new FeatureDetailDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    SubCategoryId = i.SubCategoryId
                });
            }
            else
            {
                var newFeatureDto = new FeatureDto
                {
                    Id = i.FeatureId,
                    Title = i.Feature.Title,
                    Priority = i.Priority,
                    Details = new List<FeatureDetailDto>()
                };
                newFeatureDto.Details.Add(new FeatureDetailDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    SubCategoryId = i.SubCategoryId
                });
                featureDtos.Add(newFeatureDto);
            }
        }

        return featureDtos;
    }
}