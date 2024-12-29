using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public Plant plant;
    public bool stoppedGrowing = false;
    public int growthStage = 0;
    MapManager mapManager;
    Inventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
        mapManager = FindAnyObjectByType<MapManager>();
        GetComponent<SpriteRenderer>().sprite = plant.stage[0];
        StartCoroutine("Grow");
    }

    IEnumerator Grow()
    {
        while (stoppedGrowing == false)
        {
            growthStage++;
            GetComponent<SpriteRenderer>().sprite = plant.stage[growthStage - 1];
            if (growthStage == plant.stage.Count)
            {
                stoppedGrowing = true;
            }
            yield return new WaitForSeconds(plant.growthTime);
        }
        
    }

    public void Harvest()
    {
        for (int i = 0; i < plant.harvest; i++)
        {
            inventory.AddItemToInventory(plant.produce);
        }
        mapManager.UnBlockTile();
        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (stoppedGrowing == true)
        {
            Harvest();
        }
    }
}
