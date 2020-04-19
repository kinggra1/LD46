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

    private float grassFuelValue = 0.1f;
    private float waterDamageRate = -10f;

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

        CheckTileUnderPlayer();
    }

    private void CheckTileUnderPlayer()
    {
        if (!tilemap) {
            return;
        }

        Vector3Int cellPosition = grid.WorldToCell(player.gameObject.transform.position);
        Tile tile = tilemap.GetTile<Tile>(cellPosition);
        if (tile && tile.sprite)
        {
            if (tile.sprite.name == "Grass")
            {
                BurnGrass(cellPosition);
            }
            if (tile.sprite.name == "Water")
            {
                player.AddFuel(waterDamageRate * Time.deltaTime);
            }
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

    public void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
