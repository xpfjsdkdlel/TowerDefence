using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject claer;
    public GameObject fail;
    public void Init()
    {
        claer = transform.GetChild(0).gameObject; // 클리어 할 경우 출력할 UI
        if (transform.childCount >= 2)
            fail = transform.GetChild(1).gameObject; // 실패할 경우 출력할 UI
    }
}
