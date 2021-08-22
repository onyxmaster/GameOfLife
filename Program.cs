using System;

class Program
{
    static void Main(string[] args)
    {
        const int FieldWidth = 100;
        const int FieldHeight = 50;
        var field = new bool[FieldWidth, FieldHeight];
        field[4, 5] = true;
        var x = 6;
        var y = 1;
        while (true)
        {
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            for (int row = 0; row < FieldHeight; row++)
            {
                for (int column = 0; column < FieldWidth; column++)
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
                    if (x < FieldWidth - 1)
                        x++;
                    break;

                case ConsoleKey.UpArrow:
                    if (y > 0)
                        y--;
                    break;

                case ConsoleKey.DownArrow:
                    if (y < FieldHeight - 1)
                        y++;
                    break;

                case ConsoleKey.Spacebar:
                    field[x, y] = !field[x, y];
                    break;

                case ConsoleKey.N:
                    {
                        var newField = new bool[FieldWidth, FieldHeight];
                        for (int row = 0; row < FieldHeight; row++)
                        {
                            for (int column = 0; column < FieldWidth; column++)
                            {
                                var count = 0;
                                if (row > 0 && column > 0 && field[column - 1, row - 1])
                                {
                                    count++;
                                }

                                if (row > 0 && field[column, row - 1])
                                {
                                    count++;
                                }

                                if (row > 0 && column < FieldWidth - 1 && field[column + 1, row - 1])
                                {
                                    count++;
                                }

                                if (column < FieldWidth - 1 && field[column + 1, row])
                                {
                                    count++;
                                }

                                if (column < FieldWidth - 1 && row < FieldHeight - 1 && field[column + 1, row + 1])
                                {
                                    count++;
                                }

                                if (row < FieldHeight - 1 && field[column, row + 1])
                                {
                                    count++;
                                }

                                if (row < FieldHeight - 1 && column > 0 && field[column - 1, row + 1])
                                {
                                    count++;
                                }

                                if (column > 0 && field[column - 1, row])
                                {
                                    count++;
                                }

                                if (field[column, row])
                                {
                                    if (count == 2 || count == 3)
                                    {
                                        newField[column, row] = true;
                                    }
                                }
                                else
                                {
                                    if (count == 3)
                                    {
                                        newField[column, row] = true;
                                    }
                                }
                            }
                        }

                        field = newField;
                    }
                    break;
            }
        }
    }
}
