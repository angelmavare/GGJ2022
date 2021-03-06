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
    public GameObject blueDynamic;
    public GameObject redDynamic;
    private int soundAttackCountRed = 0;
    private int soundAttackCountBlue = 0;

    public Vector3[] redButtonPositions = {
        new Vector3(-809,5,0),
        new Vector3(-481,351,0),
        new Vector3(-699,-294,0),
        new Vector3(-249,-294,0),
    };

    public string[] redButtonTexts = {
        "D",
        "W",
        "A",
        "S"
    };

    public Vector3[] blueButtonPositions = {
        new Vector3(810,-5,0),
        new Vector3(481,351,0),
        new Vector3(699,-294,0),
        new Vector3(249,-294,0),
    };

    public string[] blueButtonTexts = {
        "←",
        "↑",
        "→",
        "↓"
    };
    public string redKey;
    public string blueKey;
    [SerializeField] float timeToChangeButton = 5f;
    float timerValue;
    [SerializeField] float timeToStart = 3f;
    float timerStart;
    private bool fightHasStarted;


    private Animator animator;
    public Vector2 direccion;

    //public GameObject aviso;

    // Start is called before the first frame update
    void Start()
    {        
        timerStart = timeToStart;
        fightHasStarted = false;
        rb2D = GetComponent<Rigidbody2D>();
        //originalPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        originalPosRed = new Vector2(GameObject.FindGameObjectWithTag("PlayerRed").transform.position.x, GameObject.FindGameObjectWithTag("PlayerRed").transform.position.y);
        originalPosBlue = new Vector2(GameObject.FindGameObjectWithTag("PlayerBlue").transform.position.x, GameObject.FindGameObjectWithTag("PlayerBlue").transform.position.y);
        moveRed = false;
        moveBlue = false;
        triggerCharacter = false;

        scoreRed = PlayerPrefs.GetInt("HighScoreRed");
        scoreBlue = PlayerPrefs.GetInt("HighScoreBlue");
        var redButton = GameObject.FindGameObjectWithTag("RedButton");
        var blueButton = GameObject.FindGameObjectWithTag("BlueButton");
        blueButtonPositions = new Vector3[] {
            new Vector3(810,-5,0),
            new Vector3(481,351,0),
            new Vector3(699,-294,0),
            new Vector3(249,-294,0),
        };
        redButtonPositions = new Vector3[] {
            new Vector3(-809,5,0),
            new Vector3(-481,351,0),
            new Vector3(-699,-294,0),
            new Vector3(-249,-294,0),
        };
        redButton.transform.localPosition = redButtonPositions[0];
        redKey = "D";
        blueButton.transform.localPosition = blueButtonPositions[0];
        blueKey = "←";

        animator = GetComponent<Animator>();
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
            AudioSource audio = gameObject.AddComponent<AudioSource >();
            audio.PlayOneShot((AudioClip)Resources.Load("yame"));
            Invoke("GoToMainScene", 1f);
            
            // aviso.SetActive(true);
            //GameObject.FindGameObjectWithTag("PlayerRed").transform.position = originalPosRed;
            //GameObject.FindGameObjectWithTag("PlayerBlue").transform.position = originalPosBlue;
        }
        
}

public void GoToMainScene(){
    SceneManager.LoadScene("MainScene");
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
            animator.SetTrigger("attackred");
            Instantiate(soundAttackRed);
            //AnimarMovimiento(direccion);
            
            //animator.Play("AttackBlue");


            if (soundAttackCountRed > 6) {
                Instantiate(soundAttackLongRed);
                soundAttackCountRed = 0;
            }
        }
        else if(ch == "blue" ){
            soundAttackCountBlue++;
            laneTarget = -50;
            animator.SetTrigger("attackblue");
            Instantiate(soundAttackBlue);
            //AnimarMovimiento(direccion);
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
        if (character == "blue") {
            animator.SetTrigger("standblue");
        }
        else if(character == "red"){
            animator.SetTrigger("standred");
        }
        
       
        if (PlayerPrefs.GetString("Difficulty") == "hard") {
            UpdateTimer();
        }
        UpdateTimerStart();
        if (fightHasStarted) {
            if (this.gameObject.name == "DuelistRed") {
                switch(redKey) 
                {
                    case "W":
                        if (Input.GetKeyDown(KeyCode.W)) {
                            ButtonClick("red");
                        }
                        break;
                    case "D":
                        if (Input.GetKeyDown(KeyCode.D)) {
                            ButtonClick("red");
                        }   
                        break;
                    case "S":
                        if (Input.GetKeyDown(KeyCode.S)) {
                            ButtonClick("red");
                        }
                        break;
                    case "A":
                        if (Input.GetKeyDown(KeyCode.A)) {
                            ButtonClick("red");
                        }
                        break;
                }
            }
            if (this.gameObject.name == "DuelistBlue") {
                switch(blueKey) 
                {
                    case "↑":
                        if (Input.GetKeyDown(KeyCode.UpArrow)) {
                            ButtonClick("blue");
                        }
                        break;
                    case "←":
                        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                            ButtonClick("blue");
                        }   
                        break;
                    case "→":
                        if (Input.GetKeyDown(KeyCode.RightArrow)) {
                            ButtonClick("blue");
                        }
                        break;
                    case "↓":
                        if (Input.GetKeyDown(KeyCode.DownArrow)) {
                            ButtonClick("blue");
                        }
                        break;
                }
            }
        }
        
        TXTScoreRed.text = ""+scoreRed;
        TXTScoreBlue.text = ""+scoreBlue;

    }
    private void FixedUpdate()
    {
        rb2D.MovePosition(targetPosition);
    }

    private void HideDynamics()
    {
        blueDynamic = GameObject.FindGameObjectWithTag("BlueDynamic");
        blueDynamic.GetComponent<Renderer>().enabled = false;
        redDynamic = GameObject.FindGameObjectWithTag("RedDynamic");
        redDynamic.GetComponent<Renderer>().enabled = false;
    }

    private void ShowDynamics()
    {
        blueDynamic.SetActive(true);
        redDynamic.SetActive(true);
    }

    public GameObject ShowObjectByTag(string tagName)
    {
        return GameObject.FindGameObjectWithTag(tagName);
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(timerValue <= 0)
        {
            UpdateButtonPositions();
            timerValue = timeToChangeButton;
        }
    }

    void UpdateTimerStart()
    {
        timerStart -= Time.deltaTime;
        if(((int)timerStart) < 0)
        {
            fightHasStarted = true;
        }
    }

    void UpdateButtonPositions()
    {
        if (this.gameObject.name == "DuelistBlue") {
            var blueButton = GameObject.FindGameObjectWithTag("BlueButton");
            var blueButtonText = GameObject.FindGameObjectWithTag("BlueButtonText");
            int blueIndex = Random.Range(0, 3);
            blueButton.transform.localPosition = blueButtonPositions[blueIndex];
            blueKey = blueButtonTexts[blueIndex];
            blueButtonText.GetComponent<UnityEngine.UI.Text>().text = blueKey;
        }
        if (this.gameObject.name == "DuelistRed") {
            var redButton = GameObject.FindGameObjectWithTag("RedButton");
            var redButtonText = GameObject.FindGameObjectWithTag("RedButtonText");
            int redIndex = Random.Range(0, 3);
            redButton.transform.localPosition = redButtonPositions[redIndex];
            redKey = redButtonTexts[redIndex];
            redButtonText.GetComponent<UnityEngine.UI.Text>().text = redKey;
        }
        
        
    }

    public void AnimarMovimiento(Vector2 direccion) {
        animator.SetFloat("x", direccion.x);

    }

}
