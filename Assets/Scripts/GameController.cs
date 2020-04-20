using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum TileType { NONE, GRASS, BURNT_GRASS, WATER, DEEP_WATER }

    private PlayerController player;
    private Grid grid;
    private Tilemap tilemap;

    private CanvasElementsNeeded uiData;
    private SceneTransitions sceneTransitions;

    private float grassFuelValue = 0.2f;
    private float waterDamageRate = -10f;
    private float deepWaterDamageRate = -50f;

    private int totalSquirrels;
    private int remainingSqirrels;
    private string squirrelString;

    private bool paused = false;

    private void Awake() {
        if (instance) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        FindNeededObjects();

        sceneTransitions = GetComponent<SceneTransitions>();

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void FindNeededObjects() {
        StatsController.instance.StartLevelStats();

        player = GameObject.FindObjectOfType<PlayerController>();
        grid = GameObject.FindObjectOfType<Grid>();
        tilemap = GameObject.FindObjectOfType<Tilemap>();

        uiData = GameObject.FindObjectOfType<CanvasElementsNeeded>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        CheckTilesUnderPlayer();
    }

    private void CheckTilesUnderPlayer()
    {
        if (!tilemap)
        {
            return;
        }
        PlayerController.size playerSize = player.GetSize();

        Vector3Int centerCellPosition = grid.WorldToCell(player.gameObject.transform.position);

        CheckTile(centerCellPosition);

        if (playerSize == PlayerController.size.ohLawd || playerSize == PlayerController.size.medium)
        {
            float waterMultiplier = .1f;
            CheckTile(centerCellPosition + new Vector3Int(1, 0, 0), waterMultiplier);
            CheckTile(centerCellPosition + new Vector3Int(-1, 0, 0), waterMultiplier);
            CheckTile(centerCellPosition + new Vector3Int(0, 1, 0), waterMultiplier);
            CheckTile(centerCellPosition + new Vector3Int(0, -1, 0), waterMultiplier);
        }

        if (playerSize == PlayerController.size.ohLawd)
        {
            CheckTile(centerCellPosition + new Vector3Int(1, 1, 0), 0);
            CheckTile(centerCellPosition + new Vector3Int(-1, 1, 0), 0);
            CheckTile(centerCellPosition + new Vector3Int(1, -1, 0), 0);
            CheckTile(centerCellPosition + new Vector3Int(-1, -1, 0), 0);
        }
    }

    private void CheckTile(Vector3Int cellPosition, float waterMultiplier = 1f)
    {
        Tile tile = tilemap.GetTile<Tile>(cellPosition);

        switch (GetTileType(tile))
        {
            case TileType.NONE:
                break;
            case TileType.GRASS:
                BurnGrass(cellPosition);
                break;
            case TileType.BURNT_GRASS:
                break;
            case TileType.WATER:
                player.AddFuel(waterDamageRate * Time.deltaTime * waterMultiplier);
                break;
            case TileType.DEEP_WATER:
                player.AddFuel(deepWaterDamageRate * Time.deltaTime * waterMultiplier);
                break;
        }
    }

    private void BurnGrass(Vector3Int cellPosition)
    {
        // change sprite
        Tile burntGrassTile = (Tile)Resources.Load("Tiles/BurntGrass", typeof(Tile));
        tilemap.SetTile(cellPosition, burntGrassTile);

        // give fuel to player
        player.AddFuel(grassFuelValue);
    }

    public bool IsWaterHere(Vector3 worldPos) {
        return GetTileTypeAtPos(worldPos) == TileType.WATER;
    }

    public TileType GetTileTypeAtPos(Vector3 pos) {
        return GetTileType(TileAtPosition(pos));
    }

    private Tile TileAtPosition(Vector3 worldPos) {
        return tilemap.GetTile<Tile>(grid.WorldToCell(worldPos));
    }

    private TileType GetTileType(Tile tile) {
        if (tile && tile.sprite) {
            if (tile.sprite.name == "Grass") {
                return TileType.GRASS;
            }
            if (tile.sprite.name == "BurntGrass") {
                return TileType.BURNT_GRASS;
            }
            if (tile.sprite.name == "Water") {
                return TileType.WATER;
            }
            if (tile.sprite.name == "DeepWater")
            {
                return TileType.DEEP_WATER;
            }
        }
        return TileType.NONE;
    }

    public string GetSquirrilStats() {
        return squirrelString;
    }

    public void PortalToScene(string sceneName) {
        StatsController.instance.EndLevelStats();
        sceneTransitions.PortalToScene(sceneName);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode) {
        FindNeededObjects();
    }

    public void PauseGame() {
        paused = true;
    }

    public void ResumeGame() {
        paused = false;
    }

    public bool IsPaused() {
        return paused;
    }

    public void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
