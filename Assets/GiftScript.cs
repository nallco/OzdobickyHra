using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftScript : MonoBehaviour
{
    public List<Item> giftContents = new List<Item>();
    private Inventory inventory;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }
    private void OnMouseDown()
    {
        Debug.Log("darek!");
        foreach (var item in giftContents)
        {
            inventory.AddItemToInventory(item);
            
        }
        gameObject.SetActive(false);
    }
}
