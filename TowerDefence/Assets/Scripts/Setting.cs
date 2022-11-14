using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    [SerializeField]
    Slider bgmVolume;
    [SerializeField]
    Slider sfxVolume;
    [SerializeField]
    AudioSource clickSound;
    public void changeBgmVolume()
    {
        GameData.bgmVolume = bgmVolume.value;
    }
    public void changeSfxVolume()
    {
        GameData.sfxVolume = sfxVolume.value;
    }
    public void CloseSetting()
    {
        gameObject.SetActive(false);
    }
}
