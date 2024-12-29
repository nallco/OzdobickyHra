using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Grid map;

    public Tilemap highlightMap;
    public Tilemap groundMap;
    public Tilemap structureMap;

    public Tile highlightTile;

    public Vector3Int gridPositon;

    private TileBase previousHoverTile = null;
    Vector3Int previousTilePosition;
    public bool hoverOn = true; //pro vypinani v inspectoru a tak
    public bool hoverOnDirt = false;

    public TileBase hightlightedTileType;
    public TileBase dirt;
    public List<Vector3Int> blockedTiles = new List<Vector3Int>(); //pro tily na kterch uz neco je (zasazene kytky napr)

    private void Start()
    {
        highlightTile.color = Color.white;
    }
    //je tu trosku sussy mouse position, mozna by to chtelo poupravit
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gridPositon = map.WorldToCell(mousePosition);
        if (hoverOn)
        {
            if (previousHoverTile != highlightMap.GetTile(gridPositon) | previousHoverTile == null)
            {
                HighlightDirt();
                ChangeHighlihtedTile();
            }

        }
        /*
        if(Input.GetMouseButtonDown(0))
        {
            TileBase clickedTile = groundMap.GetTile(gridPositon);
            print (clickedTile);
        } */
    }

    public TileBase ReadHighlightedTile()
    {
        TileBase tile = groundMap.GetTile(gridPositon);
        Debug.Log("clicked tile = " + groundMap.GetTile(gridPositon));
        return tile;
    }

    public void HighlightDirt()
    {
        if (hoverOnDirt)
        {
            if (groundMap.GetTile(gridPositon) == dirt)
            {
                if (IsThisTileFree() == false) {
                    highlightTile.color = Color.red;
                } else
                {
                    highlightTile.color = Color.green;
                }
            }
            else
            {
                    highlightTile.color = Color.white;
            }
        }
    }

    public void ChangeHighlihtedTile()
    {
        highlightMap.SetTile(gridPositon, highlightTile);
        highlightMap.SetTile(previousTilePosition, null);
        previousHoverTile = highlightMap.GetTile(gridPositon);
        previousTilePosition = gridPositon;
        hightlightedTileType = groundMap.GetTile(gridPositon);
        //Debug.Log("zmenen highlight");
    }

    public void GetPositionOnGrid(int maplvl) //ground 1, structure 2, ui 3
    {
        switch (maplvl)
        {
            
        }
    }

    public void BlockTile()
    {
        blockedTiles.Add(gridPositon);
        previousHoverTile = null;
    }

    public void UnBlockTile()
    {
        blockedTiles.Remove(gridPositon);
        previousHoverTile = null;
    }

    public bool IsThisTileFree()
    {
        if (blockedTiles.Contains(gridPositon))
        {
            return false;
        } else
        {
            return true;
        }
        
    }
}
