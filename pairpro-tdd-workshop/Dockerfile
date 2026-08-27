# ペアプロ TDD 研修用のテスト実行コンテナ。
# ローカルに .NET 10 SDK が無くても `docker compose run --rm tdd` で
# `dotnet test` を回せるようにするためのイメージ。
FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /workspace

# 先にプロジェクトファイルだけコピーして restore し、Docker レイヤーキャッシュを効かせる。
COPY PairProTdd.sln ./
COPY src/ClosedRange/ClosedRange.csproj src/ClosedRange/
COPY tests/ClosedRange.Tests/ClosedRange.Tests.csproj tests/ClosedRange.Tests/
RUN dotnet restore

COPY . .

CMD ["dotnet", "test"]
