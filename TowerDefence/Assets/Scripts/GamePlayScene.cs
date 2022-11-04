using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayScene : MonoBehaviour
{
    Fade fade;
    BGM bgm; // ������� ������
    [SerializeField]
    AudioClip clip; // ������ �������
    AudioSource audioSource; // ȿ����
    void Start()
    {
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
        UIStage uiStage = GameObject.FindObjectOfType<UIStage>();
        if (uiStage != null)
            uiStage.Init();
        // ����� ���� �ִٸ� �ε��մϴ�.
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
        audioSource.Play();
        if (fade != null)
            fade.FadeOut();
        Invoke("LoadMain", 2.0f);
    }
}
