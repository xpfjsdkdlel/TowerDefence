using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject selectBlock; // 선택된 블록
    GameObject tower; // 생성될 타워
    GameObject text; // 광물이 부족할 때 출력되는 메세지
    GameObject selectedTower; // 선택된 타워
    public Tower towerInfo; // 선택된 타워의 정보
    public TowerUpgrade towerUpgrade;
    Block block;
    Fade fade;
    Result result; // 결과 창
    EnemySpawner enemySpawner;
    GameObject spawner; // 몬스터가 생성될 위치
    Text waveText;
    Text mineralText;
    Text lifeText;
    public int mineral; // 초기에 지급되는 광물
    public int life; // 해당 스테이지의 라이프
    WayPoints wayPoints;
    bool end = false;
    BGM bgm; // 배경음악 관리자
    [SerializeField]
    AudioClip clip; // 변경할 배경음악
    AudioSource audioSource; // 효과음
    void Start()
    {
        GameData.gameover = false;
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
        }
        fade.FadeIn();
        bgm = GameObject.FindObjectOfType<BGM>();
        if (bgm == null)
        {
            bgm = Resources.Load<BGM>("Prefabs/UI/BGM");
            bgm = Instantiate(bgm);
            if (bgm != null)
                bgm.Init();
        }
        bgm.playBGM(clip);
        wayPoints = GameObject.Find("WayPoints").GetComponent<WayPoints>();
        wayPoints.Init();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        mineralText = GameObject.Find("MineralText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        enemySpawner = spawner.GetComponent<EnemySpawner>();
        enemySpawner.Init();
        result = GameObject.Find("Result").GetComponent<Result>();
        result.Init();
        text = Resources.Load<GameObject>("PreFabs/UI/Enough");
        audioSource = GetComponent<AudioSource>();
    }
    public void BuildTower(string towerName)
    {
        tower = Resources.Load<GameObject>("PreFabs/Tower/" + towerName);
        if (GameData.selectBlock != null)
        {// 블록을 선택했을 때
            block = GameData.selectBlock.GetComponent<Block>();
            selectBlock = GameData.selectBlock;
            Tower buildTower = tower.GetComponentInChildren<Tower>();
            if(block.isBuild == false)
            {// 선택한 블록에 타워가 건설되어 있지않다면 새로운 타워를 건설
                if (GameData.mineral >= buildTower.totalPrice)
                {// 광물이 충분하다면 건설
                    Instantiate(tower, selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity).transform.parent = selectBlock.transform;
                    GameData.selectBlock = null;
                    block.isBuild = true;
                    GameData.mineral -= buildTower.totalPrice;
                }
                else
                {// 광물이 부족하다면 광물이 부족하다는 UI를 출력
                    GameObject enough = GameObject.Find("Enough");
                    if (enough == null) // 이미 출력중인 경우 출력하지 않음
                        Instantiate(text, new Vector3(0, 0, 0), Quaternion.identity);
                }
            }
        }
    }
    public void selectTower(GameObject towerObject)
    {
        GameData.selectBlock = towerObject.transform.parent.gameObject;
        selectedTower = towerObject;
        towerInfo = towerObject.transform.GetChild(0).GetComponent<Tower>();
        if(towerInfo.towerLevel == 3)
            towerUpgrade.open(true); // 3레벨 타워일 경우 판매 UI만 출력
        else
            towerUpgrade.open(); // 선택한 타워의 위치에 UI 출력
    }
    public void UpgradeTower()
    {
        if (GameData.mineral >= towerInfo.upgradePrice)
        {
            tower = Resources.Load<GameObject>("PreFabs/Tower/" + towerInfo.towerType + (towerInfo.towerLevel + 1));
            Instantiate(tower, GameData.selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity).transform.parent = GameData.selectBlock.transform;
            GameData.mineral -= towerInfo.upgradePrice;
            Destroy(selectedTower);
        }
        else
        {
            GameObject enough = GameObject.Find("Enough");
            if (enough == null)
                Instantiate(text, new Vector3(0, 0, 0), Quaternion.identity);
        }
        towerUpgrade.close();
    }
    public void SellTower()
    {
        GameData.mineral += towerInfo.totalPrice / 2;
        Destroy(selectedTower);
        towerUpgrade.close();
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
        result.claer.SetActive(true);
        bgm.stopBGM();
    }
    void failed()
    {
        // 결과창 출력
        result.fail.SetActive(true);
        bgm.stopBGM();
    }
    public void LoadNextStage() // 다음 스테이지로 넘어가는 함수
    {
        GameData.selectStage++;
        SceneManager.LoadSceneAsync("Stage" + GameData.selectStage);
    }
    public void LoadSelectStage() // 스테이지 선택 창으로 넘어가는 함수
    {
        SceneManager.LoadSceneAsync("GamePlayScene");
    }
    public void LoadReStart() // 스테이지를 재시작하는 함수
    {
        SceneManager.LoadSceneAsync("Stage" + GameData.selectStage);
    }
    public void LoadExit() // 메인화면으로 가는 함수
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
    public void SceneChange(string SceneName)
    {
        if (fade != null)
            fade.FadeOut();
        Invoke("Load" + SceneName, 2.0f);
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
        }
        if (!GameData.gameover)
        {
            waveText.text = "WAVE " + GameData.wave;
            mineralText.text = "" + GameData.mineral;
            lifeText.text = "X " + GameData.life;
            if (GameData.life <= 0) // 라이프가 0이되면 게임오버
                GameData.gameover = true;
        }
        else
        {
            if(!end)
            {
                if (GameData.isClear)
                {
                    Invoke("clear", 3f);
                    end = true;
                }
                else
                {
                    Invoke("failed", 3f);
                    end = true;
                }
            }
        }
    }
}
