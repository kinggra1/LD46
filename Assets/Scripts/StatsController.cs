using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public static StatsController instance;

    private int sticksCollectedLevel;
    private int totalSticksLevel;
    private int sticksCollectedGlobal;
    private int totalSticksGlobal;
    private string stickStatsString;

    private int bushesCollectedLevel;
    private int totalBushesLevel;
    private int bushesCollectedGlobal;
    private int totalBushesGlobal;
    private string bushStatsString;

    private int logsCollectedLevel;
    private int totalLogsLevel;
    private int logsCollectedGlobal;
    private int totalLogsGlobal;
    private string logStatsString;

    private int treesCollectedLevel;
    private int totalTreesLevel;
    private int treesCollectedGlobal;
    private int totalTreesGlobal;
    private string treeStatsString;

    private int squirrelsCollectedLevel;
    private int totalSquirrelsLevel;
    private int squirrelsCollectedGlobal;
    private int totalSquirrelsGlobal;
    private string squirrelStatsString;

    public void Awake() {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    public void StartLevelStats() {
        totalSticksLevel = GameObject.FindGameObjectsWithTag("Stick").Length;
        totalSticksGlobal += totalSticksLevel;

        totalBushesLevel = GameObject.FindGameObjectsWithTag("Bush").Length;
        totalBushesGlobal += totalBushesLevel;

        totalLogsLevel = GameObject.FindGameObjectsWithTag("Log").Length;
        totalLogsGlobal += totalLogsLevel;

        totalTreesLevel = GameObject.FindGameObjectsWithTag("Tree").Length;
        totalTreesGlobal += totalTreesLevel;

        totalSquirrelsLevel = GameObject.FindGameObjectsWithTag("Squirrel").Length;
        totalSquirrelsGlobal += totalSquirrelsLevel;
    }

    public void EndLevelStats() { 
        int remaining = GameObject.FindGameObjectsWithTag("Stick").Length;
        sticksCollectedLevel = totalSticksLevel - remaining;
        sticksCollectedGlobal += sticksCollectedLevel;
        stickStatsString = FormatStatsString("Sticks: ", sticksCollectedLevel, totalSticksLevel);

        bushesCollectedLevel = CountBurntBushes();
        bushesCollectedGlobal += bushesCollectedLevel;
        bushStatsString = FormatStatsString("Bushes: ", bushesCollectedLevel, totalBushesLevel);

        remaining = GameObject.FindGameObjectsWithTag("Log").Length;
        logsCollectedLevel = totalLogsLevel - remaining;
        logsCollectedGlobal += logsCollectedLevel;
        logStatsString = FormatStatsString("Logs: ", logsCollectedLevel, totalLogsLevel);

        treesCollectedLevel = CountBurntTrees();
        treesCollectedGlobal += treesCollectedLevel;
        treeStatsString = FormatStatsString("Trees: ", treesCollectedLevel, totalTreesLevel);

        remaining = GameObject.FindGameObjectsWithTag("Squirrel").Length;
        squirrelsCollectedLevel = totalSquirrelsLevel - remaining;
        squirrelsCollectedGlobal += squirrelsCollectedLevel;
        squirrelStatsString = FormatStatsString("Squirrels: ", squirrelsCollectedLevel,  totalSquirrelsLevel);
    }

    private int CountBurntBushes() {
        int total = 0;
        foreach (GameObject tree in GameObject.FindGameObjectsWithTag("Bush")) {
            BurnableObject burnable = tree.GetComponent<BurnableObject>();
            if (burnable.IsBurned()) {
                total++;
            }
        }
        return total;
    }

    private int CountBurntTrees() {
        int total = 0;
        foreach(GameObject tree in GameObject.FindGameObjectsWithTag("Tree")) {
            BurnableObject burnable = tree.GetComponent<BurnableObject>();
            if (burnable.IsBurned()) {
                total++;
            }
        }
        return total;
    }

    private string FormatStatsString(string prefix, int collected, int total) {
        return prefix + collected + "/" + total;
    }

    public string GetStickStatsString() {
        return stickStatsString;
    }

    public string GetBushStatsString() {
        return bushStatsString;
    }

    public string GetLogStatsString() {
        return logStatsString;
    }

    public string GetTreeStatsString() {
        return treeStatsString;
    }

    public string GetSquirrelStatsString() {
        return squirrelStatsString;
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
