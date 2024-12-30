using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    [TextArea(3, 10)]
    public string itenDescription;
    public string itemType;
    public Sprite itemIcon;
    public bool unstuckable;

    [Header("Ingredients")]
    public Ornament.Color color;
    [Header("Seeds")]
    public Plant targetPlant; //pro seminka
    //public MonoScript itemScript;
    public ScriptableObject itemExtencion;
}
