using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrnamentTreeScript : MonoBehaviour
{

    public Sprite treeSprite;

    public Inventory inventory;
    public Ornament targetOrnament;

    public bool harvestReady = false;

    public Item ornamentScriptableOb;

    public int treeGrowthTime = 10;

    [Header(" Grown Tree")]
    public Sprite redStarTree;
    public Sprite goldStarTree;
    public Sprite silverStarTree;

    [Header(" Grown Outline")]
    public Sprite outline;
    public Sprite redOutline;
    public Sprite goldOutline;
    public Sprite silverOutline;

    [Header(" Growing Tree")]
    public Sprite redGrowing;
    public Sprite goldGrowing;
    public Sprite silverGrowing;

    [Header(" Growing Outline")]
    public Sprite redGrowingOutline;
    public Sprite goldGrowingOutline;
    public Sprite silverGrowingOutline;

    public MapManager mapManager;

    void Start()
    {
        mapManager = FindAnyObjectByType<MapManager>();
        treeSprite = GetComponent<SpriteRenderer>().sprite;
        inventory = FindAnyObjectByType<Inventory>();
    }

    IEnumerator TreeGrowing(Ornament.Color color)
    {
        yield return new WaitForSeconds(treeGrowthTime);
        GrowTree(color);
    }

    private void OnMouseDown()
    {
        if (harvestReady == false)
        {
            if (inventory.activeSlot.itemInSlot.itemType == "star")
            {
                targetOrnament = inventory.activeSlot.ornamentData;
                PutOnStar(targetOrnament.color);
                inventory.activeSlot.RemoveItem();
                //harvestReady = true;
            }
        } else if (harvestReady)
        {
            HarvestOrnaments();
        }
       
    }

    public void PutOnStar(Ornament.Color color)
    {
        switch (color)
        {
            case Ornament.Color.gold:
                GetComponent<SpriteRenderer>().sprite = goldGrowing;
                break;
            case Ornament.Color.silver:
                GetComponent<SpriteRenderer>().sprite = silverGrowing;
                break;
            case Ornament.Color.red:
                GetComponent<SpriteRenderer>().sprite = redGrowing;
                break;

        }
        StartCoroutine(TreeGrowing(color));
    }

    public void GrowTree(Ornament.Color color)
    {
        switch (color)
        {
            case Ornament.Color.gold:
                GetComponent<SpriteRenderer>().sprite = goldStarTree;
                break;
            case Ornament.Color.silver:
                GetComponent<SpriteRenderer>().sprite = silverStarTree;
                break;
            case Ornament.Color.red:
                GetComponent<SpriteRenderer>().sprite = redStarTree;
                break;
        }
        harvestReady = true;
    }

    public void HarvestOrnaments()
    {
        GetComponent<SpriteRenderer>().sprite = treeSprite;
        harvestReady = false;
        inventory.AddItemToInventory(ornamentScriptableOb, targetOrnament);
    }

    private void OnMouseEnter()
    {
        mapManager.hoverOn = false;
        Debug.Log("enter");
        var sprite = GetComponent<SpriteRenderer>().sprite;
        if (sprite == goldStarTree)
        {
            GetComponent<SpriteRenderer>().sprite = goldOutline;
        } else if (sprite == silverStarTree)
        {
            GetComponent<SpriteRenderer>().sprite = silverOutline;
        } else if (sprite == redStarTree)
        {
            GetComponent<SpriteRenderer>().sprite = redOutline;
        } else if (sprite == treeSprite) {
            GetComponent<SpriteRenderer>().sprite = outline;
        }
        
    }

    private void OnMouseExit()
    {
        mapManager.hoverOn = true;
        Debug.Log("exit");
        var sprite = GetComponent<SpriteRenderer>().sprite;
        if (sprite == goldOutline)
        {
            GetComponent<SpriteRenderer>().sprite = goldStarTree;
        }
        else if (sprite == silverOutline)
        {
            GetComponent<SpriteRenderer>().sprite = silverStarTree;
        }
        else if (sprite == redOutline)
        {
            GetComponent<SpriteRenderer>().sprite = redStarTree;
        }
        else if (sprite == outline)
        {
            GetComponent<SpriteRenderer>().sprite = treeSprite;
        } 
    }
}
