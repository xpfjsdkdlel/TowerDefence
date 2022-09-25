using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    GameController gameController;
    InfiniteScene infiniteScene;
    TowerUpgrade towerUpgrade;
    public Color startColor; // ����� ����
    public Color selectColor; // ����� ���õǾ��� ���� ����
    public Renderer blockColor; // ���
    public bool isBuild = false; // ��Ͽ� Ÿ���� �Ǽ��Ǿ����� ����
    public Tower tower;
    void Start()
    {
        blockColor = gameObject.GetComponent<Renderer>();
        startColor = blockColor.material.color;
        selectColor = Color.blue;
        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        else if(GameObject.Find("InfiniteScene") != null)
            infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
    }
    private void OnMouseUp()
    {
        blockColor.material.color = selectColor;
        GameData.selectBlock = gameObject;
        if (gameController != null)
        {
            towerUpgrade = gameController.towerUpgrade;
            towerUpgrade.close();
        }
        else if(infiniteScene != null)
        {
            towerUpgrade = infiniteScene.towerUpgrade;
            towerUpgrade.close();
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            blockColor.material.color = startColor;
        if (transform.childCount > 0)
            isBuild = true;
        else
            isBuild = false;
    }
}
