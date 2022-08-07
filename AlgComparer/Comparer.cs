namespace AlgComparer;

public static class Comparer
{
    public static ChessPrediction MoneyLine(string game, ChessPrediction alg1, ChessPrediction alg2)
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

    public static ChessPredictionDnb Dnb(string game, ChessPredictionDnb alg1, ChessPredictionDnb alg2)
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
}