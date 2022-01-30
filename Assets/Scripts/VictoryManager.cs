using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    string winner;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("HighScoreRed") >= 3) {
            (GameObject.FindGameObjectWithTag("BlueVictory")).GetComponent<Renderer>().enabled = false;
        }
        else
        {
            (GameObject.FindGameObjectWithTag("RedVictory")).GetComponent<Renderer>().enabled = false;
        }
        PlayerPrefs.SetInt("HighScoreRed", 0);
        PlayerPrefs.SetInt("HighScoreBlue", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
