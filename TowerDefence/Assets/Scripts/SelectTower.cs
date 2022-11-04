using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTower : MonoBehaviour
{
    GameController gameController;
    InfiniteScene infiniteScene;
    private void Start()
    {
        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        else
            infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
    }
    private void OnMouseUp()
    {// 타워를 클릭했을때 선택된 상태로 변경
        GameData.selectBlock = transform.parent.gameObject;
        if (gameController != null)
            gameController.selectTower(gameObject);
        else if (infiniteScene != null)
            infiniteScene.selectTower(gameObject);
    }
}
