using System;

class Program
{
    static void Main(string[] args)
    {
        var fieldWidth = 40;
        var fieldHeight = 30;
        var field = new bool[fieldWidth, fieldHeight];
        field[4,5] = true;

        for (int row = 0; row < fieldHeight; row++)
        {
            for (int column = 0; column < fieldWidth; column++)
            {
                if (field[column,row])
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
        
        Console.ReadKey();
    }
}
