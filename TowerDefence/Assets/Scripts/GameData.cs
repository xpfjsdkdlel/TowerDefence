using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // �ΰ���
    public static GameObject selectBlock; // ���õ� ���
    public static bool isClear = false; // ���������� Ŭ���� �� ���� ����
    public static bool gameover = false; // ���������� ����Ǿ����� ����
    public static int wave; // ���� ���̺�
    public static int mineral; // ����
    public static int life; // ���� ���� ���
    public static int enemyCount; // ���� ���� ��

    // ���� ������
    public static int selectStage = 1; // ���� ������ ��������
    public static int clearStage = 0; // Ŭ������ �������� �� 
    public static int totalStage = 6; // �ִ� ����������
    public static int money = 500; // ���ӳ� ��ȭ
    public static int price = 0; // ������ Ÿ���� ����
    public static int index; // ������ Ÿ���� �ε���
    public static int unlockGatling = 0;
    public static int unlockLaser = 0;
    public static int unlockLethal = 0;
    public static int unlockMachinegun = 0;
    public static int unlockMinigun = 0;
    public static int unlockNapalm = 0;
    public static int unlockPlasma = 0;
    public static void Reset()
    {
        selectBlock = null;
        isClear = false;
        gameover = false;
        wave = 1;
    }
}