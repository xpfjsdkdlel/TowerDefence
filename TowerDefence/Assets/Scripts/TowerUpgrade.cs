using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField]
    Tower towerinfo;
    [SerializeField]
    Text upText;
    [SerializeField]
    Text sellText;
    GameObject upgrade;
    GameObject sell;
    bool isOpen = false;
    GameController gameController;
    InfiniteScene infiniteScene;
    void Start()
    {
        upgrade = transform.GetChild(0).gameObject;
        sell = transform.GetChild(1).gameObject;
        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        else
            infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
    }
    public void open(bool level3 = false)
    {// 타워를 선택하면 업그레이드, 판매 버튼 활성화
        if(level3)
        {// 타워가 3레벨일 경우 판매 버튼만 활성화
            upgrade.GetComponent<Button>().interactable = false;
            sell.GetComponent<Button>().interactable = true;
        }
        else
        {
            upgrade.GetComponent<Button>().interactable = true;
            sell.GetComponent<Button>().interactable = true;
        }
        isOpen = true;
    }
    public void close()
    {
        upgrade.GetComponent<Button>().interactable = false;
        sell.GetComponent<Button>().interactable = false;
        isOpen = false;
    }
    void Update()
    {
        if(isOpen)
        {
            if(gameController != null)
            {
                upText.text = "" + gameController.towerInfo.upgradePrice;
                sellText.text = "" + (gameController.towerInfo.totalPrice / 2);
            }
            else
            {
                upText.text = "" + infiniteScene.towerInfo.upgradePrice;
                sellText.text = "" + (infiniteScene.towerInfo.totalPrice / 2);
            }
        }
        else if(!isOpen)
        {
            upText.text = " - ";
            sellText.text = " - ";
        }
    }
}
