using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    public Fade fade;
    Text money;
    void Start()
    {
        money = GameObject.Find("MoneyText").GetComponent<Text>();
        if (PlayerPrefs.HasKey("clearStage"))
            GameData.clearStage = PlayerPrefs.GetInt("clearStage");
        if (PlayerPrefs.HasKey("Money"))
            GameData.money = PlayerPrefs.GetInt("Money");
        money.text = ("X " + GameData.money);
        if (PlayerPrefs.HasKey("unlockGatling"))
            GameData.unlockGatling = 1;
        if (PlayerPrefs.HasKey("unlockLaser"))
            GameData.unlockLaser = 1;
        if (PlayerPrefs.HasKey("unlockLethal"))
            GameData.unlockLethal = 1;
        if (PlayerPrefs.HasKey("unlockMachinegun"))
            GameData.unlockMachinegun = 1;
        if (PlayerPrefs.HasKey("unlockMinigun"))
            GameData.unlockMinigun = 1;
        if (PlayerPrefs.HasKey("unlockNapalm"))
            GameData.unlockNapalm = 1;
        if (PlayerPrefs.HasKey("unlockPlasma"))
            GameData.unlockPlasma = 1;
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
    }
    void LoadGamePlay()
    {
        SceneManager.LoadScene("GamePlayScene");
    }
    void LoadInfinite()
    {
        SceneManager.LoadScene("InfiniteScene");
    }
    void LoadTowerUpgrade()
    {
        SceneManager.LoadScene("TowerUpgradeScene");
    }
    public void SceneChange(string SceneName)
    {
        if (fade != null)
            fade.FadeOut();
        Invoke("Load" + SceneName,2.0f);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
