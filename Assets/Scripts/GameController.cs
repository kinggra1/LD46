using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    private PlayerController player;
    private Grid grid;
    private Tilemap tilemap;

    private TileBase burntGrassTile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        grid = GameObject.FindObjectOfType<Grid>();
        tilemap = GameObject.FindObjectOfType<Tilemap>();

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
}
