using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Ornament : ScriptableObject
{
    public Shape shape;
    public Color color;
    public Pattern pattern;
    public int price;
    public Sprite sprite;
    public Sprite starSprite;
    [Header("Recipe")]
    public Item recipeS1;
    public Item recipeS2;
    public Item recipeS3;
    public Item recipeS4;


    public enum Shape
    {
        ball, cone
    }
   public enum Color
    {
        silver, red, gold, none
    }
   public enum Pattern
    {
        snowflakes, stripes, bare
    }
}
