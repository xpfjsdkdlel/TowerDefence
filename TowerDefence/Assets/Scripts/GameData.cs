using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // �ΰ���
    public static GameObject selectBlock; // ���õ� ���
    public static bool isClear = false; // ���������� Ŭ���� �� ���� ����

    // ���� ������
    public static int selectStage = 1; // ���� ������ ��������
    public static int clearStage = 1; // Ŭ������ �������� �� 
    public static int totalStage = 10; // �ִ� ����������
    public static int money; // ���ӳ� ��ȭ
    public static void Reset()
    {
        isClear = false;
    }
}
