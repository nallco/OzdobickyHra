using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class Plant : ScriptableObject
{
    public List<Sprite> stage;
    public int harvest;
    public int growthTime; //cas pro jednu stage, celkovy cas je tedy (stage.count - 1) * growthTime
    public Item produce;
}
