using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP; // 몬스터의 최대 체력
    public float HP; // 몬스터의 현재 체력

    public GameObject damageText; // 데미지 텍스트
    public GameObject textPos; // 데미지 텍스트가 출력되는 위치

    public GameObject HPBar; // 체력바
    public void GetDamage(int damage)
    {
        GameObject dmgText = Instantiate(damageText, textPos.transform.position, damageText.transform.rotation);
        dmgText.GetComponent<TMP_Text>().text = damage.ToString();
        HP -= damage;
        HPBar.GetComponent<Image>().fillAmount = HP / maxHP;
        Destroy(dmgText, 2.0f);
    }
}
