using Raylib_cs;

namespace Radius2D
{
    class Program
    {
        static void Main()
        {
            int num = 0;

            for (var i = 0; i <= 50; i++)
            {
                num += i;
                Console.WriteLine(i);
            };

            Console.WriteLine(num);
        }
    }
}