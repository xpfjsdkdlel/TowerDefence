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
    public float speed; // ������ �̵��ӵ�
    public int mineral; // ���͸� óġ���� �� ����
    public GameObject damageText; // ������ �ؽ�Ʈ
    public GameObject deathEffect; // ���Ͱ� ���� �� ������ ����Ʈ
    public GameObject textPos; // ������ �ؽ�Ʈ�� ��µǴ� ��ġ
    public GameController gameController;
    public EnemySpawner enemySpawner;
    public Transform target; // �̵��� ��
    int wavepointIndex = 0;

    public GameObject HPBar; // ü�¹�
    void Start()
    {
        HP = maxHP;
        gameController = GetComponent<GameController>();
        target = WayPoints.points[0];
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
        GameData.mineral += mineral;
        GameData.enemyCount--;
    }
    void GetNextWaypoint()
    {
        if(wavepointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
            GameData.life--;
            GameData.enemyCount--;
            return;
        }
        wavepointIndex++;
        target = WayPoints.points[wavepointIndex];
    }
    void Update()
    {
        if (!GameData.gameover)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
            if (HP <= 0)
            {
                Die();
            }
        }
    }
}
