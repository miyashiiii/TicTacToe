using UnityEngine;

internal class MarkManager
{
    public static MarkEmpty Empty = new MarkEmpty();
    public static MarkCircle Circle = new MarkCircle();
    public static MarkCross Cross = new MarkCross();

    public static void SetOpponent()
    {
        Circle.opponent = Cross;
        Cross.opponent = Circle;
    }

    public abstract class Mark
    {
        public virtual GameObject obj { get; set; }

        public Mark opponent;
    }


    internal class MarkEmpty : Mark
    {
        //フィールド一覧
        private static string _str = "";

        public override GameObject obj
        {
            get { return null; }
            set { }
        }

        public override string ToString()
        {
            return _str;
        }
    }

    internal class MarkCircle : Mark
    {
        //フィールド一覧
        private static readonly GameObject CircleObj = GameObject.Find("Circle");
        private static string _str = "○";

        public override GameObject obj
        {
            get { return CircleObj; }
            set { }
        }

        public override string ToString()
        {
            return _str;
        }
    }

    internal class MarkCross : Mark
    {
        //フィールド一覧
        public static readonly GameObject CrossObj = GameObject.Find("Cross");
        private static string _str = "×";

        public override GameObject obj
        {
            get { return CrossObj; }
            set { }
        }

        public override string ToString()
        {
            return _str;
        }
    }
}