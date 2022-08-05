/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/

using System.Text;

class AlgoCalcExample
{
    static void Main()
    {
        Console.WriteLine("Hello AlgoCalcExample");
        Console.WriteLine("MoneyLine predictions calculate start");

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

        var chessROI = new ChessRoi();
        var moneyLinePredictions = new List<ChessPrediction>();

        for (int i = 0; i < games.Length; i++)
        {
            var moneyLine = MoneyLine(games[i], alg1[i], alg2[i]);
            Console.WriteLine(moneyLine);

            chessROI.Append(moneyLine);
            moneyLinePredictions.Add(moneyLine);
        }

        Console.WriteLine(chessROI);

        Console.WriteLine("MoneyLine predictions calculate complete");
        Console.WriteLine("DNB predictions calculate start");

        // Алгоритм-калькулятор для прогнозов “dnb”: Сравниваем Алгоритм1 с Алгоритмом2:
        // Сравнение производим на примере 5-ти партий
        //string[] GamesResult = new string[] {"WhiteWin", "Draw", "BlackWin", "WhiteWin", "Draw"};
        // Результаты партий тут в формате string. В БД они могут лежать в другом формате.
        // Результатом работы алгоритма 1 будут массивы прогнозов
        float[] VerDnbWhite1 = new float[]
            {0.25f, 0.28f, 0.50f, 0.55f, 0.85f}; // Прогнозы победы белых Алгоритма 1 для 5-ти партий
        float[] VerDnbBlack1 = new float[]
            {0.75f, 0.72f, 0.50f, 0.45f, 0.15f}; // Прогнозы победы черных Алгоритма 1 для 5-ти партий
        // Результатом работы алгоритма 2 будут массивы прогнозов
        float[] VerDnbWhite2 = new float[]
            {0.20f, 0.25f, 0.55f, 0.75f, 0.85f}; // Прогнозы победы белых Алгоритма 2 для 5-ти партий
        float[] VerDnbBlack2 = new float[]
            {0.80f, 0.75f, 0.45f, 0.25f, 0.15f}; // Прогнозы победы черных Алгоритма 2 для 5-ти партий
        // Результатом сравнения прогнозов будут помещаться в массивы
        float[]
            PredictionDnbResultFull =
                new float[5]; // Массив реузльтатов прогнозирования по каждной партии Алгоритма 1 для 5-ти партий
        float[]
            PredictionDnbResultWhite =
                new float[5]; // Массив реузльтатов прогнозирования победы белых Алгоритма 1 для 5-ти партий
        float[]
            PredictionDnbResultBlack =
                new float[5]; // Массив реузльтатов прогнозирования победы черных Алгоритма 1 для 5-ти партий

        float ROI_dnb_Algo1_to_Algo2_Full;
        float ROI_dnb_Algo1_to_Algo2_white;
        float ROI_dnb_Algo1_to_Algo2_black;
        // Битва Алгоритма 1 с Алгоритмом 2
        for (int game = 0; game < games.Length; game++)
        {
            if (VerDnbWhite1[game] > VerDnbWhite2[game])
            {
                if (games[game] == "WhiteWin")
                    PredictionDnbResultWhite[game] =
                        (1 / VerDnbWhite2[game]) -
                        1; // Количественный результат битвы. Прогноз Алгоритма 1 выиграл в битве с прогнозом Алогоритма 2
                else if (games[game] == "Draw")
                    PredictionDnbResultWhite[game] = 0; // Ничья не учитывается.
                else if (games[game] == "BlackWin")
                    PredictionDnbResultWhite[game] =
                        -1; // Количественный результат битвы. Прогноз Алгоритма 1 проиграл в битве с прогнозом Алогоритма 2
            }
            else
                PredictionDnbResultWhite[game] =
                    0; // Прогноз Алгоритма 1 не участвовал в битве с прогнозом Алогоритма 2

            if (VerDnbBlack1[game] > VerDnbBlack2[game])
            {
                if (games[game] == "BlackWin")
                    PredictionDnbResultBlack[game] =
                        (1 / VerDnbBlack2[game]) -
                        1; // Количественный результат битвы. Прогноз Алгоритма 1 выиграл в битве с прогнозом Алогоритма 2
                else if (games[game] == "Draw")
                    PredictionDnbResultBlack[game] = 0; // Ничья не учитывается.
                else if (games[game] == "WhiteWin")
                    PredictionDnbResultBlack[game] =
                        -1; // Количественный результат битвы. Прогноз Алгоритма 1 проиграл в битве с прогнозом Алогоритма 2
            }
            else
                PredictionDnbResultBlack[game] =
                    0; // Прогноз Алгоритма 1 не участвовал в битве с прогнозом Алогоритма 2

            // Нормализация результата  не требуется. Просто склаываем. Два прогноза - если одна вероятность больше, то другая обязательно меньше
            PredictionDnbResultFull[game] = PredictionDnbResultWhite[game] + PredictionDnbResultBlack[game];
        }

        // Битва Алгоритма 1 с Алгоритмом 2 окончена вычисляем результаты
        ROI_dnb_Algo1_to_Algo2_Full = PredictionDnbResultFull.Sum() / games.Length;

        // Также считаем доп. показатели
        int PredictionDnbCountWhite = 0, PredictionDnbCountBlack = 0; // Дополнительные счетчики по цвету
        for (int game = 0; game < games.Length; game++)
        {
            if (PredictionDnbResultWhite[game] != 0) PredictionDnbCountWhite++;
            if (PredictionDnbResultBlack[game] != 0) PredictionDnbCountBlack++;
        }

        ROI_dnb_Algo1_to_Algo2_white = PredictionDnbResultWhite.Sum() / PredictionDnbCountWhite;
        ROI_dnb_Algo1_to_Algo2_black = PredictionDnbResultBlack.Sum() / PredictionDnbCountBlack;

        Console.WriteLine("ROI_dnb_Algo1_to_Algo2_Full = " + ROI_dnb_Algo1_to_Algo2_Full.ToString("##.0 %"));
        Console.WriteLine("ROI_dnb_Algo1_to_Algo2_white = " + ROI_dnb_Algo1_to_Algo2_white.ToString("##.0 %"));
        Console.WriteLine("ROI_dnb_Algo1_to_Algo2_black = " + ROI_dnb_Algo1_to_Algo2_black.ToString("##.0 %"));

        for (int game = 0; game < games.Length; game++)
        {
            Console.WriteLine(game + "{ " + PredictionDnbResultFull[game].ToString("0.0") + ",    " +
                              PredictionDnbResultWhite[game].ToString("0.0") + ", " +
                              PredictionDnbResultBlack[game].ToString("0.0") + " }");
        }

        Console.WriteLine("DNB predictions calculate complete");
        Console.WriteLine("Finish AlgoCalcExample");
    }

    private static ChessPrediction MoneyLine(string games, ChessPrediction alg1, ChessPrediction alg2)
    {
        float white = 0;
        if (alg1.White > alg2.White)
        {
            if (games == "WhiteWin")
                white = (1 / alg2.White) - 1;
            else
                white = -1;
        }

        float draw = 0;
        if (alg1.Draw > alg2.Draw)
        {
            if (games == "Draw")
                draw = (1 / alg2.Draw) - 1;
            else
                draw = -1;
        }

        float black = 0;
        if (alg1.Black > alg2.Black)
        {
            if (games == "BlackWin")
                black = (1 / alg2.Black) - 1;
            else
                black = -1;
        }

        return new ChessPrediction(white, draw, black);
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

        public float Full => (White + Draw + Black) / Count;

        public float ResultDnb => White + Black;

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
}