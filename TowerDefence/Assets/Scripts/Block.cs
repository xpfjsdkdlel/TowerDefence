using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    GameController gameController;
    InfiniteScene infiniteScene;
    TowerUpgrade towerUpgrade;
    public Color startColor; // 블록의 색깔
    public Color selectColor; // 블록이 선택되었을 때의 색깔
    public Renderer blockColor; // 블록
    public bool isBuild = false; // 블록에 타워가 건설되었는지 여부
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
                DeleteTowerInfo(); // 게임 매니저에 저장된 선택된 타워를 null로 초기화
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
                DeleteTowerInfo(); // 게임 매니저에 저장된 선택된 타워를 null로 초기화
                towerUpgrade.close();
            }
        }
    }
    void DeleteTowerInfo()
    {// 다른 블록을 클릭하면 타워 선택 해제
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
        if(GameData.selectBlock == gameObject)// 선택된 블록이라면 색을 변경
            blockColor.material.color = selectColor;
        else
            blockColor.material.color = startColor;
        if (transform.childCount > 0)
            isBuild = true; // 이미 타워가 건설되었다면 건설 불가
        else
            isBuild = false; // 타워가 건설되지 않았다면 건설 가능
    }
}
