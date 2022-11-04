using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage; // ���ݷ�
    public int range; // ��Ÿ�
    public int totalPrice; // Ÿ���� ����
    public int upgradePrice; // ���׷��̵忡 ��� ���
    public string towerType; // Ÿ���� ����
    public int towerLevel; // Ÿ���� ����
    [SerializeField]
    GameObject blaze; // Ÿ���� ������ �� ������ ����
    GameObject target; // ���� ��ǥ
    GameController gameController;
    InfiniteScene infiniteScene;
    Animator animator; // �ִϸ��̼�
    Vector3 firePos; // ����ü�� �Ҳ��� ���� ��ġ
    AudioSource audioSource; // �߻���
    void Start()
    {
        Transform t = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        else
            infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
    void UpdateTarget()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity; // ���� ª�� �Ÿ�
        GameObject nearEnemy = null; // ���� ����� ����
        foreach (GameObject enemy in enemys)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // ���Ϳ��� �Ÿ�
            if (DistanceToEnemy < shortestDistance)
            {
                shortestDistance = DistanceToEnemy;
                nearEnemy = enemy;
            }
        }
        if (nearEnemy != null && shortestDistance <= range)
        {// ���� ����� ���� ����
            target = nearEnemy;
            animator.SetBool("ATTACK", true);
        }
        else
        {// ��Ÿ��ȿ� ���Ͱ� ���� ��� �����·� ��ȯ
            target = null;
            animator.SetBool("ATTACK", false);
        }
    }
    void ATTACK()
    {
        Transform t = GetComponent<Transform>();
        if (t != null)
        {// ��� ������
         // ���ݽ� �ѱ����� �Ҳ��� �������� ó��
            for (int i = 0; i < t.childCount; i++)
            {
                firePos = t.GetChild(i).transform.position;
                if (blaze != null)
                    Instantiate(blaze, firePos, gameObject.transform.rotation);
            }
            if (target != null)
                target.GetComponent<Enemy>().GetDamage(damage);
            audioSource.Play();
        }
    }
    void trueDamageATTACK()
    {
        Transform t = GetComponent<Transform>();
        if (t != null)
        {// ��� ������
         // ���ݽ� �ѱ����� �Ҳ��� �������� ó��
            for (int i = 0; i < t.childCount; i++)
            {
                firePos = t.GetChild(i).transform.position;
                if (blaze != null)
                    Instantiate(blaze, firePos, gameObject.transform.rotation);
            }
            if (target != null)
                target.GetComponent<Enemy>().GetTrueDamage(damage); // ���������
            audioSource.Play();
        }
    }
    void Plasma()
    {
        Transform t = GetComponent<Transform>();
        if (t != null)
        {// ����ü
         // ���ݽ� �ѱ����� �Ҳ��� �������� ó��
            for (int i = 0; i < t.childCount; i++)
            {
                firePos = t.GetChild(i).transform.position;
                if (blaze != null)
                {
                    Instantiate(blaze, firePos, gameObject.transform.rotation);
                    audioSource.Play();
                }
            }
            if (target != null)
            {
                GameObject plasma = Resources.Load<GameObject>("PreFabs/Effect/Plasma" + towerLevel);
                Instantiate(plasma, firePos, gameObject.transform.rotation);
            }
        }
    }
    void soundEffect()
    {
        audioSource.Play();
    }
    // Ÿ���� ���� �ٶ󺸵��� �ϴ� �Լ�
    public void LookAt(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }
    private void OnMouseUp()
    {// Ÿ���� Ŭ�������� ���õ� ���·� ����
        GameData.selectBlock = transform.parent.parent.gameObject;
        if (gameController != null)
            gameController.selectTower(transform.parent.gameObject);
        else if (infiniteScene != null)
            infiniteScene.selectTower(transform.parent.gameObject);
    }
    void Update()
    {
        if (!GameData.gameover)
        {
            if (target != null)
            {// Ÿ���� ������� Ÿ���� �ٶ󺸰�, Ÿ���� ���ų� ��Ÿ��ۿ� ������� Ÿ���� ����
                LookAt(target.transform.position);
                if (Vector3.Distance(transform.position, target.transform.position) > range)
                    target = null;
            }
            else
                UpdateTarget();
        }
        else
            animator.SetBool("ATTACK", false);
    }
}
