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

        canvas.collectedSticks.gameObject.SetActive(false);
        canvas.collectedBushes.gameObject.SetActive(false);
        canvas.collectedLogs.gameObject.SetActive(false);
        canvas.collectedTrees.gameObject.SetActive(false);
        canvas.collectedSquirrels.gameObject.SetActive(false);


        canvas.collectedSticks.text = StatsController.instance.GetStickStatsString();
        canvas.collectedBushes.text = StatsController.instance.GetBushStatsString();
        canvas.collectedLogs.text = StatsController.instance.GetLogStatsString();
        canvas.collectedTrees.text = StatsController.instance.GetTreeStatsString();
        canvas.collectedSquirrels.text = StatsController.instance.GetSquirrelStatsString();

        LeanTween.delayedCall(1.0f, () => ActivateStat(canvas.collectedSticks.gameObject));
        LeanTween.delayedCall(1.3f, () => ActivateStat(canvas.collectedBushes.gameObject));
        LeanTween.delayedCall(1.6f, () => ActivateStat(canvas.collectedLogs.gameObject));
        LeanTween.delayedCall(1.9f, () => ActivateStat(canvas.collectedTrees.gameObject));
        LeanTween.delayedCall(2.2f, () => ActivateStat(canvas.collectedSquirrels.gameObject));
    }

    private void ActivateStat(GameObject statObject) {
        statObject.SetActive(true);
        AudioController.instance.PlayConsumeSound(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            NextScene();
        }
    }

    public void NextScene() {
        GameController.instance.PortalToScene(nextScene);
    }
}
