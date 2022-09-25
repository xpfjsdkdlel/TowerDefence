using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerUpgradeScene : MonoBehaviour
{
    public Fade fade;
    public GameObject confrim;
    Text money;
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
        money = GameObject.Find("MoneyText").GetComponent<Text>();
        if (PlayerPrefs.HasKey("Money"))
            GameData.money = PlayerPrefs.GetInt("Money");
        money.text = ("X " + GameData.money);
        confrim = GameObject.Find("Confrim");
    }
    public void YClick()
    {
        if (GameData.money < GameData.price)
        {
            GameObject impo = Resources.Load<GameObject>("Prefabs/UI/Impo");
            GameObject del = Instantiate(impo);
            Destroy(del, 2f);
            NClick();
        }
        else
        {
            buyTower(GameData.index);
            GameData.money -= GameData.price;
            money.text = ("X " + GameData.money);
            PlayerPrefs.SetInt("Money", GameData.money);
            NClick();
        }
    }
    public void NClick()
    {
        for (int i = 0; i < confrim.transform.childCount; i++)
            confrim.transform.GetChild(i).gameObject.SetActive(false);
    }
    public void buyTower(int number)
    {
        switch(number)
        {
            case 0:
                GameData.unlockGatling = 1;
                PlayerPrefs.SetInt("unlockGatling", 1);
                break;
            case 1:
                GameData.unlockLaser = 1;
                PlayerPrefs.SetInt("unlockLaser", 1);
                break;
            case 2:
                GameData.unlockLethal = 1;
                PlayerPrefs.SetInt("unlockLethal", 1);
                break;
            case 3:
                GameData.unlockMachinegun = 1;
                PlayerPrefs.SetInt("unlockMachinegun", 1);
                break;
            case 4:
                GameData.unlockMinigun = 1;
                PlayerPrefs.SetInt("unlockMinigun", 1);
                break;
            case 5:
                GameData.unlockNapalm = 1;
                PlayerPrefs.SetInt("unlockNapalm", 1);
                break;
            default:
                break;
        }
    }
    void LoadMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void SceneChange()
    {
        if (fade != null)
            fade.FadeOut();
        Invoke("LoadMain", 2.0f);
    }
    void Update()
    {
        
    }
}
