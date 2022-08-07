namespace AlgComparer;

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