using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject backpack;
    public List<SlotScript> backpackSlots;
    public GameObject hotbar;
    public List<SlotScript> hotbarSlots;
    public SlotScript activeSlot;
    public bool inventoryFull = false;
    public GameObject itemPrefab;

    private SeedScript seedScript;
    private MapManager mapManager;
    //private bool placing = false;

    void Start()
    {
        foreach (Transform child in backpack.transform)
        {
            backpackSlots.Add(child.GetComponent<SlotScript>());
        }
        inventoryPanel.SetActive(false);
        seedScript = GetComponent<SeedScript>();
        mapManager = FindAnyObjectByType<MapManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown("i")) //otevirani inventare
        {
            if (inventoryPanel.activeSelf == false) {
                inventoryPanel.SetActive(true);
            } else {
                inventoryPanel.SetActive(false);
            }
        }

        if (activeSlot != null) {
            activeSlot.slotbase.GetComponent<Button>().Select();
        }
        //scrollování hotbaru
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            int previous = hotbarSlots.IndexOf(activeSlot) - 1;
            if (previous < 0) { previous = hotbarSlots.Count - 1; }
            activeSlot = hotbarSlots[previous];
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            int next = hotbarSlots.IndexOf(activeSlot) + 1;
            if (next > hotbarSlots.Count - 1) { next = 0; }
            activeSlot = hotbarSlots[next];
        }
    }
    public void DropItem(Item item, Ornament ornData)
    {
        SoundManager.Instance.PlaySound2D("PickUp");
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //tu by se dalo vyøešit zaokrouhlení, at item padne na grid
        GameObject myInstance = GameObject.Instantiate(itemPrefab, player.transform.position, Quaternion.identity) as GameObject;
        myInstance.GetComponent<ItemDrop>().item = item;
        myInstance.GetComponent<ItemDrop>().targetOrnament = ornData;

    }

    public void AddItemToInventory(Item item, Ornament ornament)
    {
        bool volneMisto = false;

        for (int i = 0; i < 8; i++) //projede cely hotbar
        {
            // Debug.Log("hotbar.iteminslot = " + hotbarSlots[i].itemInSlot);
            //Debug.Log("hotbarSlots[i].itemInSlot = " + hotbarSlots[i].itemInSlot + ", " + "item = " + item);
            if (hotbarSlots[i].itemInSlot == item && item.unstuckable == false)
             {
                hotbarSlots[i].ornamentData = ornament;
                hotbarSlots[i].AddItemToStack(item);
                volneMisto = true;
                break;
             }
            else if (hotbarSlots[i].itemInSlot == null)
            {
                hotbarSlots[i].ornamentData = ornament;
                hotbarSlots[i].AddItem(item);
                volneMisto = true;
                break;
            }
        }

        if (volneMisto) //pokud nasel misto v hotbaru jinak...
        { }
            else
            {
            for (int i = 0; i < backpackSlots.Count; i++) // projede cely inventar, checkuje misto
            {
                if (backpackSlots[i].itemInSlot == null)
                {
                    backpackSlots[i].ornamentData = ornament;
                    backpackSlots[i].AddItem(item);
                    volneMisto = true;
                    break;
                }
            }
        if (volneMisto == false) //jestli nenajde misto ani v backpacku
            { 
            Debug.Log("neni misto v inventar");
            inventoryFull = true;
            } 
        } 
        }

    public void AddItemToInventory(Item item)
    {
        bool volneMisto = false;

        for (int i = 0; i < 8; i++) //projede cely hotbar
        {
            // Debug.Log("hotbar.iteminslot = " + hotbarSlots[i].itemInSlot);
            //Debug.Log("hotbarSlots[i].itemInSlot = " + hotbarSlots[i].itemInSlot + ", " + "item = " + item);
            if (hotbarSlots[i].itemInSlot == item && item.unstuckable == false)
            {
                hotbarSlots[i].AddItemToStack(item);
                volneMisto = true;
                break;
            }
            else if (hotbarSlots[i].itemInSlot == null)
            {
                hotbarSlots[i].AddItem(item);
                volneMisto = true;
                break;
            }
        }

        if (volneMisto) //pokud nasel misto v hotbaru jinak...
        { }
        else
        {
            for (int i = 0; i < backpackSlots.Count; i++) // projede cely inventar, checkuje misto
            {
                if (backpackSlots[i].itemInSlot == null)
                {
                    backpackSlots[i].AddItem(item);
                    volneMisto = true;
                    break;
                }
            }
            if (volneMisto == false) //jestli nenajde misto ani v backpacku
            {
                Debug.Log("neni misto v inventar");
                inventoryFull = true;
            }
        }
    }
    public void ActivateItem()
    {
            switch (activeSlot.itemInSlot.itemType)
            {
                case "seed":
                mapManager.hoverOn = true;
                seedScript.PlacingPlant();
                    break;

                default:
                    Debug.Log("nejde polozit");
                    break;
            }
        }
    }

