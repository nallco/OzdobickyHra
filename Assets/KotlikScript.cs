using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotlikScript : MonoBehaviour
{
    public StarMagic magicScript;
    public Sprite kotlikSprite;
    public Sprite outline;
    

    private void Start()
    {
        kotlikSprite = GetComponent<SpriteRenderer>().sprite;
        magicScript = FindAnyObjectByType<StarMagic>();
    }
    private void OnMouseDown()
    {
        magicScript.MagicModeOnOff();
    }

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = outline;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = kotlikSprite;
    }
    void Update()
    {
        
    }
}
