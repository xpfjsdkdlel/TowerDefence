using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public Fade fade;
    void Start()
    {
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
    void Update()
    {
        
    }
}
