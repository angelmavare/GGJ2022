using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatCollider : MonoBehaviour
{
    public string character;
    private string playerTag;
    public GameObject aviso;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (character == "red") {
            playerTag = "PlayerRed";
        }
        else
        {
            playerTag = "PlayerBlue";
        }
        if (collision.gameObject.tag == playerTag) {
            aviso.SetActive(true);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
