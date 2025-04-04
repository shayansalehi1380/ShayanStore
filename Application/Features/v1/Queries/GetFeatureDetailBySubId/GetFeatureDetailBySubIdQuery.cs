using Application.Dtos.Features;
using MediatR;

namespace Application.Features.v1.Queries.GetFeatureDetailBySubId;

public record GetFeatureDetailBySubIdQuery(int Id):IRequest<List<FeatureDto>>;