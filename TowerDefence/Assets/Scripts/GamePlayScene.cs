using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // ����� ���� �ִٸ� �ε��մϴ�.
        if (PlayerPrefs.HasKey("ClearStage"))
        {
            GameData.clearStage = PlayerPrefs.GetInt("ClearStage");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
