using Application.Common;
using Application.Common.ApiResult;
using Application.Common.Utilities;
using Application.Interface;
using Domain.Entity.Products;
using Domain.Entity.Products.Brands;
using Domain.Entity.Products.Categories;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.V1.Commands.CreateProduct;

public class CreateProductCommandHandler(IUnitOfWork _unitOfWork, IWebHostEnvironment env)
    : IRequestHandler<CreateProductCommand, ApiResult<int>>
{
    public async Task<ApiResult<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        #region Validation

        if (await _unitOfWork.GenericRepository<Product>().TableNoTracking
                .AnyAsync(x => x.UniqueCode == request.Product.UniqueCode, cancellationToken: cancellationToken))
        {
            return new ApiResult<int>(0, "کد محصول تکراری میباشد", ApiResultStatusCode.BadRequest);
        }

        if (!await _unitOfWork.GenericRepository<Brand>().TableNoTracking
                .AnyAsync(x => x.Id == request.Product.BrandId.ToInt(), cancellationToken: cancellationToken))
        {
            return new ApiResult<int>(0, "برند محصول نا معتبر میباشد", ApiResultStatusCode.BadRequest);
        }

        if (!await _unitOfWork.GenericRepository<CategoryDetail>().TableNoTracking
                .AnyAsync(x => x.Id == request.Product.CategoryDetailId.ToInt(), cancellationToken: cancellationToken))
        {
            return new ApiResult<int>(0, "زیر گروه محصول نا معتبر میباشد", ApiResultStatusCode.BadRequest);
        }

        #endregion

        Upload up = new Upload(env);
        var prod = new Product
        {
            UniqueCode = request.Product.UniqueCode,
            BrandId = request.Product.BrandId.ToInt(),
            Status = (ProductStatus)request.Product.ProductStatus.ToInt(),
            Detail = request.Product.Detail!,
            Strengths = request.Product.Strengths,
            CreatorId = request.UserId,
            DiscountAmount = request.Product.DiscountAmount.ToInt(),
            EnTitle = request.Product.EnTitle!,
            WeakPoints = request.Product.WeakPoints,
            CategoryDetailId = request.Product.CategoryDetailId.ToInt(),
            SeoTitle = request.Product.SeoTitle,
            SeoDesc = request.Product.SeoDesc,
            SeoCanonical = request.Product.SeoCanonical,
            ProductGift = request.Product.ProductGift,
            OnClick = 0,
            IsOffer = request.Product.IsOffer,
            IsActive = request.Product.IsActive,
            InterestRate = request.Product.InterestRate,
            InsertDate = DateTime.Now,
            ImageUri = up.AddImage(request.Product.ImageUri!, "Images/Product",
                Guid.NewGuid().ToString().Substring(0, 6)),
            FaTitle = request.Product.FaTitle!,
            FullDetail = request.Product.FullDetail!,
        };
        await _unitOfWork.GenericRepository<Product>().AddAsync(prod, cancellationToken);
        return new ApiResult<int>(prod.Id, ApiResultStatusCode.Success.ToDisplay(), ApiResultStatusCode.Success);
    }
}