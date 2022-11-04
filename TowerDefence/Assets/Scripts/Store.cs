using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField]
    int number;
    [SerializeField]
    public int price;
    GameObject confrim;
    Button button;
    Image image;
    GameObject sold;
    AudioSource audioSource;
    
    void Start()
    {
        confrim = GameObject.Find("Confrim");
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        sold = transform.GetChild(4).gameObject;
        audioSource = GameObject.Find("TowerUpgradeScene").GetComponent<AudioSource>();
    }
    public void btnClick()
    {
        audioSource.Play();
        GameData.index = number;
        GameData.price = price;
        for (int i = 0; i < confrim.transform.childCount; i++)
            confrim.transform.GetChild(i).gameObject.SetActive(true);
    }
    private void Update()
    {
        switch (number)
        {
            case 0:
                if (PlayerPrefs.HasKey("unlockGatling"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 1:
                if (PlayerPrefs.HasKey("unlockLaser"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 2:
                if (PlayerPrefs.HasKey("unlockLethal"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 3:
                if (PlayerPrefs.HasKey("unlockMachinegun"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 4:
                if (PlayerPrefs.HasKey("unlockMinigun"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 5:
                if (PlayerPrefs.HasKey("unlockNapalm"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 6:
                if (PlayerPrefs.HasKey("unlockPlasma"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 7:
                if (PlayerPrefs.HasKey("unlockRapid"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            case 8:
                if (PlayerPrefs.HasKey("unlockRocket"))
                {
                    button.interactable = false;
                    image.color = new Color(0, 255, 255, 100);
                    sold.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
