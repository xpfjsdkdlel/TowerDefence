using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP; // ������ �ִ� ü��
    public float HP; // ������ ���� ü��

    public GameObject damageText; // ������ �ؽ�Ʈ
    public GameObject textPos; // ������ �ؽ�Ʈ�� ��µǴ� ��ġ

    public GameObject HPBar; // ü�¹�
    public void GetDamage(int damage)
    {
        GameObject dmgText = Instantiate(damageText, textPos.transform.position, damageText.transform.rotation);
        dmgText.GetComponent<TMP_Text>().text = damage.ToString();
        HP -= damage;
        HPBar.GetComponent<Image>().fillAmount = HP / maxHP;
        Destroy(dmgText, 2.0f);
    }
}
