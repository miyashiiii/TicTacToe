using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public int squareId;

    // Start is called before the first frame update
    private void Start()
    {
        Board.Init();
    }

    public void onClickAct()
    {
        Debug.Log("Board Update");

        Board.Update(squareId, this.gameObject.transform.position);
    }
}