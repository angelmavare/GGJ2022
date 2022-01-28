using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DuelistManager : MonoBehaviour
{
    public float speed = 5;
    public float dirX;
    private Rigidbody2D rb2D;
    private bool moveRed;
    private bool moveBlue;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

        moveRed = false;
        moveBlue = false;

    }

    // Update is called once per frame
    void Update()
    {
        //dirX = 
    }

    

    public void ButtonDownRed() {
        moveRed = true;
        dirX = speed;
    }
    public void ButtonDownBlue()
    {
        moveRed = true;
        dirX = -speed;
    }

    private void MovementRed()
    {

        //Output this to console when Button1 or Button3 is clicked
        Debug.Log("You have clicked the button!");

    }
    private void FixedUpdate()
    {
        //comments
        rb2D.velocity = new Vector2(dirX, rb2D.velocity.y);
    }
}
