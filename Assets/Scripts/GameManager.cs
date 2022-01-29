using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string variableTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("HighScoreRed") >= 3) {
            SceneManager.LoadScene("MenuScene");
        }
        if (PlayerPrefs.GetInt("HighScoreBlue") >= 3)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
