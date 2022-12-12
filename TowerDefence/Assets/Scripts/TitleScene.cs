using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    BGM bgm; // 배경음악 관리자
    [SerializeField]
    AudioClip clip; // 변경할 배경음악
    private void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<BGM>();
        bgm.playBGM(clip);
    }
    void Update()
    {
        if(Input.anyKeyDown)
            Invoke("SceneChange", 1.0f);
    }
    void SceneChange()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
}
