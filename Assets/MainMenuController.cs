using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {
            PlayGame();
        }

    }

    public void PlayGame() {
        GameController.instance.PortalToScene("Intro");
    }

    public void Exit() {
        Application.Quit();
    }
}
