using System;
using System.IO;

static class Program
{
    static uint[,] _field;
    static int _x, _y;
    static Rules _rules;

    enum Rules
    {
        Conway,
        Avgust,
        Ulam,
        HighLife,
        AvgustConway,
        LifeWithoutDead,
        DayAndNight,
        Avgust2,
        Wind,
        StableLife,
        NotStableLife,
        HardLife,
    }

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        const int FieldWidth = 70;
        const int FieldHeight = 40;
        _field = new uint[FieldWidth, FieldHeight];
        _x = 20;
        _y = 20;
        _rules = Rules.NotStableLife;
        while (true)
        {
            DrawField();
            ProcessInput();
        }
    }
    static void ProcessInput()
    {
        var k = Console.ReadKey(true);
        switch (k.Key)
        {
            case ConsoleKey.LeftArrow:
                if (_x > 0)
                    _x--;
                break;

            case ConsoleKey.RightArrow:
                if (_x < _field.GetLength(0) - 1)
                    _x++;
                break;

            case ConsoleKey.UpArrow:
                if (_y > 0)
                    _y--;
                break;

            case ConsoleKey.DownArrow:
                if (_y < _field.GetLength(1) - 1)
                    _y++;
                break;

            case ConsoleKey.Spacebar:
                if (_rules != Rules.HardLife)
                    _field[_x, _y] = _field[_x, _y] == 0 ? 1u : 0u;
                else
                {
                    if (_field[_x, _y] != 4)
                        _field[_x, _y]++;
                    else
                        _field[_x, _y] = 0;
                }
                break;

            case ConsoleKey.N:
                Evolution();
                break;

            case ConsoleKey.S:
                Save();
                break;

            case ConsoleKey.L:
                Load();
                break;

            case ConsoleKey.C:
                Array.Clear(_field, 0, _field.GetLength(0) * _field.GetLength(1));
                break;
            case ConsoleKey.R:
                switch (Console.ReadLine())
                {
                    case "Conway":
                        _rules = Rules.Conway;
                        break;
                    case "Avgust":
                    case "Seeds":
                        _rules = Rules.Avgust;
                        break;
                    case "HighLife":
                        _rules = Rules.HighLife;
                        break;
                }
                break;
            case ConsoleKey.I:
                for (int row = 0; row < _field.GetLength(1); row++)
                {
                    for (int column = 0; column < _field.GetLength(0); column++)
                    {
                        _field[column, row] = (uint)(_field[column, row] != 0 ? 0 : 1);
                    }
                }
                break;
        }
    }
    private static void Save()
    {
        Console.Clear();
        Console.Write("Введите название: ");
        var name = Console.ReadLine();
        var fileName = $"{name}.field";
        using (var file = File.Open(fileName, FileMode.Create))
        {
            var writer = new BinaryWriter(file);
            writer.Write(_field.GetLength(0));
            writer.Write(_field.GetLength(1));
            for (int row = 0; row < _field.GetLength(1); row++)
            {
                for (int column = 0; column < _field.GetLength(0); column++)
                {
                    writer.Write(_field[column, row]);
                }
            }
        }
    }

    private static void Load()
    {
        Console.Clear();
        Console.Write("Введите название: ");
        var name = Console.ReadLine();
        var fileName = $"{name}.field";
        if (!File.Exists(fileName))
        {
            return;
        }

        using (var file = File.Open(fileName, FileMode.Open))
        {
            var reader = new BinaryReader(file);
            var width = reader.ReadInt32();
            var height = reader.ReadInt32();
            var field = new uint[width, height];
            for (int row = 0; row < field.GetLength(1); row++)
            {
                for (int column = 0; column < field.GetLength(0); column++)
                {
                    field[column, row] = reader.ReadUInt32();
                }
            }
            _field = field;
        }
    }

    static int GetNeighborCount(int column, int row)
    {
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        var count = 0;
        if (row > 0 && column > 0 && _field[column - 1, row - 1] != 0)
        {
            count++;
        }

        if (row > 0 && _field[column, row - 1] != 0)
        {
            count++;
        }

        if (row > 0 && column < width - 1 && _field[column + 1, row - 1] != 0)
        {
            count++;
        }

        if (column < width - 1 && _field[column + 1, row] != 0)
        {
            count++;
        }

        if (column < width - 1 && row < height - 1 && _field[column + 1, row + 1] != 0)
        {
            count++;
        }

        if (row < height - 1 && _field[column, row + 1] != 0)
        {
            count++;
        }

        if (row < height - 1 && column > 0 && _field[column - 1, row + 1] != 0)
        {
            count++;
        }

        if (column > 0 && _field[column - 1, row] != 0)
        {
            count++;
        }

        return count;
    }
    static int GetNeighborCount(int column, int row, int cell)
    {
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        var count = 0;
        if (row > 0 && column > 0 && _field[column - 1, row - 1] == cell)
        {
            count++;
        }

        if (row > 0 && _field[column, row - 1] == cell)
        {
            count++;
        }

        if (row > 0 && column < width - 1 && _field[column + 1, row - 1] == cell)
        {
            count++;
        }

        if (column < width - 1 && _field[column + 1, row] == cell)
        {
            count++;
        }

        if (column < width - 1 && row < height - 1 && _field[column + 1, row + 1] == cell)
        {
            count++;
        }

        if (row < height - 1 && _field[column, row + 1] == cell)
        {
            count++;
        }

        if (row < height - 1 && column > 0 && _field[column - 1, row + 1] == cell)
        {
            count++;
        }

        if (column > 0 && _field[column - 1, row] == cell)
        {
            count++;
        }

        return count;
    }
    static int GetHexNeighborCount(int column, int row)
    {
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        var count = 0;
        if (row > 0 && column > 0 && _field[column - 1, row - 1] != 0)
        {
            count++;
        }

        if (row > 0 && _field[column, row - 1] != 0)
        {
            count++;
        }

        if (column < width - 1 && _field[column + 1, row] != 0)
        {
            count++;
        }

        if (column < width - 1 && row < height - 1 && _field[column + 1, row + 1] != 0)
        {
            count++;
        }

        if (row < height - 1 && _field[column, row + 1] != 0)
        {
            count++;
        }

        if (column > 0 && _field[column - 1, row] != 0)
        {
            count++;
        }

        return count;
    }

    static int GetNeighborCount2(int column, int row)
    {
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        var count = 0;
        if (row > 0 && _field[column, row - 1] != 0)
        {
            count++;
        }

        if (column < width - 1 && _field[column + 1, row] != 0)
        {
            count++;
        }

        if (row < height - 1 && _field[column, row + 1] != 0)
        {
            count++;
        }


        if (column > 0 && _field[column - 1, row] != 0)
        {
            count++;
        }

        return count;
    }

    static void Evolution()
    {
        var width = _field.GetLength(0);
        var height = _field.GetLength(1);
        var newField = new uint[width, height];
        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                switch (_rules)
                {
                    case Rules.Conway:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                                if (count == 2 || count == 3)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }

                    case Rules.Avgust:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                            }
                            else
                            {
                                if (count == 2)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }

                    case Rules.Ulam:
                        {
                            var count = GetNeighborCount2(column, row);
                            if (_field[column, row] != 0)
                            {
                                if (_field[column, row] < 3)
                                {
                                    _field[column, row] += 1;
                                }
                            }
                            else
                            {
                                if (count == 1)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            break;
                        }
                    case Rules.HighLife:
                        {
                            var count = GetNeighborCount(column, row);

                            if (_field[column, row] != 0)
                            {
                                if (count == 2 || count == 3)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            else
                            {
                                if (count is 3 or 6)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }
                    case Rules.AvgustConway:
                        {
                            int count = GetNeighborCount(column, row);
                            if (count == 2)
                            {
                                newField[column, row] = 1;
                            }
                            if (_field[column, row] != 0)
                            {
                                if (count == 2 || count == 3)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            break;
                        }
                    case Rules.LifeWithoutDead:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                                newField[column, row] = _field[column, row] + 1;
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }
                    case Rules.DayAndNight:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                                if (count is 3 or 4 or 6 or 7 or 8)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            else
                            {
                                if (count is 3 or 6 or 7 or 8)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }
                    case Rules.Avgust2:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                            }
                            else
                            {
                                if (count == 2 || count == 4)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }
                    case Rules.Wind:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                            }
                            else
                            {
                                if (count is 3 || column != 0 && _field[column - 1, row] != 0)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }
                    case Rules.StableLife:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                                if (count is 2 or 3 or 4 or 8)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            else
                            {
                                if (count == 3)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;

                        }
                    case Rules.NotStableLife:
                        {
                            var count = GetNeighborCount(column, row);
                            if (_field[column, row] != 0)
                            {
                                if (count is 2 or 4 or 5)
                                {
                                    newField[column, row] = _field[column, row] + 1;
                                }
                            }
                            else
                            {
                                if (count is 3 or 5)
                                {
                                    newField[column, row] = 1;
                                }
                            }
                            break;
                        }
                    case Rules.HardLife:
                        {
                            int NeighborCount(int cell)
                            {
                                return GetNeighborCount(column, row, cell);
                            }
                            if (_field[column, row] == 1)
                            {
                                if (NeighborCount(2) > 2)
                                {
                                    _field[column, row] = 0;
                                }
                            }
                            if (_field[column, row] == 3)
                            {
                                var leftAndRightExpand = true;
                                if (column != 0 && _field[column - 1, row] == 0)
                                {
                                    _field[column - 1, row] = 3;
                                }
                                else
                                {
                                    leftAndRightExpand = false;
                                }
                                if (column != _field.GetLength(0) && _field[column + 1, row] == 0)
                                {
                                    _field[column - 1, row] = 3;
                                }
                                else
                                {
                                    leftAndRightExpand = false;
                                }
                                var upExpand = true;
                                if (leftAndRightExpand && row != 0 && _field[column, row - 1] == 0)
                                {
                                    
                                }
                                else
                                {
                                    upExpand = false;
                                }
                            }
                            break;
                        }
                }
            }
        }

        _field = newField;
    }

    static void DrawField()
    {
        Console.CursorLeft = 0;
        Console.CursorTop = 0;
        for (int row = 0; row < _field.GetLength(1); row++)
        {
            for (int column = 0; column < _field.GetLength(0); column++)
            {
                if (_field[column, row] != 0)
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

        Console.CursorLeft = _x;
        Console.CursorTop = _y;
    }
    static void OnRandom(double probability)
    {
        var rng = new Random();
        for (int row = 0; row < _field.GetLength(1); row++)
        {
            for (int column = 0; column < _field.GetLength(0); column++)
            {
                if (rng.NextDouble() < probability)
                {
                    _field[column, row] = 1;
                }
            }
        }
    }
}