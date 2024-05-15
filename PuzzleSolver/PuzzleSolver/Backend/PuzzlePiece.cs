namespace PuzzleSolver.Backend;

public class PuzzlePiece
{
    public string Label { get; set; }

    /// <summary>
    /// <list type="table">
    ///     <listheader>
    ///         <term>Wert</term>    
    ///         <term>Beschreibung</term>
    ///     </listheader>
    ///     <item>
    ///         <term>0</term>    
    ///         <term>Loch</term>
    ///     </item>
    ///     <item>
    ///         <term>1</term>
    ///         <term>Halbkugel nur oben</term>
    ///     </item>
    ///     <item>
    ///         <term>2</term>
    ///         <term>Halbkugel nur unten</term>
    ///     </item>
    ///     <item>
    ///         <term>3</term>
    ///         <term>Halbkugel oben und unten</term>
    ///     </item>
    ///     <item>
    ///         <term>4</term>
    ///         <term>Oben und unten leer (Zu)</term>
    ///     </item>
    /// </list>

    /// </summary>
    public uint[] Vector { get; set; }

    public PuzzlePiece(int length)
    {
        Vector = new uint[length];
    }

    public PuzzlePiece Clone()
    {
        var res = new PuzzlePiece(Vector.Length);
        res.Vector = (uint[])Vector.Clone();
        res.Label = Label;
        return res;
    }

    public void Turn2D()
    {
        var temp = new uint[Vector.Length];
        for (int i = 0; i < Vector.Length; i++)
        {
            temp[i] = Vector[Vector.Length - i - 1];
        }

        Vector = temp;
    }

    public void Turn3D()
    {
        var temp = new uint[Vector.Length];
        for (int i = 0; i < Vector.Length; i++)
        {
            if (Vector[i] == 2)
            {
                temp[i] = 1;
                continue;
            }

            if (Vector[i] == 1)
            {
                temp[i] = 2;
                continue;
            }

            temp[i] = Vector[i];
        }
        Vector = temp;
    }
}