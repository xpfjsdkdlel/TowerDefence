using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waveList; // ���̺� ����Ʈ
    [SerializeField]
    private int[] waveCount; // �� ���̺��� ���� ��

    private int wave; // ���� ���̺�
    private int maxWave; // �ִ� ���̺�
    private int enemyCount; // ���� ���� ��
    public void Init()
    {
        GameData.Reset();
        wave = GameData.wave;
        maxWave = waveList.Length;
        enemyCount = waveCount[0];
        GameData.enemyCount = waveCount[0];
        Invoke("nextWave", 5);
    }
    void nextWave()
    {
        GameData.enemyCount = waveCount[wave - 1];
        StartCoroutine("spawn");
    }
    IEnumerator spawn()
    {
        for (int i = 0; i < waveCount[wave - 1]; i++)
        {
            Instantiate(waveList[wave - 1], gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(1f);
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
            {// ���̺갡 ������ ��
                wave++;
                if (wave <= maxWave)
                {// ���� ���̺갡 �����ִٸ� ����
                    nextWave();
                    GameData.mineral += 100 + ((wave / 3) * 50);
                }
                else
                {// ������ ���̺꿴�ٸ� Ŭ���� ó��
                    GameData.isClear = true;
                    GameData.gameover = true;
                }
            }
        }
    }
}
