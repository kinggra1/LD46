using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private PlayerController player;
    private Grid grid;
    private Tilemap tilemap;

    private CanvasElementsNeeded uiData;
    private SceneTransitions sceneTransitions;

    private TileBase burntGrassTile;

    // Start is called before the first frame update
    void Start()
    {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        FindNeededObjects();

        sceneTransitions = GetComponent<SceneTransitions>();

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void FindNeededObjects() {
        player = GameObject.FindObjectOfType<PlayerController>();
        grid = GameObject.FindObjectOfType<Grid>();
        tilemap = GameObject.FindObjectOfType<Tilemap>();

        uiData = GameObject.FindObjectOfType<CanvasElementsNeeded>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTileUnderPlayer();
    }

    private void CheckTileUnderPlayer()
    {
        Vector3Int cellPosition = grid.WorldToCell(player.gameObject.transform.position);
        TileBase tile = tilemap.GetTile(cellPosition);

        //if (tile is GrassTile)
        //{
        //    GrassTile grassTile = (GrassTile)tile;
        //    Debug.Log(grassTile.IsBurnt());
        //    if (!grassTile.IsBurnt())
        //    {
        //        grassTile.Burn();
        //    }
        //}
    }

    public void PortalToScene(string sceneName) {
        sceneTransitions.PortalToScene(sceneName);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        FindNeededObjects();
    }

    public void PauseGame() {
        // This breaks with tweaning. Need to freeze player input and other motion.
        // Time.timeScale = 0f;
    }

    public void ResumeGame() {
        // Time.timeScale = 1f;
    }
}
