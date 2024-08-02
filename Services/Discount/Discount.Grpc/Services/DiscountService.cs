using eCommerceMicroservicesV2.Discount.Grpc.Data;
using eCommerceMicroservicesV2.Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using static Grpc.Core.Metadata;
using System.Globalization;

namespace eCommerceMicroservicesV2.Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var couponToList = await dbContext.Coupons
            .FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);

        if (couponToList is null)
        {
            logger.LogInformation(Messages.NO_COUPON_FOUND);

            couponToList = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };
        }

        logger.LogInformation(Messages.COUPON_FOUND);

        var couponModel = couponToList.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<CouponsModel> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
    {
        var couponsToList = await dbContext.Coupons.ToListAsync();

        if (couponsToList is null)
        {
            logger.LogInformation(Messages.NO_COUPON_FOUND);
        }

        logger.LogInformation(Messages.COUPON_FOUND);

        var couponsModel = couponsToList.Adapt<CouponsModel>();

        return couponsModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var couponToCreate = request.Coupon.Adapt<Coupon>();

        await dbContext.AddAsync(couponToCreate);
        var result = await dbContext.SaveChangesAsync() > 0;

        if (!result)
        {
            logger.LogError("Problem with saving coupon to CouponDB");

            throw new RpcException(new Status(StatusCode.Internal, "CouponDB problem occurred"));
        }

        return request.Coupon;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var couponToUpdate = await dbContext.Coupons
            .SingleOrDefaultAsync(coupon => coupon.ProductName == request.Coupon.ProductName);

        if (couponToUpdate is null)
        {
            logger.LogError("Coupon Not Found to update");

            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Argument"));
        }

        couponToUpdate.ProductName = request.Coupon.ProductName;
        couponToUpdate.Description = request.Coupon.Description;
        couponToUpdate.Amount = request.Coupon.Amount;

        dbContext.Coupons.Update(couponToUpdate);
        var result = await dbContext.SaveChangesAsync() > 0;

        if (!result)
        {
            logger.LogError("Problem with updating coupon from CouponDB");

            throw new RpcException(new Status(StatusCode.Internal, "CouponDB problem occurred"));
        }

        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var couponToDelete = await dbContext.Coupons
            .SingleOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);

        if (couponToDelete is null)
        {
            logger.LogError("Coupon Not Found to delete");

            throw new RpcException(new Status(StatusCode.NotFound, "Coupon Not Found to delete"));
        }

        dbContext.Remove(couponToDelete);
        var result = await dbContext.SaveChangesAsync() > 0;

        if (!result)
        {
            logger.LogError("Problem with deleting coupon from CouponDB");

            throw new RpcException(new Status(StatusCode.Internal, "CouponDB problem occurred"));
        }

        var response = new DeleteDiscountResponse()
        {
            IsSuccess = true
        };

        return response;
    }
}
