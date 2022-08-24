using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPriceText : MonoBehaviour
{
    public int price;
    Text towerText;
    void Start()
    {
        towerText = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        towerText.text = "" + price;
        if (price > GameData.mineral)
            towerText.color = Color.red;
        else
            towerText.color = Color.white;
    }
}
