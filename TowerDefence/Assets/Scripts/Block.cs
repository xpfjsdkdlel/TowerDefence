using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Color startColor;
    public Color selectColor;
    public Renderer blockColor;
    public bool isBuild = false;
    void Start()
    {
        blockColor = gameObject.GetComponent<Renderer>();
        startColor = blockColor.material.color;
        selectColor = Color.blue;
    }
    private void OnMouseUp()
    {
        if (isBuild == false)
        {
            blockColor.material.color = selectColor;
            GameData.selectBlock = gameObject;
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            blockColor.material.color = startColor;
    }
}
