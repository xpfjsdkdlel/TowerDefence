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
        GameData.selectBlock = gameObject;
        if (gameController != null)
        {
            towerUpgrade = gameController.towerUpgrade;
            if(transform.childCount > 0)
                gameController.selectTower(transform.GetChild(0).gameObject);
            else
            {
                DeleteTowerInfo(); // ���� �Ŵ����� ����� ���õ� Ÿ���� null�� �ʱ�ȭ
                towerUpgrade.close();
            }
        }
        else if(infiniteScene != null)
        {
            towerUpgrade = infiniteScene.towerUpgrade;
            if (transform.childCount > 0)
                infiniteScene.selectTower(transform.GetChild(0).gameObject);
            else
            {
                DeleteTowerInfo(); // ���� �Ŵ����� ����� ���õ� Ÿ���� null�� �ʱ�ȭ
                towerUpgrade.close();
            }
        }
    }
    void DeleteTowerInfo()
    {// �ٸ� ����� Ŭ���ϸ� Ÿ�� ���� ����
        if (gameController != null)
        {
            gameController.towerInfo = null;
        }
        else if (infiniteScene != null)
        {
            infiniteScene.towerInfo = null;
        }
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
            blockColor.material.color = startColor;
        if(GameData.selectBlock == gameObject)// ���õ� ����̶�� ���� ����
            blockColor.material.color = selectColor;
        else
            blockColor.material.color = startColor;
        if (transform.childCount > 0)
            isBuild = true; // �̹� Ÿ���� �Ǽ��Ǿ��ٸ� �Ǽ� �Ұ�
        else
            isBuild = false; // Ÿ���� �Ǽ����� �ʾҴٸ� �Ǽ� ����
    }
}
