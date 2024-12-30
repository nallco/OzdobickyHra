using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemDrop : MonoBehaviour
{
    public Item item;
    Inventory inventory;
    private string itemType;
    public TileBase dirt;
    private MapManager mapM;
    public Ornament targetOrnament;

    private void Start()
    {
        mapM = FindAnyObjectByType<MapManager>();
        inventory = FindAnyObjectByType<Inventory>();
        itemType = item.itemType;
        if (itemType != "star")
        {
            GetComponent<SpriteRenderer>().sprite = item.itemIcon;
        }
        if ((itemType == "ornament") || (itemType == "star"))
        {
            OrnamentScript ornamentS;
            ornamentS = gameObject.AddComponent<OrnamentScript>();
            //pridat parametry ozdoby (shape, color, pattern)
            if (itemType == "ornament")
            {
                GetComponent<SpriteRenderer>().sprite = targetOrnament.sprite;
            } else
            {
                GetComponent<SpriteRenderer>().sprite = targetOrnament.starSprite;
            }
            
        }
    }

    public void ChangeSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        Debug.Log("zmen");
    }
    private void OnMouseDown()
    {
        PickUp();
    }
    public void PickUp()
    {
        SoundManager.Instance.PlaySound2D("PickUp");
        if (targetOrnament != null)
        {
            inventory.AddItemToInventory(item, targetOrnament);
        } else
        {
            inventory.AddItemToInventory(item);
        }
        if (inventory.inventoryFull == false)
        {
            Destroy(this.gameObject);
        }
    }
    /*
    public class Stars
    {
        public Ornaments targetOrnament;
    }

    public class Ornaments
    {
        public Shape shape;
        public Color color;
        public Pattern pattern;
        public int price;

        public Ornaments(Shape sh, Color co, Pattern pa)
        {
            shape = sh;
            color = co;
            pattern = pa;
        }
    }
    public enum Shape
    {
        ball, cone
    }
    public enum Color
    {
        silver, red, gold
    }
    public enum Pattern
    {
        snowflakes, stripes, bare
    }
    */
}
