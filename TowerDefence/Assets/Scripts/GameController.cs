using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject selectBlock; // ���õ� ���
    GameObject tower; // ������ Ÿ��
    GameObject text; // ������ ������ �� ��µǴ� �޼���
    GameObject selectedTower; // ���õ� Ÿ��
    public Tower towerInfo; // ���õ� Ÿ���� ����
    public TowerUpgrade towerUpgrade;
    Block block;
    Fade fade;
    Result result; // ��� â
    EnemySpawner enemySpawner;
    GameObject spawner; // ���Ͱ� ������ ��ġ
    Text waveText;
    Text mineralText;
    Text lifeText;
    public int mineral; // �ʱ⿡ ���޵Ǵ� ����
    public int life; // �ش� ���������� ������
    WayPoints wayPoints;
    bool end = false;
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
        result = GameObject.Find("Result").GetComponent<Result>();
        result.Init();
        text = Resources.Load<GameObject>("PreFabs/UI/Enough");
    }
    public void BuildTower(string towerName)
    {
        tower = Resources.Load<GameObject>("PreFabs/Tower/" + towerName);
        if (GameData.selectBlock != null)
        {// ����� �������� ��
            block = GameData.selectBlock.GetComponent<Block>();
            selectBlock = GameData.selectBlock;
            Tower buildTower = tower.GetComponentInChildren<Tower>();
            if(block.isBuild == false)
            {// ������ ��Ͽ� Ÿ���� �Ǽ��Ǿ� �����ʴٸ� ���ο� Ÿ���� �Ǽ�
                if (GameData.mineral >= buildTower.totalPrice)
                {// ������ ����ϴٸ� �Ǽ�
                    Instantiate(tower, selectBlock.transform.position + new Vector3(0, 1, 0), Quaternion.identity).transform.parent = selectBlock.transform;
                    GameData.selectBlock = null;
                    block.isBuild = true;
                    GameData.mineral -= buildTower.totalPrice;
                }
                else
                {// ������ �����ϴٸ� ������ �����ϴٴ� UI�� ���
                    GameObject enough = GameObject.Find("Enough");
                    if (enough == null) // �̹� ������� ��� ������� ����
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
            towerUpgrade.open(true); // 3���� Ÿ���� ��� �Ǹ� UI�� ���
        else
            towerUpgrade.open(); // ������ Ÿ���� ��ġ�� UI ���
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
        // ���â ���
        result.claer.SetActive(true);
    }
    void failed()
    {
        // ���â ���
        result.fail.SetActive(true);
    }
    public void LoadNextStage() // ���� ���������� �Ѿ�� �Լ�
    {
        GameData.selectStage++;
        SceneManager.LoadScene("Stage" + GameData.selectStage);
    }
    public void LoadSelectStage() // �������� ���� â���� �Ѿ�� �Լ�
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    public void LoadReStart() // ���������� ������ϴ� �Լ�
    {
        SceneManager.LoadScene("Stage" + GameData.selectStage);
    }
    public void LoadExit() // ����ȭ������ ���� �Լ�
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
        if (!GameData.gameover)
        {
            waveText.text = "WAVE " + GameData.wave;
            mineralText.text = "" + GameData.mineral;
            lifeText.text = "X " + GameData.life;
            if (GameData.life <= 0)
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
