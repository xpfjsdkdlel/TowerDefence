using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameController gameController;
    public GameObject[] waveList; // ���̺� ����Ʈ
    public bool isClear = false; // ���̺� Ŭ���� ����
    public int wave; // ���� ���̺�
    public int maxWave; // �ִ� ���̺�
    public int totalCount; // �� ���� ��
    public int enemyCount; // ���� ���� ��
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
