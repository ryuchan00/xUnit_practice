# 研修用テスト実行コンテナ。
# ローカルに .NET SDK をインストールしなくても `docker compose run test` で
# 穴埋めテストを実行できるようにするためのイメージ。
FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /workspace

# 先にプロジェクトファイルだけコピーして restore することで、
# ソース変更時の再ビルドを高速化する(Dockerレイヤーキャッシュ)。
COPY xunit-practice.sln ./
COPY src/TrainingApp/TrainingApp.csproj src/TrainingApp/
COPY tests/TrainingApp.Tests/TrainingApp.Tests.csproj tests/TrainingApp.Tests/
RUN dotnet restore

COPY . .

ENTRYPOINT ["dotnet", "test"]
