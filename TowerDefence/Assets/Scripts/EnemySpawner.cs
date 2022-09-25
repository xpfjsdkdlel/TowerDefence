using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] waveList; // 웨이브 리스트
    public int wave; // 현재 웨이브
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
    {
        for (int i = 0; i < totalCount; i++)
        {
            Instantiate(waveList[wave - 1], gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameData.gameover == false)
        {
            GameData.wave = wave;
            enemyCount = GameData.enemyCount;
            if (enemyCount <= 0)
            {// 웨이브가 끝났을 때
                wave++;
                if (wave <= maxWave)
                {// 다음 웨이브가 남아있다면 시작
                    nextWave();
                    GameData.mineral += 100 + ((wave / 3) * 50);
                }
                else
                {// 마지막 웨이브였다면 클리어 처리
                    GameData.isClear = true;
                    GameData.gameover = true;
                }
            }
        }
    }
}
