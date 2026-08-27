using PairProTdd;

namespace PairProTdd.Tests;

/// <summary>
/// 整数の閉区間クラスのテスト。
///
/// このファイルは「ひな形」です。
/// - 最初の 1 本だけ、実際に動く（そして今は失敗する）テストが書いてあります。
/// - 残りは Skip 付きのプレースホルダです。要件リスト(docs/お題-整数の閉区間.md)の
///   どれに対応するかだけ書いてあります。上から順にやる必要はありません。
///   「次に何をテストするか」をペアで相談して決めてください。
///
/// 進め方（Red -> Green -> Refactor）:
///   1. Skip を外す / 新しいテストを 1 本書く            -> ビルドが通り、テストが失敗する(Red)
///   2. そのテストを通す最小限の実装を src に書く         -> テストが通る(Green)
///   3. テストが緑のまま、実装とテストの重複や名前を整える -> (Refactor)
///   4. 1 に戻る
///
/// テスト実行: このディレクトリで `dotnet test`
/// </summary>
public class ClosedRangeSpec
{
    [Fact]
    public void 閉区間は下端点と上端点を持つ()
    {
        var range = new ClosedRange(3, 8);

        Assert.Equal(3, range.Lower);
        Assert.Equal(8, range.Upper);
    }

    [Fact(Skip = "要件: 文字列表現。[3,8] は \"[3,8]\" と表現される")]
    public void 文字列表現を返す()
    {
    }

    [Fact(Skip = "要件: 下端点 == 上端点 の区間 [5,5] は作れる")]
    public void 下端点と上端点が等しい区間は作れる()
    {
    }

    [Fact(Skip = "要件: 下端点 > 上端点 の区間は作れない（例外を投げる）")]
    public void 下端点が上端点より大きい区間は作れない()
    {
    }

    [Fact(Skip = "要件: 指定した整数が区間に含まれるか判定できる（両端を含む）")]
    public void 整数が区間に含まれるかを判定する()
    {
    }

    [Fact(Skip = "要件: 2 つの区間が等価か判定できる。[3,8] と [3,8] は等しい")]
    public void 区間どうしの等価性を判定する()
    {
    }

    [Fact(Skip = "要件: ある区間が別の区間を完全に含むか判定できる。[3,8] は [4,6] を含む")]
    public void 区間が別の区間を完全に含むかを判定する()
    {
    }

    [Fact(Skip = "発展: 2 つの区間が交差するか判定できる")]
    public void 区間どうしが交差するかを判定する()
    {
    }

    [Fact(Skip = "発展: 2 つの区間の共通部分（積集合）を求められる")]
    public void 区間どうしの共通部分を求める()
    {
    }
}
