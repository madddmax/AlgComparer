/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/

using System.Text;

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
            var prediction = MoneyLine(games[i], alg1[i], alg2[i]);
            chessRoi.Append(prediction);
            Console.WriteLine(prediction);
        }

        Console.WriteLine(chessRoi);

        var chessRoiDnb = new ChessRoiDnb();
        for (int i = 0; i < games.Length; i++)
        {
            var prediction = Dnb(games[i], alg1Dnb[i], alg2Dnb[i]);
            chessRoiDnb.Append(prediction);
            Console.WriteLine(prediction);
        }

        Console.WriteLine(chessRoiDnb);
    }

    private static ChessPrediction MoneyLine(string game, ChessPrediction alg1, ChessPrediction alg2)
    {
        float white = 0;
        if (alg1.White > alg2.White)
        {
            if (game == "WhiteWin")
                white = (1 / alg2.White) - 1;
            else
                white = -1;
        }

        float draw = 0;
        if (alg1.Draw > alg2.Draw)
        {
            if (game == "Draw")
                draw = (1 / alg2.Draw) - 1;
            else
                draw = -1;
        }

        float black = 0;
        if (alg1.Black > alg2.Black)
        {
            if (game == "BlackWin")
                black = (1 / alg2.Black) - 1;
            else
                black = -1;
        }

        return new ChessPrediction(white, draw, black);
    }

    private static ChessPredictionDnb Dnb(string game, ChessPredictionDnb alg1, ChessPredictionDnb alg2)
    {
        float white = 0;
        if (alg1.White > alg2.White)
        {
            if (game == "WhiteWin")
                white = (1 / alg2.White) - 1;
            else if (game == "Draw")
                white = 0;
            else
                white = -1;
        }

        float black = 0;
        if (alg1.Black > alg2.Black)
        {
            if (game == "BlackWin")
                black = (1 / alg2.Black) - 1;
            else if (game == "Draw")
                black = 0;
            else
                black = -1;
        }

        return new ChessPredictionDnb(white, black);
    }

    public class ChessPrediction
    {
        public float White { get; set; }
        public float Draw { get; set; }
        public float Black { get; set; }

        public bool HasWhite => White != 0;
        public bool HasDraw => Draw != 0;
        public bool HasBlack => Black != 0;

        private int Count
        {
            get
            {
                int count = 0;

                if (HasWhite)
                {
                    count++;
                }

                if (HasDraw)
                {
                    count++;
                }

                if (HasBlack)
                {
                    count++;
                }

                return count;
            }
        }

        public float Full => Count != 0 ? (White + Draw + Black) / Count : 0;

        public ChessPrediction(float white, float draw, float black)
        {
            White = white;
            Draw = draw;
            Black = black;
        }

        public override string ToString()
        {
            return $"{{ {Full:0.0},    {White:0.0}, {Draw:0.0}, {Black:0.0} }}";
        }
    }

    public class ChessRoi
    {
        public float Full { get; set; }
        public int GamesCount { get; set; }

        public float White { get; set; }
        public int WhiteCount { get; set; }

        public float Draw { get; set; }
        public int DrawCount { get; set; }

        public float Black { get; set; }
        public int BlackCount { get; set; }

        public void Append(ChessPrediction prediction)
        {
            Full += prediction.Full;
            GamesCount++;

            if (prediction.HasWhite)
            {
                White += prediction.White;
                WhiteCount++;
            }

            if (prediction.HasDraw)
            {
                Draw += prediction.Draw;
                DrawCount++;
            }

            if (prediction.HasBlack)
            {
                Black += prediction.Black;
                BlackCount++;
            }
        }

        public override string ToString()
        {
            float fullRoi = Full / GamesCount;
            float whiteRoi = White / WhiteCount;
            float drawRoi = Draw / DrawCount;
            float blackRoi = Black / BlackCount;

            var builder = new StringBuilder();
            builder.Append($"ROI_Algo1_to_Algo2_Full = {fullRoi.ToString("##.0 %")}\n");
            builder.Append($"ROI_Algo1_to_Algo2_white = {whiteRoi.ToString("##.0 %")}\n");
            builder.Append($"ROI_Algo1_to_Algo2_draw = {drawRoi.ToString("##.0 %")}\n");
            builder.Append($"ROI_Algo1_to_Algo2_black = {blackRoi.ToString("##.0 %")}\n");

            return builder.ToString();
        }
    }

    public class ChessPredictionDnb
    {
        public float White { get; set; }
        public float Black { get; set; }

        public bool HasWhite => White != 0;
        public bool HasBlack => Black != 0;

        public float Full => White + Black;

        public ChessPredictionDnb(float white, float black)
        {
            White = white;
            Black = black;
        }

        public override string ToString()
        {
            return $"{{ {Full:0.0},    {White:0.0}, {Black:0.0} }}";
        }
    }

    public class ChessRoiDnb
    {
        public float Full { get; set; }
        public int GamesCount { get; set; }

        public float White { get; set; }
        public int WhiteCount { get; set; }

        public float Black { get; set; }
        public int BlackCount { get; set; }

        public void Append(ChessPredictionDnb prediction)
        {
            Full += prediction.Full;
            GamesCount++;

            if (prediction.HasWhite)
            {
                White += prediction.White;
                WhiteCount++;
            }

            if (prediction.HasBlack)
            {
                Black += prediction.Black;
                BlackCount++;
            }
        }

        public override string ToString()
        {
            float fullRoi = Full / GamesCount;
            float whiteRoi = White / WhiteCount;
            float blackRoi = Black / BlackCount;

            var builder = new StringBuilder();
            builder.Append($"ROI_dnb_Algo1_to_Algo2_Full = {fullRoi.ToString("##.0 %")}\n");
            builder.Append($"ROI_dnb_Algo1_to_Algo2_white = {whiteRoi.ToString("##.0 %")}\n");
            builder.Append($"ROI_dnb_Algo1_to_Algo2_black = {blackRoi.ToString("##.0 %")}\n");

            return builder.ToString();
        }
    }
}