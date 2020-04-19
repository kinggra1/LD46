using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneTextSceneController : MonoBehaviour
{

    public string[] textToDisplay;
    public Object nextScene;

    private CanvasElementsNeeded canvas;

    private int textIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<CanvasElementsNeeded>();
        canvas.ShowCutsceneUI();

        if (textToDisplay.Length > 0) {
            canvas.cutsceneText.text = textToDisplay[textIndex];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            textIndex += 1;
            if (textIndex < textToDisplay.Length) {
                canvas.cutsceneText.text = textToDisplay[textIndex];
            } else {
                GameController.instance.PortalToScene(nextScene.name);
            }
        }
    }
}
