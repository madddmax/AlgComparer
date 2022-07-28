/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
using System;
using System.Linq;

class AlgoCalcExample
{
    static void Main()
    {
        bool
            PredictionWon; // Победа прогноза в битве  = true для сыгравшего исхода партии, false для остальных исходов.
        Console.WriteLine("Hello AlgoCalcExample");
        Console.WriteLine("Moneyline predictions calculate start");
        // Алгоритм-калькулятор для прогнозов “moneyline”: Сравниваем Алгоритм1 с Алгоритмом2:
        // Сравнение производим на примере 5-ти партий
        string[] GamesResult = new string[] {"WhiteWin", "Draw", "BlackWin", "WhiteWin", "Draw"};
        // Результаты партий тут в формате string. В БД они могут лежать в другом формате.
        // Результатом работы алгоритма 1 будут массивы прогнозов
        float[] VerWhite1 = new float[]
            {0.15f, 0.28f, 0.50f, 0.90f, 0.80f}; // Прогнозы победы белых Алгоритма 1 для 5-ти партий
        float[] VerDraw1 = new float[]
            {0.10f, 0.07f, 0.10f, 0.05f, 0.05f}; // Прогнозы на ничью Алгоритма 1 для 5-ти партий
        float[] VerBlack1 = new float[]
            {0.75f, 0.65f, 0.40f, 0.05f, 0.15f}; // Прогнозы победы черных Алгоритма 1 для 5-ти партий
        // Результатом работы алгоритма 2 будут массивы прогнозов
        float[] VerWhite2 = new float[]
            {0.10f, 0.25f, 0.50f, 0.75f, 0.85f}; // Прогнозы победы белых Алгоритма 2 для 5-ти партий
        float[] VerDraw2 = new float[]
            {0.10f, 0.05f, 0.05f, 0.15f, 0.10f}; // Прогнозы на ничью Алгоритма 2 для 5-ти партий
        float[] VerBlack2 = new float[]
            {0.80f, 0.70f, 0.45f, 0.10f, 0.05f}; // Прогнозы победы черных Алгоритма 2 для 5-ти партий
        // Результатом сравнения прогнозов будут помещаться в массивы
        float[]
            PredictionResultFull =
                new float[5]; // Массив реузльтатов прогнозирования по каждной партии Алгоритма 1 для 5-ти партий
        float[]
            PredictionResultWhite =
                new float[5]; // Массив реузльтатов прогнозирования победы белых Алгоритма 1 для 5-ти партий
        float[]
            PredictionResultDraw =
                new float[5]; // Массив реузльтатов прогнозирования ничейного результата Алгоритма 1 для 5-ти партий
        float[]
            PredictionResultBlack =
                new float[5]; // Массив реузльтатов прогнозирования победы черных Алгоритма 1 для 5-ти партий

        float ROI_Algo1_to_Algo2_Full;
        float ROI_Algo1_to_Algo2_white;
        float ROI_Algo1_to_Algo2_draw;
        float ROI_Algo1_to_Algo2_black;
        int PredictionCount = 0; // Счетчик прогнозов, где мнения алгоритмов разошлись и активирован режим битвы
        // Битва Алгоритма 1 с Алгоритмом 2
        for (int game = 0; game < GamesResult.Length; game++)
        {
            PredictionCount = 0;
            if (VerWhite1[game] > VerWhite2[game])
            {
                if (GamesResult[game] == "WhiteWin")
                    PredictionWon = true; // Прогноз Алгоритма 1 выиграл в битве с прогнозом Алогоритма 2
                else
                    PredictionWon = false; // Прогноз Алгоритма 1 проиграл в битве с прогнозом Алогоритма 2
                if (PredictionWon)
                    PredictionResultWhite[game] = (1 / VerWhite2[game]) - 1; // Количественный результат битвы
                else
                    PredictionResultWhite[game] = -1; // Количественный результат битвы
                PredictionCount++;
            }
            else
                PredictionResultWhite[game] = 0; // Прогноз Алгоритма 1 не участвовал в битве с прогнозом Алогоритма 2

            if (VerDraw1[game] > VerDraw2[game])
            {
                if (GamesResult[game] == "Draw")
                    PredictionWon = true;
                else
                    PredictionWon = false;
                if (PredictionWon)
                    PredictionResultDraw[game] = (1 / VerDraw2[game]) - 1;
                else
                    PredictionResultDraw[game] = -1;
                PredictionCount++;
            }
            else
                PredictionResultDraw[game] = 0;

            if (VerBlack1[game] > VerBlack2[game])
            {
                if (GamesResult[game] == "BlackWin")
                    PredictionWon = true;
                else
                    PredictionWon = false;
                if (PredictionWon)
                    PredictionResultBlack[game] = (1 / VerBlack2[game]) - 1;
                else
                    PredictionResultBlack[game] = -1;
                PredictionCount++;
            }
            else
                PredictionResultBlack[game] = 0;

            // Нормализация результата
            PredictionResultFull[game] =
                (PredictionResultWhite[game] + PredictionResultDraw[game] + PredictionResultBlack[game]) /
                PredictionCount;
        }

        // Битва Алгоритма 1 с Алгоритмом 2 окончена вычисляем результаты
        ROI_Algo1_to_Algo2_Full = PredictionResultFull.Sum() / GamesResult.Length;

        // Также считаем доп. показатели
        int PredictionCountWhite = 0,
            PredictionCountDraw = 0,
            PredictionCountBlack = 0; // Дополнительные счетчики по цвету
        for (int game = 0; game < GamesResult.Length; game++)
        {
            if (PredictionResultWhite[game] != 0) PredictionCountWhite++;
            if (PredictionResultDraw[game] != 0) PredictionCountDraw++;
            if (PredictionResultBlack[game] != 0) PredictionCountBlack++;
        }

        ROI_Algo1_to_Algo2_white = PredictionResultWhite.Sum() / PredictionCountWhite;
        ROI_Algo1_to_Algo2_draw = PredictionResultDraw.Sum() / PredictionCountDraw;
        ROI_Algo1_to_Algo2_black = PredictionResultBlack.Sum() / PredictionCountBlack;

        Console.WriteLine("ROI_Algo1_to_Algo2_Full = " + ROI_Algo1_to_Algo2_Full.ToString("##.0 %"));
        Console.WriteLine("ROI_Algo1_to_Algo2_white = " + ROI_Algo1_to_Algo2_white.ToString("##.0 %"));
        Console.WriteLine("ROI_Algo1_to_Algo2_draw = " + ROI_Algo1_to_Algo2_draw.ToString("##.0 %"));
        Console.WriteLine("ROI_Algo1_to_Algo2_black = " + ROI_Algo1_to_Algo2_black.ToString("##.0 %"));
        for (int game = 0; game < GamesResult.Length; game++)
        {
            Console.WriteLine(game + "{ " + PredictionResultFull[game].ToString("0.0") + ",    " +
                              PredictionResultWhite[game].ToString("0.0") +
                              ", " + PredictionResultDraw[game].ToString("0.0") + ", " +
                              PredictionResultBlack[game].ToString("0.0") + " }");
        }

        Console.WriteLine("moneyline predictions calculate complete");
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
        for (int game = 0; game < GamesResult.Length; game++)
        {
            if (VerDnbWhite1[game] > VerDnbWhite2[game])
            {
                if (GamesResult[game] == "WhiteWin")
                    PredictionDnbResultWhite[game] =
                        (1 / VerDnbWhite2[game]) -
                        1; // Количественный результат битвы. Прогноз Алгоритма 1 выиграл в битве с прогнозом Алогоритма 2
                else if (GamesResult[game] == "Draw")
                    PredictionDnbResultWhite[game] = 0; // Ничья не учитывается.
                else if (GamesResult[game] == "BlackWin")
                    PredictionDnbResultWhite[game] =
                        -1; // Количественный результат битвы. Прогноз Алгоритма 1 проиграл в битве с прогнозом Алогоритма 2
            }
            else
                PredictionDnbResultWhite[game] =
                    0; // Прогноз Алгоритма 1 не участвовал в битве с прогнозом Алогоритма 2

            if (VerDnbBlack1[game] > VerDnbBlack2[game])
            {
                if (GamesResult[game] == "BlackWin")
                    PredictionDnbResultBlack[game] =
                        (1 / VerDnbBlack2[game]) -
                        1; // Количественный результат битвы. Прогноз Алгоритма 1 выиграл в битве с прогнозом Алогоритма 2
                else if (GamesResult[game] == "Draw")
                    PredictionDnbResultBlack[game] = 0; // Ничья не учитывается.
                else if (GamesResult[game] == "WhiteWin")
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
        ROI_dnb_Algo1_to_Algo2_Full = PredictionDnbResultFull.Sum() / GamesResult.Length;

        // Также считаем доп. показатели
        int PredictionDnbCountWhite = 0, PredictionDnbCountBlack = 0; // Дополнительные счетчики по цвету
        for (int game = 0; game < GamesResult.Length; game++)
        {
            if (PredictionDnbResultWhite[game] != 0) PredictionDnbCountWhite++;
            if (PredictionDnbResultBlack[game] != 0) PredictionDnbCountBlack++;
        }

        ROI_dnb_Algo1_to_Algo2_white = PredictionDnbResultWhite.Sum() / PredictionDnbCountWhite;
        ROI_dnb_Algo1_to_Algo2_black = PredictionDnbResultBlack.Sum() / PredictionDnbCountBlack;

        Console.WriteLine("ROI_dnb_Algo1_to_Algo2_Full = " + ROI_dnb_Algo1_to_Algo2_Full.ToString("##.0 %"));
        Console.WriteLine("ROI_dnb_Algo1_to_Algo2_white = " + ROI_dnb_Algo1_to_Algo2_white.ToString("##.0 %"));
        Console.WriteLine("ROI_dnb_Algo1_to_Algo2_black = " + ROI_dnb_Algo1_to_Algo2_black.ToString("##.0 %"));

        for (int game = 0; game < GamesResult.Length; game++)
        {
            Console.WriteLine(game + "{ " + PredictionDnbResultFull[game].ToString("0.0") + ",    " +
                              PredictionDnbResultWhite[game].ToString("0.0") + ", " +
                              PredictionDnbResultBlack[game].ToString("0.0") + " }");
        }

        Console.WriteLine("DNB predictions calculate complete");
        Console.WriteLine("Finish AlgoCalcExample");
    }
}