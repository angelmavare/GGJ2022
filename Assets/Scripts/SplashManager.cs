using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToMenuScene",5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
