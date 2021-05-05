using UnityEngine;

public class GameDirector : MonoBehaviour
{
    private const int EMPTY = 0;
    private const int CIRCLE = 0;
    private const int CROSS = 0;

    private int[] board =
    {
        EMPTY, EMPTY, EMPTY,
        EMPTY, EMPTY, EMPTY,
        EMPTY, EMPTY, EMPTY
    };

    private GameObject circle;
    private GameObject cross;

    private GameObject text;

    // Start is called before the first frame update
    private void Start()
    {
        // this.text = GameObject.Find("Text");
        // this.circle = GameObject.Find("Circle");
        // this.cross = GameObject.Find("Cross");
        // Board.Init();
    }

    // Update is called once per frame
    private void Update()
    {
        // Debug.Log("Update");
        if (Input.GetMouseButtonDown(0))
        {
            // Vector2 clickpos = Input.mousePosition;
            // this.text.GetComponent<Text>().text = clickpos.ToString();
            // this.square0
            // Debug.Log("Click");
        }
    }

    public void PutMark(int squareId, Vector2 objPos)
    {
        // Debug.Log(objPos.ToString());
        Board.Update(squareId, objPos);
        // Debug.Log("Touch");
    }
}