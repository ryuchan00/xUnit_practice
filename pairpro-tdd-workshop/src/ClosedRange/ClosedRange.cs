namespace PairProTdd;

/// <summary>
/// 整数の閉区間 [Lower, Upper] を表すクラス（回答例）。
///
/// これは reference 実装の一例です。TDD で辿り着く形は 1 つではありません。
/// メソッド名・等価性の実現方法・交差しない区間の扱いなどは、
/// ペアの議論次第で別解になり得ます。
/// </summary>
public sealed class ClosedRange : IEquatable<ClosedRange>
{
    public int Lower { get; }
    public int Upper { get; }

    public ClosedRange(int lower, int upper)
    {
        if (lower > upper)
        {
            throw new ArgumentException($"下端点が上端点より大きい区間は作れません: [{lower},{upper}]");
        }

        Lower = lower;
        Upper = upper;
    }

    /// <summary>指定した整数がこの区間に含まれるか（両端を含む）。</summary>
    public bool Includes(int value) => Lower <= value && value <= Upper;

    /// <summary>この区間が other を完全に含むか（自分自身も含むとみなす）。</summary>
    public bool Contains(ClosedRange other) =>
        Lower <= other.Lower && other.Upper <= Upper;

    /// <summary>2 つの区間が交差する（共通部分を持つ）か。</summary>
    public bool Intersects(ClosedRange other) =>
        Lower <= other.Upper && other.Lower <= Upper;

    /// <summary>
    /// 2 つの区間の共通部分（積集合）。交差しない場合は null を返す。
    /// </summary>
    public ClosedRange? Intersection(ClosedRange other)
    {
        if (!Intersects(other))
        {
            return null;
        }

        return new ClosedRange(Math.Max(Lower, other.Lower), Math.Min(Upper, other.Upper));
    }

    public override string ToString() => $"[{Lower},{Upper}]";

    public bool Equals(ClosedRange? other) =>
        other is not null && Lower == other.Lower && Upper == other.Upper;

    public override bool Equals(object? obj) => Equals(obj as ClosedRange);

    public override int GetHashCode() => HashCode.Combine(Lower, Upper);
}
