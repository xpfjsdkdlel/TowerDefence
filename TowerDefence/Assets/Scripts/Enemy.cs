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
    public float speed; // 몬스터의 이동속도
    public int mineral; // 몬스터를 처치했을 때 보상
    public GameObject damageText; // 대미지 텍스트
    public GameObject deathEffect; // 몬스터가 죽을 때 나오는 이펙트
    public GameObject textPos; // 대미지 텍스트가 출력되는 위치
    public Transform target; // 이동할 곳
    int wavepointIndex = 0;
    InfiniteScene infiniteScene;
    EnemySpawnerInfinite esi;
    public bool isBurn = false; // 화상 상태
    GameObject flame; // 화상 상태일 때 생성할 화염 오브젝트
    int burnDamage; // 화상 대미지
    public GameObject HPBar; // 체력바
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
    {// 일반 대미지
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
    {// 고정 대미지
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
    {// 화상을 입었을 경우
        if (isBurn && HP >= 100)
        {// 체력이 100이상이면 화상대미지를 입음
            GetTrueDamage(burnDamage); // 화상대미지는 고정대미지
            flame.SetActive(true);// 불타는 모션 추가
            yield return new WaitForSeconds(0.5f);
            StartCoroutine("Burn", 0.5f);
        }
        else
        {// 체력이 100미만으로 내려갈경우 화상이 풀림
            isBurn = false;
            flame.SetActive(false);
        }
        yield break;
    }
    void Die()
    {// 몬스터가 죽었을 경우
        Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity); // 폭발이미지 출력
        Destroy(gameObject);
        GameData.mineral += mineral;
        GameData.enemyCount--;
        if(infiniteScene != null)
        {
            infiniteScene.AddKill(); // 킬 증가
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
