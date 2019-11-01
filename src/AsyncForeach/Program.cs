using System;
using System.Threading.Tasks; // Task
using System.Threading; // CancellationToken

namespace AsyncForeach
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await foreach (var x in new A()) { Console.WriteLine($"{x}"); }
            await foreach (var x in new B()) { Console.WriteLine($"{x}"); }
        }
        struct A
        {
            // Enumerable必須
            public A GetAsyncEnumerator() => this;
         
            // Enumerator必須
            public int Current => 0;
            public ValueTask<bool> MoveNextAsync()
            {
                Console.WriteLine("MoveNextAsync");
                return new ValueTask<bool>(false);
            }
         
            // 未実装なら呼ばれない
            public ValueTask DisposeAsync()
            {
                Console.WriteLine("DisposeAsync");
                return default;
            }
        }
        struct B
        {
            // 可変長引数可
            public B GetAsyncEnumerator(params int[] dummy) => this;
            public int Current => 0;
            // オプション引数可
            public ValueTask<bool> MoveNextAsync(CancellationToken token = default) => default;
        }
    }
}
