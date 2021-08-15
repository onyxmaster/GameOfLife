using System;

class Program
{
    static void Main(string[] args)
    {
        var fieldWidth = 40;
        var fieldHeight = 30;
        var field = new bool[fieldWidth, fieldHeight];
        field[4, 5] = true;
        var x = 6;
        var y = 1;
        while (true)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            for (int row = 0; row < fieldHeight; row++)
            {

                for (int column = 0; column < fieldWidth; column++)
                {
                    if (field[column, row])
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.CursorLeft = x;
            Console.CursorTop = y;

            var k = Console.ReadKey();
            switch (k.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (x > 0)
                        x--;
                    break;

                case ConsoleKey.RightArrow:
                    if (x < fieldWidth - 1)
                        x++;
                    break;
                case ConsoleKey.UpArrow:
                    if (y > 0)
                        y--;
                    break;

                case ConsoleKey.DownArrow:
                    if (y < fieldHeight - 1)
                        y++;
                    break;
                case ConsoleKey.Spacebar:
                field[x,y]=!field [x,y];
                        
                    break;
            }
        }
    }
}
