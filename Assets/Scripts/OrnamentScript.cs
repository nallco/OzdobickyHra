using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Ornament;

public class OrnamentScript : MonoBehaviour
{
    public StarMagic starMagic;

    public Shape shape;
    public Ornament.Color color;
    public Pattern pattern;
    public int price;
    public Ornament ornament;


    private void Start()
    {
        starMagic = FindAnyObjectByType<StarMagic>();
        ornament = FindOrnamentType();
    }

    public Ornament FindOrnamentType()
    {
        foreach (Ornament ornamentType in starMagic.ornamentTypes)
        {
            if (ornamentType.shape == shape)
            {
                if(ornamentType.color == color)
                {
                    if(ornamentType.pattern == pattern)
                    {
                        return ornamentType;
                    }
                }
            }
        }
        return starMagic.ornamentTypes[0];
    }
}
