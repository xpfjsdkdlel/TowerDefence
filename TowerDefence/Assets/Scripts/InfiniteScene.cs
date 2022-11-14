using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InfiniteScene : MonoBehaviour
{
    GameObject selectBlock; // 선택된 블록
    GameObject tower; // 생성될 타워
    GameObject text; // 광물이 부족할 때 출력되는 메세지
    GameObject selectedTower; // 선택된 타워
    public Tower towerInfo; // 선택된 타워의 정보
    public TowerUpgrade towerUpgrade;
    Block block;
    Fade fade;
    Result result; // 결과창
    public int kill = 0; // 적 처치 수
    EnemySpawnerInfinite esi;
    GameObject spawner; // 몬스터가 생성될 위치
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
        mineralText = GameObject.Find("MineralText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        esi = spawner.GetComponent<EnemySpawnerInfinite>();
        esi.Init();
        result = GameObject.Find("Result").GetComponent<Result>();
        result.Init();
        text = Resources.Load<GameObject>("PreFabs/UI/Enough");
        audioSource = GetComponent<AudioSource>();
    }
    public void BuildTower(string towerName)
    {
        tower = Resources.Load<GameObject>("PreFabs/Tower/" + towerName);
        if (GameData.selectBlock != null)
        {
            block = GameData.selectBlock.GetComponent<Block>();
            selectBlock = GameData.selectBlock;
            Tower buildTower = tower.GetComponentInChildren<Tower>();
            if (block.isBuild == false)
            {
                if (GameData.mineral >= buildTower.totalPrice)
                {
                    Instantiate(tower, selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity).transform.parent = selectBlock.transform;
                    GameData.selectBlock = null;
                    block.isBuild = true;
                    GameData.mineral -= buildTower.totalPrice;
                }
                else
                {
                    GameObject enough = GameObject.Find("Enough");
                    if (enough == null)
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
        if (towerInfo.towerLevel == 3)
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
        GameData.money += 5 * kill;
        PlayerPrefs.SetInt("Money", GameData.money);
        // 결과창 출력
        result.claer.SetActive(true);
    }
    public void LoadExit() // 메인화면으로 가는 함수
    {
        if (fade != null)
            fade.FadeOut();
        Invoke("SceneChange", 2.0f);
    }
    void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void AddKill()
    {
        kill++;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.Play();
        }
        if (!GameData.gameover)
        {
            mineralText.text = "" + GameData.mineral;
            lifeText.text = "X " + GameData.life;
            if (GameData.life <= 0) // 라이프가 0이되면 게임오버
                GameData.gameover = true;
        }
        else
        {
            if (!end)
            {
                Invoke("clear", 3f);
                end = true;
            }
        }
    }
}