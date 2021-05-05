using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;
using UnityEngine.UI;

public static class Board
{
    private static MarkManager.Mark _currentMark;

    private const int StatusInPlay = 0;
    private const int StatusFinish = 1;

    private static int _status = StatusInPlay;

    private static GameObject _srcObj;

    private static readonly GameObject Canvas = GameObject.Find("Canvas");
    private static readonly GameObject ResultText = GameObject.Find("ResultText");


    private static MarkManager.Mark[] _board;
    private static GameObject[] _objects;


    public static void Init()
    {
        Debug.Log("Board Init");
        _board = new MarkManager.Mark[]
        {
            MarkManager.Empty, MarkManager.Empty, MarkManager.Empty,
            MarkManager.Empty, MarkManager.Empty, MarkManager.Empty,
            MarkManager.Empty, MarkManager.Empty, MarkManager.Empty,
        };
        _objects = new GameObject[]
        {
            null, null, null,
            null, null, null,
            null, null, null
        };

        _currentMark = MarkManager.Circle;
        _status = StatusInPlay;
        ResultText.GetComponent<Text>().text = "";
        MarkManager.SetOpponent();
    }

    private static readonly int[,] Lines =
    {
        {0, 1, 2},
        {3, 4, 5},
        {6, 7, 8},
        {0, 3, 6},
        {1, 4, 7},
        {2, 5, 8},
        {0, 4, 8},
        {2, 4, 6},
    };

    private static void Finish(MarkManager.Mark winMark)
    {
        Debug.Log("Finish");

        ResultText.GetComponent<Text>().text = "Winner: " + winMark;
        _status = StatusFinish;
    }

    private static void CheckFinish()
    {
        Debug.Log("CHECK FINISH");
        for (var i = 0; i < Lines.GetLength(0); i++)
        {
            var square0 = Lines[i, 0];
            var square1 = Lines[i, 1];
            var square2 = Lines[i, 2];
            var mark0 = _board[square0];
            var mark1 = _board[square1];
            var mark2 = _board[square2];
            Debug.Log("squareID:" + square0 + square1 + square2 + ", mark:" + mark0 + mark1 + mark2);

            if (mark0 == MarkManager.Empty)
            {
                continue;
            }

            if (mark1 == mark0 && mark2 == mark0)
            {
                Finish(mark0);
                break;
            }

            Debug.Log("NO FINISH");
        }
    }

    public static void Update(int squareId, Vector2 objPos)
    {
        Debug.Log("Board Update");
        if (_board[squareId] != MarkManager.Empty)
        {
            return;
        }

        if (_status == StatusFinish)
        {
            return;
        }

        Debug.Log("board:" + _board.Length);
        Debug.Log("squareId:" + squareId);
        Debug.Log("square:" + _board[squareId]);
        _board[squareId] = _currentMark;


        var newObj = Object.Instantiate(_currentMark.Obj, objPos, _currentMark.Obj.transform.rotation);
        newObj.transform.SetParent(Canvas.transform);
        _objects[squareId] = newObj;

        _currentMark = _currentMark.Opponent;

        CheckFinish();
    }

    public static void Reset()
    {
        foreach (var obj in _objects)
        {
            if (obj == null)
            {
                continue;
            }

            Object.Destroy(obj);
        }

        Init();
    }
}