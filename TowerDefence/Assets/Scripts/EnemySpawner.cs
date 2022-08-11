using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameController gameController;
    public GameObject[] waveList; // 웨이브 리스트
    public bool isClear = false; // 웨이브 클리어 여부
    public int wave; // 현재 웨이브
    public int maxWave; // 최대 웨이브
    public int totalCount; // 총 적의 수
    public int enemyCount; // 현재 적의 수
    public void Init()
    {
        wave = 1;
        gameController = GetComponent<GameController>();
        maxWave = waveList.Length;
        enemyCount = totalCount;
        Invoke("nextWave", 5);
    }
    void nextWave()
    {
        StartCoroutine("spawn");
    }
    IEnumerator spawn()
    {
        for (int i = 0; i < totalCount; i++)
        {
            Instantiate(waveList[wave - 1], gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
        yield break;
    }
    void clear()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            isClear = true;
            wave++;
            enemyCount = totalCount;
        }
        if (isClear)
        {
            if (wave <= maxWave)
                nextWave();
            else
                GameData.isClear = true;
        }
    }
}
