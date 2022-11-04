using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIStage : MonoBehaviour
{
    List<StageButton> stageBtns = new List<StageButton>();
    int currentPage = 0;
    int maxPage = 0;
    BGM bgm; // ������� ������
    [SerializeField]
    AudioClip clip; // ������ �������
    AudioSource audioSource; // ȿ����
    GameObject gpScene;
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
            // ��ư�� �ʱ�ȭ�Լ��� ȣ���մϴ�.
            // �������� ���� ui�� �Լ��� �����մϴ�.
            btn.Init(OnClickStage);
        }
        SetPage(0);
        // ����� �ҽ��� ã���ϴ�.
        gpScene = GameObject.Find("GamePlaySceneManager");
        audioSource = gpScene.GetComponent<AudioSource>();
    }
    void OnClickLeftPage()
    {
        audioSource.Play();
        --currentPage;
        if (currentPage < 0)
            currentPage = 0;
        SetPage(currentPage);
    }
    void OnClickRightPage()
    {
        audioSource.Play();
        ++currentPage;
        if (currentPage >= maxPage)
            currentPage = maxPage - 1;
        SetPage(currentPage);
    }
    void MoveToStage()
    {
        bgm = GameObject.FindObjectOfType<BGM>();
        if(bgm != null)
            bgm.stopBGM();
        SceneManager.LoadSceneAsync("Stage" + GameData.selectStage.ToString());
    }
    void OnClickStage(int stage)
    {
        Fade fade = GameObject.FindObjectOfType<Fade>();
        if (fade != null)
            fade.FadeOut();
        Invoke("MoveToStage", 1.0f);
        GameData.selectStage = stage;
        audioSource.Play();
    }
    void SetPage(int page)
    {
        for (int i = 0; i < 4; ++i)
        {
            int stage = i + 1 + page * 4;

            // ���� ���� �ִ� �������� �̻��̶��
            // ���� ������ ��ư�� ��Ȱ��ȭ �մϴ�.
            stageBtns[i].gameObject.SetActive(true);

            if (GameData.totalStage < stage)
                stageBtns[i].gameObject.SetActive(false);

            // Ŭ����� ����������� �� �̹����� üũ�̹����� �ѵΰ�,
            // ���̹����� ���Ӵϴ�.
            if (stage < GameData.clearStage + 1)
            {
                stageBtns[i].SetButtonInfo(stage, true);
            }
            // Ŭ����� ���������� �ƴ����� ���� �÷����� �����������
            // ��ư�� ����� �ѵΰ�, ���̹���, ���̹����� 
            // ������ �ʵ��� ó���մϴ�.
            else if (stage == GameData.clearStage + 1)
                stageBtns[i].SetButtonInfo(stage, false, true);
            // Ŭ����� ���������� �ƴ϶�� ��ư�� ����� ���Ӵϴ�.
            else
                stageBtns[i].SetButtonInfo(stage, false);

        }
    }
}
