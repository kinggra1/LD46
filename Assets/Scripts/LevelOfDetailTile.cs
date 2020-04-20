using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelOfDetailTile : Tile
{
    public Sprite zoomedIn;
    public Sprite zoomedOut;

    private PlayerController player;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        
        if (!player)
        {
            player = GameObject.FindObjectOfType<PlayerController>();
        }

        PlayerController.size playerSize = player.GetSize();

        if (playerSize == PlayerController.size.ohLawd)
        {
            tileData.sprite = zoomedOut;
        } else
        {
            tileData.sprite = zoomedIn;
        }
    }
}