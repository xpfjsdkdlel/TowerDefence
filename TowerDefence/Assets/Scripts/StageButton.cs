using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageButton : MonoBehaviour
{
    TMP_Text stageText;
    Image starImage;
    Image lockImage;
    System.Action<int> handle;
    // 버튼이 클릭될때 호출될 함수
    void OnClickButton()
    {
        // 외부에서 받아온 델리게이트 ( 함수 )
        if (handle != null)
        {
            int stageIndex = 0;
            int.TryParse(name, out stageIndex);
            handle.Invoke(stageIndex);
        }
    }
    public void Init(System.Action<int> handle)
    {
        // 버튼이 클릭되었을때 실질적으로 실행될 함수를
        // 외부로부터 받습니다.
        this.handle = handle;
        // 버튼에 함수를 연결합니다.
        Button button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(OnClickButton);

        Transform t = transform.Find("StageText");
        if (t != null)
            stageText = t.GetComponent<TMP_Text>();
        t = transform.Find("SBack/SFor");
        if (t != null)
            starImage = t.GetComponent<Image>();
        t = transform.Find("Lock");
        if (t != null)
            lockImage = t.GetComponent<Image>();
    }
    public void SetButtonInfo(int stage, bool clear, bool current = false)
    {
        // UIStageBtn이 붙은 게임오브젝트의 이름을 
        // 숫자로 변경합니다.
        name = stage.ToString();
        stageText.text = stage.ToString();

        starImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);

        Button button = GetComponent<Button>();

        if (clear)
        {
            // 버튼의 기능을 켭니다.
            // 별 이미지와 체크이미지를 켭니다.
            button.enabled = true;
            starImage.gameObject.SetActive(true);
        }
        else
        {
            // 클리어하지 않은 스테이지지만,
            // 현재 플레이할 시점의 스테이지라면
            // 버튼의 기능을 켭니다.
            if (current)
                button.enabled = true;

            // 현재 시점에 플레이할 스테이지도 아니고,
            // 클리어한 스테이지도 아니라면 
            // 버튼의 기능을 끄고, 락이미지를 켭니다.
            else
            {
                button.enabled = false;
                lockImage.gameObject.SetActive(true);
            }
        }
    }
}
