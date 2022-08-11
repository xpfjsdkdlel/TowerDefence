using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject selectBlock; // 선택된 블록
    public GameObject tower; // 생성될 타워
    public Block block;
    public Fade fade;
    EnemySpawner enemySpawner;
    GameObject spawner; // 몬스터가 생성될 위치
    Text waveText;
    Text mineralText;
    Text lifeText;
    public bool stageClear = false;
    public int mineral;
    public int life;

    void Start()
    {
        spawner = GameObject.Find("Spawner");
        fade = GameObject.FindObjectOfType<Fade>();
        if (fade == null)
        {
            fade = Resources.Load<Fade>("Prefabs/UI/Fade");
            fade = Instantiate(fade);
            if (fade != null)
                fade.Init();
            fade.FadeIn();
        }
        else
            fade.FadeIn();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        mineralText = GameObject.Find("MineralText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        enemySpawner = spawner.GetComponent<EnemySpawner>();
        enemySpawner.Init();
    }
    public void BuildTower(string towerName)
    {
        tower = Resources.Load<GameObject>("PreFabs/Tower/" + towerName);
        if (GameData.selectBlock != null)
        {
            block = GameData.selectBlock.GetComponent<Block>();
            selectBlock = GameData.selectBlock;
            Tower buildTower = tower.GetComponentInChildren<Tower>();
            if(mineral >= buildTower.totalPrice)
            {
                Instantiate(tower, selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                GameData.selectBlock = null;
                block.isBuild = true;
            }
            else
                Instantiate(tower, selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }
    public void addMineral(int mineral)
    {
        this.mineral += mineral;
    }
    void Clear()
    {
        GameData.isClear = false;
        if (GameData.clearStage <= GameData.selectStage)
            GameData.clearStage = GameData.selectStage;
        // 결과창 출력
    }
    void failed()
    {
        // 결과창 출력
    }
    void Update()
    {
        waveText.text = "WAVE " + enemySpawner.wave;
        mineralText.text = "" + mineral;
        lifeText.text = "X " + life;
        if (GameData.isClear)
            Clear();
        else if (life <= 0)
            failed();
    }
}
