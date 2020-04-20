using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            RestertGame();
        }

    }

    public void RestertGame() {
        GameController.instance.PortalToScene("Intro");
    }

    public void Exit() {
        Application.Quit();
    }
}
