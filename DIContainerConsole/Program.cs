using DIContainerConsole.Target;
using TinyDIContainer;
using System;

namespace DIContainerConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      // DIコンテナ登録
      DIContainer.Add<ITest1, Test1>();
      DIContainer.Add<ITest2, Test2>();


      // 利用例
      // Test1のインスタンス生成
      var test1 = DIContainer.CreateInstance<ITest1>();

      Console.WriteLine($"Test1.Test() [{test1.Test()}]");
      Console.WriteLine($"Test1.GetName() [{test1.GetName()}]");

      // Test2のインスタンス生成
      var test2 = DIContainer.CreateInstance<ITest2>();

      Console.WriteLine($"Test2.GetTest() [{test2.GetTest()}]");
      Console.WriteLine($"Test2.GetName() [{test2.GetName()}]");
    }
  }
}
