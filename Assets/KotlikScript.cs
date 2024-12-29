using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotlikScript : MonoBehaviour
{
    public StarMagic magicScript;

    private void Start()
    {
        magicScript = FindAnyObjectByType<StarMagic>();
    }
    private void OnMouseDown()
    {
        magicScript.MagicModeOnOff();
    }
    void Update()
    {
        
    }
}
