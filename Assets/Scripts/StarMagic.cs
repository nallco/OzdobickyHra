using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Ornament;

public class StarMagic : MonoBehaviour
{
    public GameObject kotlik;
    public Inventory inventory;
    public List<SlotScript> kotlikSlots = new List<SlotScript>();
    public bool magicOn = false;
    public Button magicButton;
    public ItemDrop itemPrefab;
    public List<Ornament> ornamentTypes = new List<Ornament>();
    

    [Header("base")]
    public Item starBase;
    public Item ornamentBase;

    private void Start()
    {
        inventory = FindAnyObjectByType<Inventory>();
    }

    public void MagicModeOnOff()
    {
        if (magicOn == false)
        {
            OpenMagic();
        } else if (magicOn == true)
        {
            CloseMagic();
        }
    }

    public void OpenMagic()
    {
        magicOn = true;
        foreach (SlotScript child in kotlikSlots)
        {
            child.gameObject.SetActive(true);
        }
        magicButton.gameObject.SetActive(true);
    }

    public void CloseMagic()
    {
        magicOn = false;
        foreach (SlotScript child in kotlikSlots)
        {
            child.gameObject.SetActive(false);
        }
        magicButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (((Input.GetAxis("Horizontal") != 0 | Input.GetAxis("Vertical") != 0)) && (magicOn))
        {
            CloseMagic() ;
        }
    }

    public void CreateStar()
    {
        CloseMagic();
        //pridat trigger pro anim
        Ornament targetOrnament = GetOrnamentByRecipe();

        ItemDrop hvezda = Instantiate(itemPrefab, kotlik.transform.position + new Vector3(0, 2, 0), Quaternion.identity) as ItemDrop;
        hvezda.item = starBase;
        hvezda.ChangeSprite(targetOrnament.starSprite);
        magicButton.gameObject.SetActive(false);
    }

    Ornament.Color createdColor;
    Ornament.Pattern createdPattern;

    public Ornament GetOrnamentByRecipe()
    {
        int redCount = 0;
        int silverCount = 0;
        int redStar = 0;
        int whiteStar = 0;
        int snowDrop = 0;

        foreach (SlotScript slot in kotlikSlots)
        {
            var ingredient = slot.itemInSlot;
            if (ingredient.color == Ornament.Color.red) redCount++;
            else if (ingredient.color == Ornament.Color.silver) silverCount++;

            if (ingredient.itemName == "Red poinsettia") redStar++;
            if (ingredient.itemName == "White poinsettia") whiteStar++;
            if (ingredient.itemName == "Snowdrop") snowDrop++;
        } //seèteny barvy + zjisteno jestli obsahuje obì hvìzdy
        Debug.Log ("redstars = " + redStar + " " + "whiteStar = " + whiteStar);
        if ((redStar > 0) && (whiteStar > 0)) {
            createdColor = Ornament.Color.gold;
        } else if (redCount > 2) {
            createdColor = Ornament.Color.red;
        } else if (silverCount > 2) {
            createdColor = Ornament.Color.silver;
        } else {
            if (Random.Range(1, 2) == 1)
            {
                createdColor = Ornament.Color.red;
            } else
            {
                createdColor = Ornament.Color.silver;
            }
        } //zjistena barva ozdoby

            // ÈERVENÁ
            if (createdColor == Ornament.Color.red)
            {
                if (snowDrop > 0)
                {
                    createdPattern = Pattern.snowflakes;
                } else if (silverCount == 1)
                {
                    createdPattern = Pattern.stripes;
                } else
                {
                    createdPattern = Pattern.bare;
                }
            }

            // STØÍBRNÁ
            if (createdColor == Ornament.Color.silver)
            {
                if (snowDrop == 2)
                {
                    createdPattern = Pattern.snowflakes;
                } else if (redCount == 1)
                {
                    createdPattern = Pattern.stripes;
                } else
                {
                    createdPattern = Pattern.bare;
                }
            }

            // ZLATÁ
            if (createdColor == Ornament.Color.gold)
            {
                if (redStar + whiteStar == 4)
                {
                    createdPattern = Pattern.stripes;
                } else if (snowDrop == 2)
                {
                    createdPattern = Pattern.snowflakes;
                } else
                {
                    createdPattern = Pattern.bare;
                }
            }

        foreach (Ornament or in ornamentTypes) {
            if (or.pattern == createdPattern)
            {
                if (or.color == createdColor)
                {
                    Debug.Log("created " + createdColor.ToString() + createdPattern.ToString() + " ornament star");
                    return or;
                }
            }
        }
        Debug.Log("ozdoba nenalezena");
        return null;
        
    }

}
