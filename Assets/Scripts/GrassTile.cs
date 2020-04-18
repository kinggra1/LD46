using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "GrassTile.asset", menuName = "Tiles/Grass")]
public class GrassTile : Tile
{
    public Sprite Grass;
    public Sprite BurntGrass;

    private bool burnt;

    public GrassTile()
    {
        burnt = false;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        tileData.sprite = burnt ? BurntGrass : Grass;
    }

    public void Burn()
    {
        burnt = true;
    }

    public bool IsBurnt()
    {
        return burnt;
    }    

}
