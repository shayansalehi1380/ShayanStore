using Domain.Entity.Products.Categories;
using MediatR;

namespace Application.Categories.v1.Queries.GetCatDetailsBySubId;

public record GetCateDetailsBySubIdQuery(int Id) : IRequest<List<CategoryDetail>>;