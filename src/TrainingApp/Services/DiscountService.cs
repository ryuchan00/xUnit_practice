using TrainingApp.Models;

namespace TrainingApp.Services;

public class DiscountService
{
    /// <summary>送料無料になる購入金額のしきい値。</summary>
    private const decimal FreeShippingThreshold = 10000m;

    private readonly IReadOnlyList<Coupon> _coupons;

    /// <summary>利用可能なクーポンの一覧を受け取る。どのクーポンが存在するかは呼び出し側が決める。</summary>
    public DiscountService(IReadOnlyList<Coupon> coupons) => _coupons = coupons;

    /// <summary>購入金額が10000円以上の場合、送料無料の対象になる。</summary>
    public bool IsEligibleForFreeShipping(decimal totalAmount) => totalAmount >= FreeShippingThreshold;

    /// <summary>クーポンコードに一致するクーポンを返す。見つからなければ null。</summary>
    public Coupon? FindCoupon(string code) =>
        _coupons.FirstOrDefault(c => c.Code == code);

    /// <summary>利用可能なクーポンコードの一覧を返す。</summary>
    public IReadOnlyList<string> GetAvailableCouponCodes() =>
        _coupons.Select(c => c.Code).ToList();
}
