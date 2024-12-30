using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTreeScript : MonoBehaviour
{
    public GameObject camera2;
    public List<GameObject> ozdoby;
    public int chybejiciOzdoby;
    public Inventory inventory;

    public Sprite bigTreeSprite;
    public Sprite outline;

    public GiftGIving giftGiving;
    
    void Start()
    {
        bigTreeSprite = GetComponent<SpriteRenderer>().sprite;
        chybejiciOzdoby = ozdoby.Count;
        inventory = FindAnyObjectByType<Inventory>();
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = outline;
        camera2.SetActive(true);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = bigTreeSprite;
        camera2.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log("ozdobeno!");
        if (inventory.activeSlot.itemInSlot.itemType == "ornament")
        {
            var ornament = inventory.activeSlot.ornamentData;
            DecorateTree(ornament);
            Debug.Log("ozdobeno!");
        }
    }

    public void DecorateTree(Ornament orn)
    {
        var randomOzdoba = ozdoby[Random.Range(0, ozdoby.Count -1)];
        Debug.Log(randomOzdoba.ToString());
        randomOzdoba.SetActive(true);
        randomOzdoba.GetComponent<SpriteRenderer>().sprite = orn.sprite;
        ozdoby.Remove(randomOzdoba);
        inventory.activeSlot.RemoveItem();
        giftGiving.GiveGift();
    }
}
