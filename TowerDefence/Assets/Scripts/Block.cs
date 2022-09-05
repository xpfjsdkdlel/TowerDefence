using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Transform t;
    public Color startColor; // ����� ����
    public Color selectColor; // ����� ���õǾ��� ���� ����
    public Renderer blockColor; // ���
    public bool isBuild = false; // ��Ͽ� Ÿ���� �Ǽ��Ǿ����� ����
    public Transform buildTower; // �Ǽ��� Ÿ��
    public Tower tower;
    void Start()
    {
        blockColor = gameObject.GetComponent<Renderer>();
        startColor = blockColor.material.color;
        selectColor = Color.blue;
        t = GetComponent<Transform>();
    }
    private void OnMouseUp()
    {
        blockColor.material.color = selectColor;
        GameData.selectBlock = gameObject;
        if (transform.childCount > 0)
            buildTower = transform.GetChild(0);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            blockColor.material.color = startColor;
        if (buildTower == null)
            isBuild = false;
    }
}
