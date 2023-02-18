using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().volume = GameData.sfxVolume;
        Destroy(gameObject, 3f);
    }
}
