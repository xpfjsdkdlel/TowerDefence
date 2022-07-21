using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Color startColor; // ����� ����
    public Color selectColor; // ����� ���õǾ��� ���� ����
    public Renderer blockColor; // ���
    public bool isBuild = false; // ��Ͽ� Ÿ���� �Ǽ��Ǿ����� ����
    public string towerName; // �Ǽ��� Ÿ���� �̸�
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
