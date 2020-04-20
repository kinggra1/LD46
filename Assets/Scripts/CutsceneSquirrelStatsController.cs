using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneSquirrelStatsController : MonoBehaviour
{
    public string nextScene;

    private CanvasElementsNeeded canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<CanvasElementsNeeded>();
        canvas.ShowSquirrelStatsUI();
        canvas.collectedSquirrels.text = GameController.instance.GetSquirrilStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            GameController.instance.PortalToScene(nextScene);
        }
    }
}
