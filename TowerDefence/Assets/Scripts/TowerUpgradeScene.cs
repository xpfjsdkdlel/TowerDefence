using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeScene : MonoBehaviour
{
    public Fade fade;
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
