using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayScene : MonoBehaviour
{
    public Fade fade;
    // Start is called before the first frame update
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

        UIStage uiStage = GameObject.FindObjectOfType<UIStage>();
        if (uiStage != null)
            uiStage.Init();
        // 저장된 값이 있다면 로드합니다.
        if (PlayerPrefs.HasKey("ClearStage"))
        {
            GameData.clearStage = PlayerPrefs.GetInt("ClearStage");
        }
    }
    void LoadMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void SceneChange()
    {
        if (fade != null)
            fade.FadeOut();
        Invoke("LoadMain", 2.0f);
    }
    void Update()
    {
        
    }
}
