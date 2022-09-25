using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGroup : MonoBehaviour
{
    public int number;
    Sprite image;
    public Sprite changeImage;
    Button button;
    void Start()
    {
        image = GetComponent<Sprite>();
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (number)
        {
            case 1:
                if (!PlayerPrefs.HasKey("unlockGatling"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 2:
                if (!PlayerPrefs.HasKey("unlockLaser"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 3:
                if (!PlayerPrefs.HasKey("unlockLethal"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 4:
                if (!PlayerPrefs.HasKey("unlockMachinegun"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 5:
                if (!PlayerPrefs.HasKey("unlockMinigun"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 6:
                if (!PlayerPrefs.HasKey("unlockNapalm"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 7:
                if (!PlayerPrefs.HasKey("unlockPlasma"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 8:
                if (!PlayerPrefs.HasKey("unlockRapid"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            case 9:
                if (!PlayerPrefs.HasKey("unlockRocket"))
                {
                    button.interactable = false;
                    image = changeImage;
                }
                break;
            default:
                break;
        }
    }
}
