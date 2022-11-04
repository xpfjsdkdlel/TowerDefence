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
    // ��ư�� Ŭ���ɶ� ȣ��� �Լ�
    void OnClickButton()
    {
        // �ܺο��� �޾ƿ� ��������Ʈ ( �Լ� )
        if (handle != null)
        {
            int stageIndex = 0;
            int.TryParse(name, out stageIndex);
            handle.Invoke(stageIndex);
        }
    }
    public void Init(System.Action<int> handle)
    {
        // ��ư�� Ŭ���Ǿ����� ���������� ����� �Լ���
        // �ܺηκ��� �޽��ϴ�.
        this.handle = handle;
        // ��ư�� �Լ��� �����մϴ�.
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
        // UIStageBtn�� ���� ���ӿ�����Ʈ�� �̸��� 
        // ���ڷ� �����մϴ�.
        name = stage.ToString();
        stageText.text = stage.ToString();

        starImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);

        Button button = GetComponent<Button>();

        if (clear)
        {
            // ��ư�� ����� �մϴ�.
            // �� �̹����� üũ�̹����� �մϴ�.
            button.enabled = true;
            starImage.gameObject.SetActive(true);
        }
        else
        {
            // Ŭ�������� ���� ������������,
            // ���� �÷����� ������ �����������
            // ��ư�� ����� �մϴ�.
            if (current)
                button.enabled = true;

            // ���� ������ �÷����� ���������� �ƴϰ�,
            // Ŭ������ ���������� �ƴ϶�� 
            // ��ư�� ����� ����, ���̹����� �մϴ�.
            else
            {
                button.enabled = false;
                lockImage.gameObject.SetActive(true);
            }
        }
    }
}
