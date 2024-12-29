using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedScript : MonoBehaviour
{
    private Inventory inventory;
    private SlotScript slot;
    private MapManager mapManager;
    //private ItemDrop itemDrop;
    private TileBase dirt;
    public GameObject plantPrefab;
    private Plant targetPlant;
    public Transform plantParent;

    void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        inventory = GetComponent<Inventory>();
        //itemDrop = GetComponent<ItemDrop>();
        dirt = mapManager.dirt;
    }
    void Update()
    {
        if ((inventory.activeSlot.itemInSlot != null) && (inventory.activeSlot.itemInSlot.itemType == "seed"))
        {
            PlacingPlant();
        } else
        {
            mapManager.highlightTile.color = Color.white;
            mapManager.hoverOnDirt = false;
        }
    }

    public void PlacingPlant()
    {
        targetPlant = inventory.activeSlot.itemInSlot.targetPlant;
        mapManager.hoverOnDirt = true;
        if ((Input.GetMouseButtonDown(0)) && (mapManager.hightlightedTileType == mapManager.dirt))
        {
            Debug.Log("plant plant");
            Plant();
        }
    }

    public void Plant()
    {
        if (mapManager.IsThisTileFree())
        {
            Debug.Log(inventory.activeSlot.itemInSlot + "plant planted");
            GameObject plant = Instantiate(plantPrefab, mapManager.gridPositon, Quaternion.identity); //+0.5 +0.5
            plant.transform.position = plant.transform.position + new Vector3(0.5f, 0.5f, 0);
            Debug.Log(targetPlant);
            plant.GetComponent<PlantScript>().plant = targetPlant;
            plant.transform.SetParent(plantParent);
            inventory.activeSlot.RemoveItem(); // :)
            mapManager.BlockTile();
        } else
        {
            Debug.Log("blockedTile");
        } 
    }
}
