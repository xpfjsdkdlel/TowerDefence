using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerInfinite : MonoBehaviour
{
    public GameObject[] waveList; // ���̺� ����Ʈ
    public int wave; // ���� ���̺�
    public int level = 0; // ������ ����
    public int maxWave; // �ִ� ���̺�
    public int totalCount; // �� ���� ��
    public int enemyCount; // ���� ���� ��
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
    {// ���̺� ����Ʈ�� �ִ� ���͸� ��ȯ
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
        {// ������ �������� ���
            GameData.wave = wave;
            enemyCount = GameData.enemyCount;
            if (enemyCount <= 0)
            {// ���̺갡 ������ ��
                wave++;
                if (wave <= maxWave)
                {// ���� ���̺� ����
                    nextWave();
                    GameData.mineral += 100 + ((wave / 3) * 50);
                }
                else
                {// ������ ���̺꿴�ٸ� ���͸� ��ȭ�ؼ� �ٽ� ����
                    level++;
                    wave = 1;
                    nextWave();
                }
            }
        }
    }
}
