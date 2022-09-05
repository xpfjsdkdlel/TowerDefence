using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public int number;
    public int price;
    public GameObject confrim;
    void Start()
    {
        confrim = GameObject.Find("Confrim");
    }
    public void btnClick()
    {
        GameData.index = number;
        GameData.price = price;
        for (int i = 0; i < confrim.transform.childCount; i++)
            confrim.transform.GetChild(i).gameObject.SetActive(true);
    }
}
