using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public int mineral;
    public int life;
    WayPoints wayPoints;

    void Start()
    {
        GameData.isClear = false;
        GameData.mineral = mineral;
        GameData.life = life;
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
        wayPoints = GameObject.Find("WayPoints").GetComponent<WayPoints>();
        wayPoints.Init();
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
    void clear()
    {
        GameData.money += 100 * GameData.selectStage;
        GameData.isClear = false;
        if (GameData.clearStage < GameData.selectStage)
            GameData.clearStage = GameData.selectStage;
        PlayerPrefs.SetInt("clearStage",GameData.clearStage);
        PlayerPrefs.SetInt("Money", GameData.money);
        // 결과창 출력
    }
    void failed()
    {
        // 결과창 출력
    }
    public void nextStage() // 다음 스테이지로 넘어가는 함수
    {
        GameData.selectStage++;
        SceneManager.LoadScene("Stage" + GameData.selectStage);
    }
    public void LoadSelectStage() // 스테이지 선택 창으로 넘어가는 함수
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    public void LoadReStart() // 스테이지를 재시작하는 함수
    {
        SceneManager.LoadScene("Stage" + GameData.selectStage);
    }
    public void LoadExit() // 메인화면으로 가는 함수
    {
        SceneManager.LoadScene("MainScene");
    }
    public void SceneChange(string SceneName)
    {
        if (fade != null)
            fade.FadeOut();
        Invoke("Load" + SceneName, 2.0f);
    }
    void Update()
    {
        waveText.text = "WAVE " + GameData.wave;
        mineralText.text = "" + GameData.mineral;
        lifeText.text = "X " + GameData.life;
        if (GameData.isClear)
            clear();
        else if (life <= 0)
            failed();
    }
}
