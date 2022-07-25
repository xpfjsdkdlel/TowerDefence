using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScene : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
