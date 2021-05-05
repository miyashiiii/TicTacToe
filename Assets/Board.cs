using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Timeline;
using UnityEngine.UI;

public static class Board
{
    const int MARK_EMPTY = 0; 
    const int MARK_CIRCLE = 1; 
    const int MARK_CROSS = 2;
    
    static int nextMark=MARK_CIRCLE;
    
    const int STATUS_INPLAY=0;
    const int STATUS_FINISH=1;
    
    static int status=STATUS_INPLAY;
    
    private static GameObject srcObj;
    
    static GameObject circle = GameObject.Find("Circle");
    static GameObject cross = GameObject.Find("Cross");
    static GameObject canvas = GameObject.Find("Canvas");
    static GameObject resultText = GameObject.Find("ResultText");
 
    
    static int[] board;
    static GameObject[] objects;

    public static void Init()
    {
        board = new int[]
        {
            MARK_EMPTY, MARK_EMPTY, MARK_EMPTY,
            MARK_EMPTY, MARK_EMPTY, MARK_EMPTY,
            MARK_EMPTY, MARK_EMPTY, MARK_EMPTY,
        };
        objects = new GameObject[]
        {
        null,null,null,
        null,null,null,
        null,null,null
        };
        
    }
    static int[,] lines   = new int[,]
    {
        { 0,1, 2 }, 
        { 3, 4 ,5},
        {  6,7,8 }, 
        {  0,3,6 }, 
        {  1,4,7 }, 
        {  2,5,8 }, 
        {  0,4,8 }, 
        {  2,4,6 }, 
    };

    static void Finish(int winmark)
    {
        Debug.Log("Finish");
        string winnerStr ;
        if (winmark == MARK_CIRCLE)
        {
        winnerStr = "○";
        }
        else 
        {
        winnerStr = "×";
        }
        resultText.GetComponent<Text>().text = "Winner: " + winnerStr;
        status = STATUS_FINISH;
    }
    static void CheckFinish()
    {
        Debug.Log("CHECK FINISH");
        for (var i=0;i < lines.GetLength(0); i++)
        {
            int square0 = lines[i, 0]; 
            int square1 = lines[i, 1]; 
            int square2 = lines[i, 2]; 
            int mark0 = board[square0]; 
            int mark1 = board[square1]; 
            int mark2 = board[square2]; 
            Debug.Log("squareID:"+square0+square1+square2+", mark:"+mark0+mark1+mark2);
            
            if ( 
                mark0== MARK_CIRCLE && mark1== MARK_CIRCLE && mark2== MARK_CIRCLE )
            {
            Finish(MARK_CIRCLE);
            break;
            }
            else if (
               mark0 == MARK_CROSS && mark1 == MARK_CROSS && mark2 == MARK_CROSS)
            {
            Finish(MARK_CROSS);
            break;
            }
        Debug.Log("NO FINISH");
            
        }
    }
    public static void Update(int squareId,Vector2 objPos )
    {
        if (board[squareId] != MARK_EMPTY) { return;}

        if (status == STATUS_FINISH) { return;}
        board[squareId] = nextMark;
        
        if (nextMark == MARK_CIRCLE)
        {
            srcObj = circle;
            nextMark = MARK_CROSS;
        }
        else
        {
            srcObj = cross;
            nextMark = MARK_CIRCLE;
        }
        // Debug.Log("Click");
        // Debug.Log("objPos:"+objPos.ToString());
        // Debug.Log("squareId:"+squareId.ToString());
 
        GameObject newObj=GameObject.Instantiate(srcObj, objPos, circle.transform.rotation);
        newObj.transform.SetParent(canvas.transform);
        // newObj.transform.SetAsFirstSibling();

 

        objects[squareId] = newObj;
        CheckFinish();
    }

    public static void Reset()
    {
        foreach (var obj in objects)
        {
            if (obj == null)
            {
                continue;
            }
            Object.Destroy(obj);
        }
        Init();
        status = STATUS_INPLAY;
        resultText.GetComponent<Text>().text = "";
 
    }
}