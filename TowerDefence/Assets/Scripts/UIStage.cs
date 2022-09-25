using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStage : MonoBehaviour
{
    private List<StageButton> stageBtns = new List<StageButton>();
    private int currentPage = 0;
    private int maxPage = 0;
    public void Init()
    {
        int page = GameData.totalStage / 4;
        int lastPage = GameData.totalStage % 4;
        if (lastPage > 0)
            page++;
        maxPage = page;
        Transform t = transform.Find("LeftPage");
        if (t != null)
        {
            Button button = t.GetComponent<Button>();
            button.onClick.AddListener(OnClickLeftPage);
        }
        t = transform.Find("RightPage");
        if (t != null)
        {
            Button button = t.GetComponent<Button>();
            button.onClick.AddListener(OnClickRightPage);
        }
        stageBtns.AddRange(GetComponentsInChildren<StageButton>(true));
        foreach (var btn in stageBtns)
        {
            // 버튼의 초기화함수를 호출합니다.
            // 스테이지 관리 ui의 함수를 전달합니다.
            btn.Init(OnClickStage);
        }
        SetPage(0);
    }
    void OnClickLeftPage()
    {
        --currentPage;
        if (currentPage < 0)
            currentPage = 0;

        SetPage(currentPage);
    }
    void OnClickRightPage()
    {
        ++currentPage;
        if (currentPage >= maxPage)
            currentPage = maxPage - 1;
        SetPage(currentPage);
    }
    void MoveToStage()
    {
        SceneManager.LoadSceneAsync("Stage" + GameData.selectStage.ToString());
    }
    void OnClickStage(int stage)
    {
        Fade fade = GameObject.FindObjectOfType<Fade>();
        if (fade != null)
            fade.FadeOut();
        Invoke("MoveToStage", 1.0f);
        GameData.selectStage = stage;
    }
    void SetPage(int page)
    {
        for (int i = 0; i < 4; ++i)
        {
            int stage = i + 1 + page * 4;

            // 구한 값이 최대 스테이지 이상이라면
            // 현재 시점의 버튼을 비활성화 합니다.
            stageBtns[i].gameObject.SetActive(true);

            if (GameData.totalStage < stage)
                stageBtns[i].gameObject.SetActive(false);

            // 클리어된 스테이지라면 별 이미지와 체크이미지는 켜두고,
            // 락이미지를 꺼둡니다.
            if (stage < GameData.clearStage + 1)
            {
                stageBtns[i].SetButtonInfo(stage, true);
            }
            // 클리어된 스테이지가 아니지만 현재 플레이할 스테이지라면
            // 버튼의 기능을 켜두고, 별이미지, 락이미지를 
            // 보이지 않도록 처리합니다.
            else if (stage == GameData.clearStage + 1)
                stageBtns[i].SetButtonInfo(stage, false, true);
            // 클리어된 스테이지가 아니라면 버튼의 기능을 꺼둡니다.
            else
                stageBtns[i].SetButtonInfo(stage, false);

        }
    }
}
