using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    Image leftImage;
    Image rightImage;
    AudioSource audioSource;
    float speed = 1.0f;
    bool isOpen = false;
    bool update = false;
    float elapsed = 0;
    public void Init()
    {
        Transform t = transform.Find("Gate(L)");
        if(t != null)
            leftImage = t.GetComponent<Image>();
        t = transform.Find("Gate(R)");
        if (t != null)
            rightImage = t.GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    // ���� ��ȯ�Ǳ� �� ����Ʈ�� ����
    public void FadeOut()
    {
        audioSource.Play();
        update = true;
        elapsed = 0;
        isOpen = true;
    }
    // ���� ��ȯ�� �� ����Ʈ�� ����
    public void FadeIn()
    {
        audioSource.Play();
        update = true;
        elapsed = 0;
        isOpen = false;
    }
    void Update()
    {
        // ������Ʈ �Ǿ��ٸ� �Լ��� �����մϴ�.
        if (update == false)
            return;
        elapsed += Time.deltaTime / speed;
        // ���� ���¶�� ����Ʈ�� �ݽ��ϴ�.
        if (isOpen)
        {
            if (leftImage != null)
                leftImage.transform.localPosition = Vector3.Lerp(new Vector3(-1100, 0, 0), Vector3.zero, elapsed);
            if (rightImage != null)
                rightImage.transform.localPosition = Vector3.Lerp(new Vector3(1100, 0, 0), Vector3.zero, elapsed);
        }
        // ���� ���¶�� ����Ʈ�� ���ϴ�.
        else
        {
            if (leftImage != null)
                leftImage.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-1100, 0, 0), elapsed);
            if (rightImage != null)
                rightImage.transform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(1100, 0, 0), elapsed);
        }
        if (elapsed > 1.0f)
            update = false;
    }
}
