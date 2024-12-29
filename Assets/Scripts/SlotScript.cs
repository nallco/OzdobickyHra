
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item itemInSlot;
    public int itemCount = 0;
    public TMP_Text itemCountText;
    public Inventory inventoryScript;
    public GameObject itemPlace;
    private bool hover = false;
    public Image slotbase;
    public bool cauldronSlot = false;
    public bool full = false;

    void Start()
    {
        inventoryScript = FindAnyObjectByType<Inventory>();
    }

    private void Update()
    {
      if (itemInSlot != null & hover & Input.GetMouseButtonDown(1)) {
            Debug.Log("vyhozeno 1x " + itemInSlot.name);
            inventoryScript.DropItem(itemInSlot);
            RemoveItem();
        }
    }
    public void SelectSlot()
    {
        inventoryScript = FindAnyObjectByType<Inventory>();
        Debug.Log(inventoryScript.activeSlot);
        inventoryScript.activeSlot = transform.parent.GetComponent<SlotScript>();

    }

    public void AddItem(Item it)
    {
        if (itemInSlot == null)
        {
            itemInSlot = it;
            itemCount = 1;
        }
        itemPlace.SetActive(true);
        itemPlace.GetComponent<Image>().sprite = itemInSlot.itemIcon;
    }
    public void AddItemToStack(Item it)
    {
        itemCount++;
        if (itemCount > 1)
        {
            itemCountText.gameObject.SetActive(true);
            itemCountText.text = itemCount.ToString();
        }
    }
    public void RemoveItem() {
        itemCount--;
        if (itemCount == 0)
        {
            itemInSlot = null;
            itemPlace.SetActive(false);
            itemCountText.gameObject.SetActive(false);
        } else if (itemCount == 1)
        {
            itemCountText.gameObject.SetActive(false);
        }
        itemCountText.text = itemCount.ToString();
    }
    /*
    void OnPointerExit()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("vyhozeno 1x " + itemInSlot.name);
            RemoveItem();
        }
    }
    */
    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
    }

    public void AddToCauldron()
    {
        SlotScript activeSlot = inventoryScript.activeSlot;
        if (activeSlot.itemInSlot != null) // v inventari ANO //
        {
            if (full == false) //v inventari ANO a v kotliku NE =======> z inv do kotliku
            {
                itemPlace.SetActive(true);
                AddItem(activeSlot.itemInSlot);
                itemInSlot = activeSlot.itemInSlot;
                activeSlot.RemoveItem();
                full = true;
            }
            else if (inventoryScript.inventoryFull == false) //v inventari ANO a v kotliku ANO ======> vymena
            {
                Item itemNaVymenu = itemInSlot; //ukladam item v tomto slotu
                itemInSlot = null;
                AddItem(activeSlot.itemInSlot);
                //itemInSlot = activeSlot.itemInSlot;
                //itemPlace.GetComponent<Image>().sprite = itemInSlot.itemIcon;
                inventoryScript.AddItemToInventory(itemNaVymenu); //pridama do inventare item v tomto slotu
                activeSlot.RemoveItem(); //odebiram item z aktivniho slotu
            }
            else
            {
                Debug.Log("neco se posralo v kotliku");
            }
        } else if ((full == true) && (itemInSlot != null)) //v inventari NE a v kotliku ANO
        {
            activeSlot.AddItem(itemInSlot);
            full = false;
            RemoveItem();

        } else //v obou NE
        {
        }
    }

}

