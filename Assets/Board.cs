using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;
using UnityEngine.UI;

public static class Board
{
    private const int MarkEmpty = 0;
    private const int MarkCircle = 1;
    private const int MarkCross = 2;

    private static int _nextMark = MarkCircle;

    private const int StatusInplay = 0;
    private const int StatusFinish = 1;

    private static int _status = StatusInplay;

    private static GameObject _srcObj;

    private static readonly GameObject Circle = GameObject.Find("Circle");
    private static readonly GameObject Cross = GameObject.Find("Cross");
    private static readonly GameObject Canvas = GameObject.Find("Canvas");
    private static readonly GameObject ResultText = GameObject.Find("ResultText");


    private static int[] _board;
    private static GameObject[] _objects;

    public static void Init()
    {
        Debug.Log("Board Init");
        _board = new[]
        {
            MarkEmpty, MarkEmpty, MarkEmpty,
            MarkEmpty, MarkEmpty, MarkEmpty,
            MarkEmpty, MarkEmpty, MarkEmpty,
        };
        _objects = new GameObject[]
        {
            null, null, null,
            null, null, null,
            null, null, null
        };
    }

    private static readonly int[,] Lines = new int[,]
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

    private static void Finish(int winMark)
    {
        Debug.Log("Finish");
        var winnerStr = winMark == MarkCircle ? "○" : "×";

        ResultText.GetComponent<Text>().text = "Winner: " + winnerStr;
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

            if (
                mark0 == MarkCircle && mark1 == MarkCircle && mark2 == MarkCircle)
            {
                Finish(MarkCircle);
                break;
            }

            if (
                mark0 == MarkCross && mark1 == MarkCross && mark2 == MarkCross)
            {
                Finish(MarkCross);
                break;
            }

            Debug.Log("NO FINISH");
        }
    }

    public static void Update(int squareId, Vector2 objPos)
    {
        Debug.Log("Board Update");
        if (_board[squareId] != MarkEmpty)
        {
            return;
        }

        if (_status == StatusFinish)
        {
            return;
        }

        _board[squareId] = _nextMark;

        if (_nextMark == MarkCircle)
        {
            _srcObj = Circle;
            _nextMark = MarkCross;
        }
        else
        {
            _srcObj = Cross;
            _nextMark = MarkCircle;
        }

        var newObj = Object.Instantiate(_srcObj, objPos, Circle.transform.rotation);
        newObj.transform.SetParent(Canvas.transform);


        _objects[squareId] = newObj;
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
        _status = StatusInplay;
        ResultText.GetComponent<Text>().text = "";
    }
}