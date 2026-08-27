# Level 8: コードカバレッジのはかり方とベストプラクティス

Level1〜7 で「テストを書くこと」、付録のミューテーションテストで「テストの質」を扱いました。
この Level では、その中間にある **コードカバレッジ(どのコードがテストで実行されたか)** の
計測方法と、その数字との付き合い方を学びます。

穴埋め課題ではありません。実際に計測し、レポートを読み、テストが薄い場所を自分で見つけるのがゴールです。

---

## これから何をするか

1. `dotnet test --collect:"XPlat Code Coverage"` でカバレッジを計測する
2. ReportGenerator で HTML レポートに変換して読む
3. line coverage / branch coverage の差を確認する
4. カバレッジが低い、あるいは branch が漏れているクラスを見つけ、テストを 1〜2 本足して改善する

## なぜそうするか

カバレッジツールの一番の使いみちは、**テストが当たっていないコードを機械的に洗い出すこと**です
(Martin Fowler も「未テスト箇所を見つける道具」としての価値を強調しています)。

一方で、カバレッジは「そのコードが**実行された**」ことしか示さず、
「その振る舞いが**正しく検証された**」ことは保証しません。
`Assert` が一切なくても、メソッドを呼びさえすればカバレッジは上がります。
だからカバレッジは **必要条件であって十分条件ではない**、という前提で扱います。

---

## 計測方法

この教材のテストプロジェクトには `coverlet.collector` が既に入っているので、追加インストールなしで計測できます。

```bash
cd tests/TrainingApp.Tests
dotnet test --collect:"XPlat Code Coverage"
```

実行すると `TestResults/<GUID>/coverage.cobertura.xml` が出力されます。
`--collect:"XPlat Code Coverage"` は Coverlet のデータコレクターを指す決まった名前です
(`"Code Coverage"` だと .NET 組み込みのバイナリ形式コレクターになります)。

### HTML レポートに変換する

XML のままでは読みにくいので [ReportGenerator](https://github.com/danielpalme/ReportGenerator) で変換します。

```bash
# 初回のみ: グローバルツールとして導入
dotnet tool install -g dotnet-reportgenerator-globaltool

reportgenerator \
  -reports:"TestResults/**/coverage.cobertura.xml" \
  -targetdir:"coveragereport" \
  -reporttypes:Html

# coveragereport/index.html をブラウザで開く
```

> CI では ReportGenerator を dotnet ローカルツール(`dotnet-tools.json`)に加えておくと、
> `dotnet tool restore` だけで揃うので再現性が上がります。

### 計測対象から除外する

生成コードや自明なコードまで数えるとノイズになります。次のような単位で除外できます。

- **属性で除外**: クラスやメソッドに `[ExcludeFromCodeCoverage]`(`System.Diagnostics.CodeAnalysis`)を付ける
- **設定ファイルで除外**: `.runsettings` や `coverlet.runsettings` で
  `ExcludeByFile` / `ExcludeByAttribute` / `Exclude`(アセンブリ・名前空間単位)を指定する

```xml
<!-- coverlet.runsettings -->
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <DataCollectionRunSettings>
    <DataCollectors>
      <DataCollector friendlyName="XPlat Code Coverage">
        <Configuration>
          <ExcludeByAttribute>GeneratedCodeAttribute,ExcludeFromCodeCoverageAttribute</ExcludeByAttribute>
          <ExcludeByFile>**/Migrations/**/*.cs</ExcludeByFile>
        </Configuration>
      </DataCollector>
    </DataCollectors>
  </DataCollectionRunSettings>
</RunSettings>
```

```bash
dotnet test --collect:"XPlat Code Coverage" --settings coverlet.runsettings
```

### しきい値でビルドを落とす(MSBuild 版 Coverlet)

```bash
dotnet test -p:CollectCoverage=true -p:CoverletOutputFormat=cobertura \
  -p:Threshold=80 -p:ThresholdType=line -p:ThresholdType=branch
```

---

## カバレッジ指標の種類

| 指標 | 何を数えるか | 補足 |
|---|---|---|
| **line coverage** | 実行された行の割合 | 一番ゆるい。1 行に複数分岐があると見逃す |
| **branch coverage** | 各条件の true/false 両方を通ったか | line より厳しく、示唆に富む。まずここを見る |
| **method coverage** | 1 度でも呼ばれたメソッドの割合 | 未使用コードの発見向け |
| condition / MC/DC | 条件式の各項の組み合わせ | 高信頼性システムが必要とする厳格な指標。通常のアプリでは過剰なことが多い |

`if (a && b)` のような行は、line coverage では 100% でも branch coverage では 50% になり得ます。
**line だけ見て安心しない**のが第一歩です。

---

## ベストプラクティス

### 1. 目標値(ターゲット)にしない

> カバレッジをターゲットにすると、人はその数字を満たしにいく — Martin Fowler

数字合わせのための中身の薄いテストが増え、本来の目的(バグを減らす・変更を怖くなくする)から離れます。
カバレッジは「達成する数字」ではなく「眺めて穴を探す地図」として使います。

### 2. 妥当なレンジの目安

- **80〜90%台**が現実的な上限。ここを超えると費用対効果が急落する
- **100% は疑わしい**。到達させるためだけの不自然なテストが混ざっているサイン
- **50% を下回る**なら要警戒。テスト文化そのものを見直す

数値そのものより、「本番のバグが減ったか」「リファクタが怖くなくなったか」で効果を測ります。

### 3. チームが自分で決め、少しずつ上げる

会社一律の閾値を上から強制しない(Google のガイダンスも同様の立場)。
チームが対象コードの性質を踏まえて目標を決め、時間をかけて引き上げていく方が機能します。

### 4. ゲートにするなら「絶対値」より「差分」

CI で強制するなら、リポジトリ全体の絶対%ではなく次を条件にする方が痛みが少なく効果的です。

- **新規・変更行のカバレッジ(patch / diff coverage)** が一定以上
- 既存カバレッジを**下げない**(リグレッション防止)

### 5. カバーする優先順位をつける

全体を均一に上げようとしない。**変更が多い / 金銭・安全のリスクがある / システムをつなぐ結合点**の
コードから優先的にカバーする。「どの 80% か」が重要で、「とにかく 80%」ではない。

### 6. カバレッジ 100% でもアサーションが弱ければ無意味

カバレッジは「実行されたか」だけ。「正しく検証したか」は測れない。
そこを補うのが付録の**ミューテーションテスト**です。
カバレッジ(広さ)とミューテーションスコア(深さ)はセットで見る。

### 7. ノイズを除外して信号を上げる

DTO・自動生成コード・`Program.cs` のブートストラップ・EF マイグレーションなどは除外する。
分母がきれいになると「本当に手が回っていないロジック」が見えやすくなる。

---

## 課題

1. `dotnet test --collect:"XPlat Code Coverage"` を実行し、HTML レポートを生成する
2. `src/TrainingApp/Services/` の各クラスの **branch coverage** を確認する
3. line は高いのに branch が低いクラスを 1 つ選び、通っていない分岐を読み解く
4. その分岐を通すテストを 1〜2 本追加し、再計測して branch coverage が上がることを確認する
5. 余力があれば、そのクラスに `dotnet stryker`(付録)をかけ、
   カバレッジを埋めたことでミューテーションスコアも上がったかを見る

> カバレッジが 100% になっても、それは「テストが完璧」ではなく「未実行のコードが無い」だけ、
> ということを最後に思い出してください。
