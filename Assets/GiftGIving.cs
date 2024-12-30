using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftGIving : MonoBehaviour
{
    public List<GameObject> gifts;

    private void Start()
    {
    }
    public void GiveGift()
    {
        var randGift = Random.Range(0, gifts.Count -1);
        gifts[randGift].SetActive(true);
    }
}
