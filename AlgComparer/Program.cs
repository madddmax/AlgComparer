/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/

namespace AlgComparer;

public static class Program
{
    public static void Main()
    {
        string[] games = {"WhiteWin", "Draw", "BlackWin", "WhiteWin", "Draw"};

        ChessPrediction[] alg1 =
        {
            new(0.15f, 0.10f, 0.75f),
            new(0.28f, 0.07f, 0.65f),
            new(0.50f, 0.10f, 0.40f),
            new(0.90f, 0.05f, 0.05f),
            new(0.80f, 0.05f, 0.15f)
        };

        ChessPrediction[] alg2 =
        {
            new(0.10f, 0.10f, 0.80f),
            new(0.25f, 0.05f, 0.70f),
            new(0.50f, 0.05f, 0.45f),
            new(0.75f, 0.15f, 0.10f),
            new(0.85f, 0.10f, 0.05f)
        };

        ChessPredictionDnb[] alg1Dnb =
        {
            new(0.25f, 0.75f),
            new(0.28f, 0.72f),
            new(0.50f, 0.50f),
            new(0.55f, 0.45f),
            new(0.85f, 0.15f)
        };

        ChessPredictionDnb[] alg2Dnb =
        {
            new(0.20f, 0.80f),
            new(0.25f, 0.75f),
            new(0.55f, 0.45f),
            new(0.75f, 0.25f),
            new(0.85f, 0.15f)
        };

        var chessRoi = new ChessRoi();
        for (int i = 0; i < games.Length; i++)
        {
            var prediction = Comparer.MoneyLine(games[i], alg1[i], alg2[i]);
            chessRoi.Append(prediction);
            Console.WriteLine(prediction);
        }

        Console.WriteLine(chessRoi);

        var chessRoiDnb = new ChessRoiDnb();
        for (int i = 0; i < games.Length; i++)
        {
            var prediction = Comparer.Dnb(games[i], alg1Dnb[i], alg2Dnb[i]);
            chessRoiDnb.Append(prediction);
            Console.WriteLine(prediction);
        }

        Console.WriteLine(chessRoiDnb);
    }
}