using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    private GameDirector director;

    public int squareId;
    // Start is called before the first frame update
    void Start()
    {
        Board.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onClickAct()
    {
        // this.director.PutMark(squareId,this.gameObject.transform.position);
        Board.Update(squareId, this.gameObject.transform.position);
 
    } 
}
