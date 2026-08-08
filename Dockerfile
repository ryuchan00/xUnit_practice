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

# Stryker.NET などのローカルツール(dotnet-tools.json)もイメージ内で復元しておく。
COPY dotnet-tools.json ./
RUN dotnet tool restore

COPY . .

# ENTRYPOINT ではなく CMD にすることで、コマンドを隠蔽せず
# `docker compose run --rm test dotnet test ...` のように明示的に実行できるようにする。
CMD ["dotnet", "test"]
