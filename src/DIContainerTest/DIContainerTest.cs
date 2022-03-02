using DIContainerTest.Target;
using System;
using TinyDIContainer;
using Xunit;

namespace DIContainerTest
{
  public class DIContainerTest : IDisposable
  {
    static bool Setuped = false;
    /// <summary>
    /// Setup
    /// </summary>
    public DIContainerTest()
    {
      if (!Setuped)
      {
        // DIコンテナ登録
        DIContainer.Add<ITest1, Test1>();
        DIContainer.Add<ITest2, Test2>();
        Setuped = true;
      }
    }

    /// <summary>
    /// Teardown
    /// </summary>
    public void Dispose()
    {
    }

    [Fact(DisplayName = "正常系：インスタンス生成")]
    public void NormalCreateTest()
    {
      var instance = DIContainer.CreateInstance<ITest1>();
      Assert.True(instance is Test1);
    }

    [Fact(DisplayName = "正常系：プロパティ")]
    public void NormalPropertyTest()
    {
      var instance = DIContainer.CreateInstance<ITest1>();
      Assert.Equal("Test1", instance.GetName());
      Assert.Equal("Test！", instance.Test());
    }

    [Fact(DisplayName = "異常系：既存項目の追加")]
    public void FailAddTest()
    {
      var ex = Assert.Throws<ArgumentException>(() =>
      {
        DIContainer.Add<ITest2, Test2>();
      });
      Assert.Equal("An item with the same key has already been added. Key: DIContainerTest.Target.ITest2", ex.Message);
    }

    [Fact(DisplayName = "異常系：インターフェイス未継承クラスの追加")]
    public void FailAddNotInheritanceTest()
    {
      var ex = Assert.Throws<Exception>(() =>
      {
        DIContainer.Add<IDummy, Test2>();
      });
      Assert.Equal("IDummy,Test2 Is Combination error", ex.Message);
    }

    [Fact(DisplayName = "異常系：存在しない項目のインスタンス生成")]
    public void FailCreateNotExitsTest()
    {
      var ex = Assert.Throws<Exception>(() =>
      {
        DIContainer.CreateInstance<IDummy>();
      });
      Assert.Equal("IDummy Is Not Exists", ex.Message);
    }
  }
}
