using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultTextInfinity : MonoBehaviour
{
    Text text;
    InfiniteScene infiniteScene;
    void Start()
    {
        infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
        text = GetComponent<Text>();
        text.text = "X " + (5 * infiniteScene.kill);
    }
}
