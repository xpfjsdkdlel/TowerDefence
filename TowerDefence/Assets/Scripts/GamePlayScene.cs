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
        if (fade != null)
            fade.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
