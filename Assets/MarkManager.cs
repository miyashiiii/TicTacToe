using UnityEngine;

internal class MarkManager
{
    public static readonly MarkEmpty Empty = new MarkEmpty();
    public static readonly MarkCircle Circle = new MarkCircle();
    public static readonly MarkCross Cross = new MarkCross();

    public static void SetOpponent()
    {
        Circle.Opponent = Cross;
        Cross.Opponent = Circle;
    }

    public abstract class Mark
    {
        public virtual GameObject Obj => null;

        public Mark Opponent;
    }


    internal class MarkEmpty : Mark
    {
        //フィールド一覧
        private const string Str = "";

        public override GameObject Obj => null;

        public override string ToString()
        {
            return Str;
        }
    }

    internal class MarkCircle : Mark
    {
        //フィールド一覧
        private static readonly GameObject CircleObj = GameObject.Find("Circle");
        private const string Str = "○";

        public override GameObject Obj => CircleObj;

        public override string ToString()
        {
            return Str;
        }
    }

    internal class MarkCross : Mark
    {
        //フィールド一覧
        private static readonly GameObject CrossObj = GameObject.Find("Cross");
        private const string Str = "×";

        public override GameObject Obj => CrossObj;

        public override string ToString()
        {
            return Str;
        }
    }
}