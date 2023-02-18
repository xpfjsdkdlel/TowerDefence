using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    Fade fade;
    Text money;
    AudioSource audioSource; // 효과음
    BGM bgm; // 배경음악 관리자
    [SerializeField]
    AudioClip clip; // 변경할 배경음악
    [SerializeField]
    GameObject setting;
    [SerializeField]
    Slider bgmSlider;
    [SerializeField]
    Slider sfxSlider;
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
        if (PlayerPrefs.HasKey("bgmVolume"))
            GameData.bgmVolume = PlayerPrefs.GetFloat("bgmVolume");
        if (PlayerPrefs.HasKey("sfxVolume"))
            GameData.sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
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
        audioSource = GetComponent<AudioSource>();
    }
    void LoadGamePlay()
    {
        SceneManager.LoadSceneAsync("GamePlayScene");
    }
    void LoadInfinite()
    {
        SceneManager.LoadSceneAsync("InfiniteScene");
    }
    void LoadTowerUpgrade()
    {
        SceneManager.LoadSceneAsync("TowerUpgradeScene");
    }
    public void SceneChange(string SceneName)
    {
        audioSource.Play();
        if (fade != null)
            fade.FadeOut();
        Invoke("Load" + SceneName,2.0f);
    }
    public void Setting()
    {
        audioSource.Play();
        setting.SetActive(true);
        bgmSlider.value = GameData.bgmVolume;
        sfxSlider.value = GameData.sfxVolume;
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
