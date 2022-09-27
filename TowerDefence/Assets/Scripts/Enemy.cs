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
    public GameObject damageText; // ����� �ؽ�Ʈ
    public GameObject deathEffect; // ���Ͱ� ���� �� ������ ����Ʈ
    public GameObject textPos; // ����� �ؽ�Ʈ�� ��µǴ� ��ġ
    public Transform target; // �̵��� ��
    int wavepointIndex = 0;
    InfiniteScene infiniteScene;
    EnemySpawnerInfinite esi;
    public bool isBurn = false; // ȭ�� ����
    GameObject flame; // ȭ�� ������ �� ������ ȭ�� ������Ʈ
    int burnDamage; // ȭ�� �����
    public GameObject HPBar; // ü�¹�
    void Start()
    {
        target = WayPoints.points[0];
        if (GameObject.Find("Spawner").GetComponent<EnemySpawnerInfinite>() != null)
        {
            esi = GameObject.Find("Spawner").GetComponent<EnemySpawnerInfinite>();
            maxHP = maxHP * (1 + (0.1f * esi.level));
            armor = armor + esi.level / 3;
        }
        if (GameObject.Find("InfiniteScene") != null)
            infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
        HP = maxHP;
        flame = transform.GetChild(2).gameObject;
    }
    public void GetDamage(int damage)
    {// �Ϲ� �����
        GameObject dmgText = Instantiate(damageText, textPos.transform.position, damageText.transform.rotation);
        int totalDamage = damage - armor;
        if (totalDamage <= 0)
            totalDamage = 1;
        dmgText.GetComponent<TMP_Text>().text = totalDamage.ToString();
        HP -= totalDamage;
        HPBar.GetComponent<Image>().fillAmount = HP / maxHP;
        Destroy(dmgText, 2.0f);
    }
    public void GetTrueDamage(int damage)
    {// ���� �����
        GameObject dmgText = Instantiate(damageText, textPos.transform.position, damageText.transform.rotation);
        dmgText.GetComponent<TMP_Text>().text = damage.ToString();
        HP -= damage;
        HPBar.GetComponent<Image>().fillAmount = HP / maxHP;
        Destroy(dmgText, 2.0f);
    }
    public void ArmorBreak(int damage)
    {
        armor -= damage;
        if (armor < 0)
            armor = 0;
    }
    public void Burning(int damage)
    {
        if(!isBurn)
        {
            burnDamage = damage;
            isBurn = true;
            StartCoroutine("Burn", 0.5f);
        }
    }
    IEnumerator Burn()
    {// ȭ���� �Ծ��� ���
        if (isBurn && HP >= 100)
        {// ü���� 100�̻��̸� ȭ�������� ����
            GetTrueDamage(burnDamage); // ȭ�������� ���������
            flame.SetActive(true);// ��Ÿ�� ��� �߰�
            yield return new WaitForSeconds(0.5f);
            StartCoroutine("Burn", 0.5f);
        }
        else
        {// ü���� 100�̸����� ��������� ȭ���� Ǯ��
            isBurn = false;
            flame.SetActive(false);
        }
        yield break;
    }
    void Die()
    {// ���Ͱ� �׾��� ���
        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity); // �����̹��� ���
        Destroy(gameObject);
        GameData.mineral += mineral;
        GameData.enemyCount--;
        if(infiniteScene != null)
        {
            infiniteScene.AddKill(); // ų ����
        }
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
            if (Vector3.Distance(transform.position, target.position) <= 0.1f)
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
