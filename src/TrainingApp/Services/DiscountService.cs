using TrainingApp.Models;

namespace TrainingApp.Services;

public class DiscountService
{
    private readonly List<Coupon> _coupons =
    [
        new Coupon("SAVE10", 0.10m),
        new Coupon("SAVE20", 0.20m),
        new Coupon("VIP30", 0.30m),
    ];

    /// <summary>購入金額が10000円以上の場合、送料無料の対象になる。</summary>
    public bool IsEligibleForFreeShipping(decimal totalAmount) => totalAmount >= 10000m;

    /// <summary>クーポンコードに一致するクーポンを返す。見つからなければ null。</summary>
    public Coupon? FindCoupon(string code) =>
        _coupons.FirstOrDefault(c => c.Code == code);

    /// <summary>利用可能なクーポンコードの一覧を返す。</summary>
    public IReadOnlyList<string> GetAvailableCouponCodes() =>
        _coupons.Select(c => c.Code).ToList();
}
