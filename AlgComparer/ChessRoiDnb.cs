using System.Text;

namespace AlgComparer;

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