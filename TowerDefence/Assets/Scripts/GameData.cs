using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    // 인게임
    public static GameObject selectBlock; // 선택된 블록
    public static bool isClear = false; // 스테이지를 클리어 한 상태 여부
    public static bool gameover = false; // 스테이지가 종료되었는지 여부
    public static int wave; // 현재 웨이브
    public static int mineral; // 광물
    public static int life; // 현재 남은 목숨
    public static int enemyCount; // 현재 적의 수

    // 게임 데이터
    public static int selectStage = 1; // 현재 선택한 스테이지
    public static int clearStage = 0; // 클리어한 스테이지 수 
    public static int totalStage = 6; // 최대 스테이지수
    public static int money = 500; // 게임내 재화
    public static int price = 0; // 선택한 타워의 가격
    public static int index; // 선택한 타워의 인덱스
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