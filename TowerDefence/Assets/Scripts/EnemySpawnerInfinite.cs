using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerInfinite : MonoBehaviour
{
    public GameObject[] waveList; // 웨이브 리스트
    public int wave; // 현재 웨이브
    public int level = 0; // 몬스터의 레벨
    public int maxWave; // 최대 웨이브
    public int totalCount; // 총 적의 수
    public int enemyCount; // 현재 적의 수
    public void Init()
    {
        GameData.Reset();
        wave = GameData.wave;
        maxWave = waveList.Length;
        enemyCount = totalCount;
        GameData.enemyCount = totalCount;
        Invoke("nextWave", 5);
    }
    void nextWave()
    {
        GameData.enemyCount = totalCount;
        StartCoroutine("spawn");
    }
    IEnumerator spawn()
    {// 웨이브 리스트에 있는 몬스터를 소환
        for (int i = 0; i < totalCount; i++)
        {
            Instantiate(waveList[wave - 1], gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
        yield break;
    }
    void Update()
    {
        if(GameData.gameover == false)
        {// 게임이 진행중인 경우
            GameData.wave = wave;
            enemyCount = GameData.enemyCount;
            if (enemyCount <= 0)
            {// 웨이브가 끝났을 때
                wave++;
                if (wave <= maxWave)
                {// 다음 웨이브 시작
                    nextWave();
                    GameData.mineral += 100 + ((wave / 3) * 50);
                }
                else
                {// 마지막 웨이브였다면 몬스터를 강화해서 다시 시작
                    level++;
                    wave = 1;
                    nextWave();
                }
            }
        }
    }
}
