# Level 2: いろいろな Assert の種類

## これから何をするか

`Assert.Equal` 以外の検証メソッド(`True` / `False` / `Null` / `NotNull` / `Contains` など)を、場面に応じて使い分けます。

## なぜそうするか

適切な Assert メソッドを選ぶことで、テストが失敗したときのエラーメッセージがより具体的になり、原因調査がしやすくなります。

## サンプルコード

```csharp
[Fact]
public void FindUser_存在するIDを渡すと_ユーザーが見つかる()
{
    // Arrange
    var repository = new UserRepository();

    // Act
    var actual = repository.FindById(1);

    // Assert
    // Nullではないことを検証
    Assert.NotNull(actual);
    // Trueであることを検証
    Assert.True(actual.IsActive);
}
```
