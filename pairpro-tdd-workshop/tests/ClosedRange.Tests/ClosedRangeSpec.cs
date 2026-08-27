using PairProTdd;

namespace PairProTdd.Tests;

/// <summary>
/// 整数の閉区間クラスのテスト（回答例）。
///
/// docs/お題-整数の閉区間.md の要件リストを一通りテストにしたもの。
/// 研修本番では、これらを 1 本ずつ Red -> Green -> Refactor で書いていく。
/// ここでは到達点として全部そろえてある。
/// </summary>
public class ClosedRangeSpec
{
    // --- 基本 ---

    [Fact]
    public void 閉区間は下端点と上端点を持つ()
    {
        var range = new ClosedRange(3, 8);

        Assert.Equal(3, range.Lower);
        Assert.Equal(8, range.Upper);
    }

    [Fact]
    public void 文字列表現を返す()
    {
        Assert.Equal("[3,8]", new ClosedRange(3, 8).ToString());
    }

    [Fact]
    public void 下端点と上端点が等しい区間は作れる()
    {
        var range = new ClosedRange(5, 5);

        Assert.Equal(5, range.Lower);
        Assert.Equal(5, range.Upper);
    }

    [Fact]
    public void 下端点が上端点より大きい区間は作れない()
    {
        Assert.Throws<ArgumentException>(() => new ClosedRange(8, 3));
    }

    // --- 判定 ---

    [Theory]
    [InlineData(3, true)]
    [InlineData(8, true)]
    [InlineData(5, true)]
    [InlineData(2, false)]
    [InlineData(9, false)]
    public void 整数が区間に含まれるかを判定する(int value, bool expected)
    {
        Assert.Equal(expected, new ClosedRange(3, 8).Includes(value));
    }

    [Fact]
    public void 区間どうしの等価性を判定する()
    {
        Assert.Equal(new ClosedRange(3, 8), new ClosedRange(3, 8));
        Assert.NotEqual(new ClosedRange(3, 8), new ClosedRange(3, 9));
    }

    [Fact]
    public void 区間が別の区間を完全に含むかを判定する()
    {
        var range = new ClosedRange(3, 8);

        Assert.True(range.Contains(new ClosedRange(4, 6)));
        Assert.True(range.Contains(new ClosedRange(3, 8)));   // 自分自身
        Assert.False(range.Contains(new ClosedRange(4, 9)));
    }

    // --- 発展 ---

    [Fact]
    public void 区間どうしが交差するかを判定する()
    {
        var range = new ClosedRange(3, 8);

        Assert.True(range.Intersects(new ClosedRange(6, 12)));
        Assert.False(range.Intersects(new ClosedRange(9, 12)));
    }

    [Fact]
    public void 区間どうしの共通部分を求める()
    {
        var range = new ClosedRange(3, 8);

        Assert.Equal(new ClosedRange(6, 8), range.Intersection(new ClosedRange(6, 12)));
        Assert.Null(range.Intersection(new ClosedRange(9, 12)));
    }
}
