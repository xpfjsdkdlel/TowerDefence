using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject claer;
    public GameObject fail;
    public void Init()
    {
        claer = transform.GetChild(0).gameObject;
        fail = transform.GetChild(1).gameObject;
    }
}
