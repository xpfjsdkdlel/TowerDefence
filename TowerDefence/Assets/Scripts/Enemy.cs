using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float maxHP; // 몬스터의 최대 체력
    public float HP; // 몬스터의 현재 체력
    public int armor; // 몬스터의 방어력
    public int mineral; // 몬스터를 처치했을 때 보상
    public GameObject damageText; // 데미지 텍스트
    public GameObject deathEffect; // 몬스터가 죽을 때 나오는 이펙트
    public GameObject textPos; // 데미지 텍스트가 출력되는 위치
    public GameController gameController;
    public EnemySpawner enemySpawner;

    public GameObject HPBar; // 체력바
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
