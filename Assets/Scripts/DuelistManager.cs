using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DuelistManager : MonoBehaviour
{
    public float speed = 50;
    public float dirX;
    public string character;
    private Rigidbody2D rb2D;
    private bool moveRed;
    private bool moveBlue;
    private bool triggerCharacter;
    //------------------------
    private Vector2 targetPosition;
    private Vector2 touchPosition;
    private Vector2 originalPosRed;
    private Vector2 originalPosBlue;
    private int lane = 0;
    private string areaTag;
    //SCORE------------------------
    public int scoreRed;
    public Text TXTScoreRed;
    public int scoreBlue;
    public Text TXTScoreBlue;
    //SOUNDS------------------------
    public GameObject soundAttackRed;
    public GameObject soundAttackBlue;
    public GameObject soundAttackLongRed;
    public GameObject soundAttackLongBlue;
    private int soundAttackCountRed = 0;
    private int soundAttackCountBlue = 0;
    //public GameObject aviso;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //originalPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        originalPosRed = new Vector2(GameObject.FindGameObjectWithTag("PlayerRed").transform.position.x, GameObject.FindGameObjectWithTag("PlayerRed").transform.position.y);
        originalPosBlue = new Vector2(GameObject.FindGameObjectWithTag("PlayerBlue").transform.position.x, GameObject.FindGameObjectWithTag("PlayerBlue").transform.position.y);
        moveRed = false;
        moveBlue = false;
        triggerCharacter = false;

        scoreRed = PlayerPrefs.GetInt("HighScoreRed");
        scoreBlue = PlayerPrefs.GetInt("HighScoreBlue");

    }

private void Awake()
{
    // set these in your Awake():
    targetPosition = transform.position;
}

 private void OnTriggerEnter2D(Collider2D collision)
{
        if (character == "red")
        {
            areaTag = "AreaRed";
        }
        if( character == "blue")
        {
            areaTag = "AreaBlue";
        }
        if (collision.gameObject.tag == areaTag)
        {
            triggerCharacter = true;
            if (areaTag == "AreaRed") {
                scoreBlue++;
                PlayerPrefs.SetInt("HighScoreBlue", scoreBlue);
            }
            if (areaTag == "AreaBlue")
            {
                scoreRed++;
                PlayerPrefs.SetInt("HighScoreRed", scoreRed);
                
            }

            SceneManager.LoadScene("MainScene");
            // aviso.SetActive(true);
            //GameObject.FindGameObjectWithTag("PlayerRed").transform.position = originalPosRed;
            //GameObject.FindGameObjectWithTag("PlayerBlue").transform.position = originalPosBlue;
        }
        
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

                if (ch == "red") {
                    lane += 1;
                }

                if (ch == "blue") {
                    lane -= 1;
                }
                
                

                lane = Mathf.Clamp(lane, -2, 2); // change to how many lanes you have
                Debug.Log("Lane: " + lane);
            }
        }

        //float laneWidth = 10f; // change from 2 if you need to
        float laneTarget = 0; //lane * laneWidth;
        
        if (ch == "red") {
            soundAttackCountRed++;
            laneTarget = 50;
            Instantiate(soundAttackRed);
            if (soundAttackCountRed > 6) {
                Instantiate(soundAttackLongRed);
                soundAttackCountRed = 0;
            }
        }
        else if(ch == "blue" ){
            soundAttackCountBlue++;
            laneTarget = -50;
            Instantiate(soundAttackBlue);
            if (soundAttackCountBlue > 6)
            {
                Instantiate(soundAttackLongBlue);
                soundAttackCountBlue = 0;
            }
        }

        targetPosition.x = Mathf.MoveTowards(targetPosition.x, laneTarget, speed * Time.deltaTime);
        float tilt = laneTarget - targetPosition.x;


    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.W)) {

            ButtonClick("red");
            //Instantiate(soundAttackRed);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            ButtonClick("blue");
            //Instantiate(soundAttackBlue);
        }

        TXTScoreRed.text = ""+scoreRed;
        TXTScoreBlue.text = ""+scoreBlue;

    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(targetPosition);
    }
}
