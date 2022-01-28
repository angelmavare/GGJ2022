using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DuelistManager : MonoBehaviour
{
    public float speed = 40;
    public float dirX;
    public string character;
    private Rigidbody2D rb2D;
    private bool moveRed;
    private bool moveBlue;
    //------------------------
    private Vector2 targetPosition;
    private Vector2 touchPosition;
    private int lane = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        moveRed = false;
        moveBlue = false;

        

    }

private void Awake()
{
    // set these in your Awake():
    targetPosition = transform.position;
}



public void ButtonClick(string ch) {
        if (ch == "" || ch == null) {
            ch = character;
        }

        if (Input.touchCount > 0)
        {

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;

                if ( ch == "red")
                lane += 1;
                if ( ch == "blue")
                lane -= 1;

                lane = Mathf.Clamp(lane, -2, 2); // change to how many lanes you have
            }
        }

        //float laneWidth = 10f; // change from 2 if you need to
        float laneTarget = 0; //lane * laneWidth;
        if (ch == "red") {
            laneTarget = 50;
        }
        else if(ch == "blue" ){
            laneTarget = -50;
        }

        targetPosition.x = Mathf.MoveTowards(targetPosition.x, laneTarget, speed * Time.deltaTime);
        float tilt = laneTarget - targetPosition.x;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            ButtonClick("red");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ButtonClick("blue");
        }
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(targetPosition);

    }
}
