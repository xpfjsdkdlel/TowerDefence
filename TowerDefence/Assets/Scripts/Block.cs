using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Color startColor; // 블록의 색깔
    public Color selectColor; // 블록이 선택되었을 때의 색깔
    public Renderer blockColor; // 블록
    public bool isBuild = false; // 블록에 타워가 건설되었는지 여부
    public string towerName; // 건설된 타워의 이름
    void Start()
    {
        blockColor = gameObject.GetComponent<Renderer>();
        startColor = blockColor.material.color;
        selectColor = Color.blue;
    }
    private void OnMouseUp()
    {
        blockColor.material.color = selectColor;
        GameData.selectBlock = gameObject;
        if (isBuild == false)
        {
            
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            blockColor.material.color = startColor;
    }
}
