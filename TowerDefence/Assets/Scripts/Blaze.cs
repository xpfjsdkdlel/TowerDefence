using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaze : MonoBehaviour
{
    [SerializeField]
    float time;
    void Start()
    {
        Destroy(gameObject, time);
    }
}
