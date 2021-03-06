using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("HighScoreRed", 0);
        PlayerPrefs.SetInt("HighScoreBlue", 0);
        if (!PlayerPrefs.HasKey("Difficulty")) {
            PlayerPrefs.SetString("Difficulty", "hard");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
