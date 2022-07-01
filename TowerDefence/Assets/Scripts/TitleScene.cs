using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown)
            Invoke("SceneChange", 1.0f);
    }
    void SceneChange()
    {
        SceneManager.LoadScene("MainScene");
    }
}
