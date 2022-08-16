using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameController gameController;
    public GameObject[] waveList; // ���̺� ����Ʈ
    public int wave; // ���� ���̺�
    public int maxWave; // �ִ� ���̺�
    public int totalCount; // �� ���� ��
    public int enemyCount; // ���� ���� ��
    public void Init()
    {
        GameData.wave = 1;
        wave = GameData.wave;
        gameController = GetComponent<GameController>();
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
            yield return new WaitForSeconds(2.0f);
        }
        yield break;
    }
    // Update is called once per frame
    void Update()
    {
        if(GameData.isClear == false)
        {
            GameData.wave = wave;
            enemyCount = GameData.enemyCount;
            if (enemyCount <= 0)
            {
                wave++;
                if (wave <= maxWave)
                    nextWave();
                else
                    GameData.isClear = true;
            }
        }
    }
}
