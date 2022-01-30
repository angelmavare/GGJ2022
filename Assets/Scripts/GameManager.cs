using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string variableTest;
    [SerializeField] float timeToStart = 3f;
    float timerStart;
    private bool fightHasStarted;
    private bool played3;
    private bool played2;
    private bool played1;
    private bool playedGo;
    // Start is called before the first frame update
    void Start()
    {
        played3 = false;
        played2 = false;
        played1 = false;
        playedGo = false;
        timerStart = timeToStart;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerStart();
        if (PlayerPrefs.GetInt("HighScoreRed") >= 3) {
            SceneManager.LoadScene("VictoryScene");
        }
        if (PlayerPrefs.GetInt("HighScoreBlue") >= 3)
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void UpdateTimerStart()
    {
        timerStart -= Time.deltaTime;
        var timerText = GameObject.FindGameObjectWithTag("TimerStart");
        if(timerStart < -1)
        {
            timerText.GetComponent<UnityEngine.UI.Text>().text = "";
        }
        else if(timerStart < 0 && timerStart > -1)
        {
            if (!playedGo) {
                AudioSource audio = gameObject.AddComponent<AudioSource >();
                audio.PlayOneShot((AudioClip)Resources.Load("go"));
                playedGo = true;
            }
            timerText.GetComponent<UnityEngine.UI.Text>().text = "GO!";
        }
        else if (timerStart > 0 && timerStart < 1) {
            if (!played1) {
                AudioSource audio = gameObject.AddComponent<AudioSource >();
                audio.PlayOneShot((AudioClip)Resources.Load("one"));
                played1 = true;
            }
            timerText.GetComponent<UnityEngine.UI.Text>().text = "one";
        }
        else {
            if(timerStart < 3 && timerStart > 2)
            {
                if (!played3) {
                    AudioSource audio = gameObject.AddComponent<AudioSource >();
                    audio.PlayOneShot((AudioClip)Resources.Load("three"));
                    played3 = true;
                    timerText.GetComponent<UnityEngine.UI.Text>().text = "three";
                }
            } else if(timerStart < 2 && timerStart > 1)
            {
                if (!played2) {
                    AudioSource audio = gameObject.AddComponent<AudioSource >();
                    audio.PlayOneShot((AudioClip)Resources.Load("two"));
                    played2 = true;
                    timerText.GetComponent<UnityEngine.UI.Text>().text = "two";
                }
            }
        }
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
