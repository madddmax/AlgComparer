namespace AlgComparer;

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