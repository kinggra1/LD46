using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasElementsNeeded : MonoBehaviour
{
    public Image portalFadeImage;

    public GameObject cutsceneUI;
    public Text cutsceneText;

    public GameObject squirrelStatsUI;
    public Text collectedSticks;
    public Text collectedBushes;
    public Text collectedLogs;
    public Text collectedTrees;
    public Text collectedSquirrels;

    public void ShowCutsceneUI() {
        cutsceneUI.SetActive(true);
    }

    public void HideCutsceneUI() {
        cutsceneUI.SetActive(false);
    }

    public void ShowSquirrelStatsUI() {
        squirrelStatsUI.SetActive(true);
    }
}
