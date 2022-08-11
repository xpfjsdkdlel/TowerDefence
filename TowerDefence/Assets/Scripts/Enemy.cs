using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP; // ������ �ִ� ü��
    public float HP; // ������ ���� ü��
    public int armor; // ������ ����
    public int mineral; // ���͸� óġ���� �� ����
    public GameObject damageText; // ������ �ؽ�Ʈ
    public GameObject deathEffect; // ���Ͱ� ���� �� ������ ����Ʈ
    public GameObject textPos; // ������ �ؽ�Ʈ�� ��µǴ� ��ġ
    public GameController gameController;
    public EnemySpawner enemySpawner;

    public GameObject HPBar; // ü�¹�
    private void Start()
    {
        HP = maxHP;
        gameController = GetComponent<GameController>();
    }
    public void GetDamage(int damage)
    {
        GameObject dmgText = Instantiate(damageText, textPos.transform.position, damageText.transform.rotation);
        int totalDamage = damage - armor;
        dmgText.GetComponent<TMP_Text>().text = totalDamage.ToString();
        HP -= totalDamage;
        HPBar.GetComponent<Image>().fillAmount = HP / maxHP;
        Destroy(dmgText, 2.0f);
    }
    void Die()
    {
        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    void Update()
    {
        if (HP <= 0)
        {
            Die();
        }
    }
}
