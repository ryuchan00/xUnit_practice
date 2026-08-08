# xUnit 穴埋め研修

新人〜ミドルエンジニア向けの、xUnit / .NET 10 を使った「穴埋め形式」のユニットテスト研修教材です。
対象読者は「テストダブル(モック・スタブなど)が全く分からない」レベルを想定しています。

## イントロダクション: テストフレームワークの系譜(xUnit系 / BDD系)

この教材で使う xUnit.net をはじめ、世の中のテストフレームワークは大きく **xUnit系** と **BDD系** の2つの潮流に分けられます。どちらも「テストコードを書いて検証する」こと自体は同じですが、設計思想と書き方(構文)が異なります。

### xUnit系

Kent Beck が Smalltalk 向けに作った SUnit を源流とし、「テストクラス」に「テストメソッド」を並べ、`Assert.Equal(expected, actual)` のような**プログラマ向けの検証**を行うスタイルです。この教材の Level1〜7(3Bを含む)はすべてこの書き方です。

| フレームワーク | 言語 |
|---|---|
| **xUnit.net** | .NET (この教材で使用) |
| **NUnit** | .NET |
| **JUnit** | Java |
| **PHPUnit** | PHP |
| **pytest** | Python |

### BDD系(Behavior-Driven Development)

「テスト」ではなく「仕様(Specification)」を書く、という発想のスタイルです。`describe` / `context` / `it` (あるいは Gherkin 記法の `Given` / `When` / `Then`) を使って、**人が読んでも仕様として理解できる**形でテストを書きます。非エンジニアのステークホルダーとも仕様を共有しやすい、という狙いがあります。

| フレームワーク | 言語 | 備考 |
|---|---|---|
| **RSpec** | Ruby | BDD系の源流にあたる代表的フレームワーク |
| **JSSpec** | JavaScript | RSpec に影響を受けた初期の JS BDD ツール。後述の Jasmine の前身にあたる |
| **Jasmine** | JavaScript | `describe`/`it` 構文の初期の代表例 |
| **Mocha + Chai** | JavaScript | Mocha(実行) + Chai(`expect(x).to.equal(y)` のようなBDD風アサーション) |
| **Cucumber** | 多言語対応 | Gherkin(`Given`/`When`/`Then`)でシナリオを書く、非エンジニアも読める形式 |
| **JBehave** | Java | Cucumber と同様 Gherkin ライクな記法で書ける、Java向けBDDフレームワークの草分け的存在 |
| **SpecFlow / Reqnroll** | .NET | .NET 版 Cucumber。Gherkin の feature ファイルを使う |

### ハイブリッドな例: Jest / Vitest

**Jest**(JS/TS)や **Vitest**(JS/TS、Vite向け)は、`describe`/`it` という BDD系の構文を採用していますが、アサーションは `expect(actual).toBe(expected)` のように xUnit 系に近い書き方です。「構文はBDDから借りているが、思想としてはxUnit系寄り」のハイブリッドと捉えるとわかりやすいです。

### まとめ

- **どちらが優れているという話ではなく、チームの文化や対象読者(エンジニアのみか、非エンジニアとも仕様を共有したいか)によって向き不向きがあります**。
- この教材は .NET の標準的な選択肢である xUnit.net を使い、xUnit系の基本の書き方(3Aパターン)を身につけることを目的としています。

## 進め方

1. `tests/TrainingApp.Tests/` 配下の各テストファイルを開く
2. コード中の `// 穴埋め: ...` というコメントの箇所だけを書き換える
3. **`src/TrainingApp/` 配下のプロダクションコードは変更しないこと**
4. `dotnet test` を実行し、テストが Green(成功) になれば完了

現在の状態では、多くのテストは意図的に **失敗する(赤)** ようになっています。
すべて正しく実装すると 40 件すべて成功することを確認済みです。

```bash
dotnet test
```

### Docker で実行する場合

ローカルに .NET SDK をインストールしなくても、Docker があればテストを実行できます。

```bash
docker compose run --rm test
```

`src/` と `tests/` はコンテナにバインドマウントされるため、ホスト側のエディタでテストコードを編集し、そのままコンテナ内で `dotnet test` を実行できます。

特定の Level だけ実行したい場合は、コマンドを明示的に指定します(`Dockerfile` の `CMD` はデフォルトの `dotnet test` を隠蔽していないので、そのまま上書きできます)。

```bash
docker compose run --rm test dotnet test --filter "FullyQualifiedName~TrainingApp.Tests.Level1_SimpleAssert"
```

## ディレクトリ構成

```
src/TrainingApp/            対象となるプロダクションコード
  Services/                 CalculatorService, DiscountService, ShoppingCartService, GreetingService, OrderService, ...
  Controllers/               ProductsController
  External/                  外部API呼び出しを抽象化したインターフェースと本番実装
  Models/                    Product, Coupon, CartItem, OrderResult など

tests/TrainingApp.Tests/    穴埋めテスト(研修課題)
  Level1_SimpleAssert/       Level 1
  Level2_AssertTypes/        Level 2
  Level3_StateChange/        Level 3
  Level3B_TimeDependency/    Level 3B (時間に依存するコードのテスト)
  Level4_ControllerSpec/     Level 4
  Level5_TestDoubles/        Level 5 (ダミー/スタブ/モック/スパイ/フェイク)
  Level6_OrderingAndParallelization/  Level 6 (実行順序・並列化)
  Level7_IntegrationTesting/  Level 7 (WebApplicationFactoryによる結合テスト)
  Infrastructure/            テスト順序制御・共有フィクスチャなどの補助コード

Dockerfile / docker-compose.yml   Docker でテストを実行するための構成
dotnet-tools.json                 Stryker.NET などのローカルツールのマニフェスト
```

## テストに関する前提知識: テストピラミッドとテストサイズ

演習に入る前に、「ユニットテスト」がテスト全体の中でどの位置づけにあるのかを整理しておきます。

### テストピラミッド / テストトロフィー

- **テストピラミッド**: 「実行が速く安価なユニットテストを土台に大量に持ち、実行が遅く高価な結合テスト・E2Eテストは少数に絞る」という古典的な考え方。下から ユニット → 結合 → E2E の順に積み上がる三角形で表される。
- **テストトロフィー** (Kent C. Dodds 氏が提唱): フロントエンド開発などモジュール間の結合が多い領域では、細かすぎるユニットテストよりも「結合テスト」に比重を置いたほうが費用対効果が高いという考え方。ユニット層を薄く、結合層を厚くしたトロフィー(優勝カップ)型の比率になる。

どちらが正しいという話ではなく、**対象システムの特性によって最適なバランスは変わる**、という点が重要です。この教材(Level1〜4)は、まず基本となるユニットテスト(テストピラミッドの土台)の書き方を身につけることを目的としています。

### 「単体テスト」「結合テスト」の定義はチームによって違う

Level 4 の `ProductsControllerTests` は、依存(`IProductRepository`)をモックに差し替えているため、この教材では便宜上「ユニットテスト」として扱っています。しかし、「Controller のテストは(HTTPパイプラインやDIコンテナを含めて検証しないと)結合テストとは呼べない」と考えるチームもあれば、「モックに差し替えていればユニットテスト」と考えるチームもあり、**「単体テスト」「結合テスト」という言葉の境界はチームや文脈によってかなり揺れます**。

日本語で著名なテストの実践者である t_wada(和田卓人)氏は、この曖昧さを踏まえて、Google のテスト文化などで使われる **「テストサイズ」** という尺度(Small / Medium / Large)を紹介しています。これは「単体/結合」という開発者の主観に依存しがちな分類の代わりに、「外部プロセス通信の有無」「ネットワーク越しの通信の有無」「実行時間」といった**客観的な基準**でテストを分類する考え方です。

- **Small**: 単一プロセス内で完結し、ネットワーク・ディスクI/O・外部プロセスに依存しない(この教材の Level1〜3, Level5 がこれに近い)
- **Medium**: 同一マシン内の別プロセス(ローカルDBなど)とは通信してよい
- **Large**: ネットワーク越しの外部サービスとの通信を含む、E2Eに近いテスト

チームで「単体テスト」という言葉を使うときは、まず「どのサイズのテストを指しているのか」を揃えておくと、認識のズレによる手戻りを防げます。

## 各レベルの内容

### Level 1: かんたんな Assert
`CalculatorServiceTests.cs`

3A パターン(Arrange-Act-Assert)の基本の型を身につける。`Assert.Equal` と `Assert.Throws` を使う。

### Level 2: いろいろな Assert の種類
`DiscountServiceTests.cs`

`Assert.True` / `Assert.False` / `Assert.Null` / `Assert.NotNull` / `Assert.Contains` など、場面に応じた Assert メソッドの使い分けを学ぶ。

### Level 3: Act した後の状態変化を確認する
`ShoppingCartServiceTests.cs`

戻り値だけでなく、Act の実行によってオブジェクトの「状態」がどう変わるかを検証する。

### Level 3B: 時間に依存するコードのテスト
`GreetingServiceTests.cs`

`GreetingService` は「現在時刻」によって結果が変わるロジックを持っています。もし `DateTime.Now` を直接呼び出していたら、テストは実行するタイミングによって結果が変わってしまう不安定なテスト(flaky test)になります。

`GreetingService` はコンストラクタで `TimeProvider`(.NET 8 で追加された標準の時刻抽象化クラス)を受け取るように作られており、テスト側では `Microsoft.Extensions.TimeProvider.Testing` パッケージの `FakeTimeProvider` を使って時刻を固定(freeze)してから検証します。時刻を固定しない限りテストの前提が成立しないため、「なぜテストのために時刻を注入・固定できる設計にするのか」を体感できる内容になっています。

### Level 4: Controller のテスト(Controller Spec)
`ProductsControllerTests.cs`

ASP.NET Core の Controller をテストし、`OkObjectResult` / `NotFoundResult` / `CreatedAtActionResult` などの `ActionResult` を検証する。依存関係(リポジトリ)は Moq でモック化する。

> 依存をすべてモックに差し替えて Controller クラス単体を直接 `new` しているため、この教材では「ユニットテスト」に分類しています。ただし、チームによってはこれを「結合テスト」と呼ぶ場合もあります(詳しくは前述の「単体テスト・結合テストの定義はチームによって違う」を参照)。

### Level 5: テストダブル

すべて「外部の決済API」を模した `IPaymentGateway`(と関連する `IOrderNotifier` / `IReceiptPrinter`)を題材にしています。

| 種類 | ファイル | 特徴 |
|---|---|---|
| ダミー (Dummy) | `DummyExampleTests.cs` | 引数として必要だが、テスト対象のロジックの中では一切使われないオブジェクト |
| スタブ (Stub) | `StubExampleTests.cs` | あらかじめ決められた戻り値を返すだけ。呼ばれ方は検証しない |
| モック (Mock) | `MockExampleTests.cs` | 「呼ばれるはずだ」という期待を事前に設定し、事後に `Verify` で検証する |
| スパイ (Spy) | `SpyExampleTests.cs` | 呼び出しの記録を自分で保持しておき、事後にその記録を検証する(手書き実装) |
| フェイク (Fake) | `FakeExampleTests.cs` | 簡易的だが実際に動くロジックを持つ、ミニチュア版の本物 |

#### なぜ外部APIをユニットテストに含めないのか

`src/TrainingApp/External/` には `PaymentGateway`(HTTP経由で本物の決済APIを呼ぶ)などの本番実装がありますが、これらは**ユニットテストの対象に含めていません**。理由は主に2つです。

1. **フレーキーなテストになるため**
   ネットワークやサードパーティのサービスの状態に依存するテストは、コードが正しくても、ネットワーク遅延・サービス障害・レート制限などによって失敗することがあります(いわゆる flaky test)。これが混ざると「テストが落ちた」ときにコードのバグなのか環境の問題なのか切り分けが難しくなり、テストスイート全体の信頼性が下がります。

2. **責任分離ができていないため**
   ユニットテストは「そのクラス/メソッド自身のロジックが正しいか」を検証するものです。外部APIの実装まで含めてテストしてしまうと、「自分のコードのバグ」と「外部サービス側の問題」が同じテストの中に混在してしまい、テストの目的(何を保証したいか)が曖昧になります。

外部サービスとの結合部分は、ユニットテストではなく **結合テスト(Integration Test)** や **E2Eテスト** で別途検証するべきものです。ユニットテストでは、`IPaymentGateway` のようなインターフェースを介してテストダブルに差し替えることで、「自分のコード(OrderService)のロジックだけ」を高速かつ安定して検証します。

### Level 6: テストの実行順序と並列化

#### 6-A: カスタム属性による実行順序の指定
`OrderedExecutionTests.cs` / `Infrastructure/TestPriorityAttribute.cs` / `Infrastructure/PriorityOrderer.cs`

xUnit は本来「テストの実行順序に依存しない」ことを前提にしていますが、シナリオテストなど順序に意味があるケースのために、`ITestCaseOrderer` を自作して `[TestPriority]` 属性の値でテストを並び替える方法を学ぶ。

#### 6-B: テストの並列化とコレクション
`ParallelCollectionExampleTests.cs` / `Infrastructure/SharedCounterFixture.cs`

xUnit はデフォルトで異なるテストクラスを並列実行します。共有状態を持つテストクラス同士を `[Collection("...")]` で同じコレクションにまとめることで、並列実行を防ぎ、安全に状態を共有する方法を学ぶ。
アセンブリ全体の並列度は `tests/TrainingApp.Tests/xunit.runner.json` で設定している。

### Level 7: DIコンテナと WebApplicationFactory を使った結合テスト
`ProductsApiFactory.cs` / `ProductsEndpointTests.cs`

Level4 では `ProductsController` を直接 `new` してテストしました(コンストラクタにモックを手動で注入)。Level7 ではさらに一歩進み、`WebApplicationFactory<Program>` を使ってアプリを実際に起動し、実際の HTTP エンドポイント(`GET`/`POST`)にリクエストを送って検証します。

- ルーティング・モデルバインディング・JSONシリアライズまで含めて動作を確認できる、より「本物」に近いテストです
- `Program.cs` に登録済みの本物の `IProductRepository` 実装を、DIコンテナのレベルでモックに差し替えます(`services.RemoveAll<IProductRepository>()` → `services.AddSingleton(mock.Object)`)
- `ProductsApiFactory` は xUnit の **Fixture**(`IClassFixture<T>` で共有されるセットアップ用オブジェクト)です。Level6 の `SharedCounterFixture` は完成品を「使う」だけでしたが、Level7 では `ConfigureWebHost` の中身を自分で実装することで、Fixture を「作る」体験もできます

> このテストは Level4 よりも実行が遅く、依存の差し替えもDIコンテナ経由と手間が増えます。前述の「テストサイズ」で言えば Level4 が Small に近いのに対し、Level7 は実際のHTTPスタックを起動する分 Medium に近づきます。「速くて安価なテストを土台に、遅くて本物に近いテストを少数だけ持つ」というテストピラミッドの考え方を、Level4 と Level7 の対比で体感できます。

## 使用技術

- .NET 10 / ASP.NET Core Web API
- xUnit 2.5
- Moq 4.20 (モック/スタブの作成)
- Microsoft.Extensions.TimeProvider.Testing (時刻のフェイク)
- Microsoft.AspNetCore.Mvc.Testing (WebApplicationFactoryによる結合テスト)
- Stryker.NET 4.16 (ミューテーションテスト)
- Docker / Docker Compose (任意)

## 付録: ミューテーションテストによるテストの質の検証

Level1〜7 では「テストを書くこと」自体を学びましたが、書いたテストが本当にバグを検知できるのか?は別の問題です。たとえば `Assert.NotNull(actual)` だけでは、`actual` の中身が間違っていてもテストは気づけません。

**ミューテーションテスト**は、プロダクションコードにわざと小さなバグ(ミュータント)を注入し、既存のテストがそのバグを検知して落ちるかどうかを機械的にチェックする手法です。テストが検知できたミュータントの割合を「ミューテーションスコア」と呼び、これがテストカバレッジよりも「テストの実効性」に近い指標になります。

これまでの Level1〜7 とは違い、穴埋めをすればクリアという単純な課題ではなく、**自分の書いたテストの弱点を自分で見つけて補強する**という、テストに対する一段深い視点が必要になります。研修の中でも一番難易度が高い内容のため、付録という位置づけにしています。

この教材では [Stryker.NET](https://stryker-mutator.io/docs/stryker-net/introduction/) を dotnet ローカルツールとして導入しています。

```bash
# 初回のみ: ツールをインストール
dotnet tool restore

# Level1〜6(3Bを含む)をすべて解いて全テストが Green の状態で実行する
cd tests/TrainingApp.Tests
dotnet stryker
```

Docker で実行する場合は、イメージのビルド時に `dotnet tool restore` 済みなので、そのまま `dotnet stryker` を実行できます。

```bash
docker compose run --rm test dotnet stryker
```

対象は `stryker-config.json` で `CalculatorService` / `DiscountService` / `ShoppingCartService` に絞っています(実行時間を抑えるため)。実行後、生存した(検知できなかった)ミュータントがあれば、それを殺せるように Level1〜3 で書いた Assert を見直すのが課題です。

> **注意**: ミューテーションテストは実行に時間がかかります(対象クラスやテスト数に応じて数分〜)。まずは Level1〜6 をすべて Green にしてから実行してください。テストが1つでも失敗した状態では Stryker は実行できません(初期テスト実行の失敗率が高いと解析を中断する仕組みになっています)。
