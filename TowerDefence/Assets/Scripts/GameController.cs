using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject selectBlock;
    public GameObject tower;
    public Block block;

    void Start()
    {
        
    }
    public void BuildTower(string towerName)
    {
        tower = Resources.Load<GameObject>("PreFabs/Tower/" + towerName);
        if (GameData.selectBlock != null)
        {
            block = GameData.selectBlock.GetComponent<Block>();
            selectBlock = GameData.selectBlock;
            Instantiate(tower, selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            GameData.selectBlock = null;
            block.isBuild = true;
        }
    }
    void Update()
    {
        
    }
}
