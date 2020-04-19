using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasElementsNeeded : MonoBehaviour
{
    public Image portalFadeImage;
    public GameObject cutsceneUI;
    public Text cutsceneText;

    public void ShowCutsceneUI() {
        cutsceneUI.SetActive(true);
    }

    public void HideCutsceneUI() {
        cutsceneUI.SetActive(false);
    }
}
