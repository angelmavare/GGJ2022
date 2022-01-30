using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour
{
    Sprite BUTTON_ON, BUTTON_OFF;
    // Start is called before the first frame update
    void Start()
    {
        BUTTON_ON =  Resources.Load <Sprite>("Asset_10_550x");
        BUTTON_OFF =  Resources.Load <Sprite>("Asset_4_550x");
        if (!PlayerPrefs.HasKey("Difficulty")) {
            PlayerPrefs.SetString("Difficulty", "hard");
        }
        if (PlayerPrefs.GetString("Difficulty") == "hard") {
            SetDifficultyToHard();
        } else {
            SetDifficultyToEasy();
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

    public void SetDifficultyToHard()
    {
        PlayerPrefs.SetString("Difficulty", "hard");
        var hardButton = GameObject.FindGameObjectWithTag("ButtonHard");
        var easyButton = GameObject.FindGameObjectWithTag("ButtonEasy");
        hardButton.GetComponent<UnityEngine.UI.Image>().sprite = BUTTON_ON;
        easyButton.GetComponent<UnityEngine.UI.Image>().sprite = BUTTON_OFF;
    }

    public void SetDifficultyToEasy()
    {
        PlayerPrefs.SetString("Difficulty", "easy");
        var hardButton = GameObject.FindGameObjectWithTag("ButtonHard");
        var easyButton = GameObject.FindGameObjectWithTag("ButtonEasy");
        hardButton.GetComponent<UnityEngine.UI.Image>().sprite = BUTTON_OFF;
        easyButton.GetComponent<UnityEngine.UI.Image>().sprite = BUTTON_ON;
    }
}
