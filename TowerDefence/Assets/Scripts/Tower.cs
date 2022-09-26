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
    public GameObject blaze; // Ÿ���� ������ �� ������ ����
    GameObject target; // ���� ��ǥ
    GameController gameController;
    InfiniteScene infiniteScene;
    Animator animator;
    Vector3 firePos;
    [SerializeField]
    TowerUpgrade towerUpgrade;
    void Start()
    {
        Transform t = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        if (GameObject.Find("GameController") != null)
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        else
            infiniteScene = GameObject.Find("InfiniteScene").GetComponent<InfiniteScene>();
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
        {
            target = nearEnemy;
            animator.SetBool("ATTACK", true);
        }
        else
        {
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
                    Instantiate(blaze, firePos, gameObject.transform.rotation);
            }
            if (target != null)
            {
                GameObject plasma = Resources.Load<GameObject>("PreFabs/Effect/Plasma" + towerLevel);
                Instantiate(plasma, firePos, gameObject.transform.rotation);
            }
        }
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
        GameData.selectBlock = gameObject.transform.parent.gameObject;
        if (gameController != null)
            gameController.selectTower(gameObject.transform.parent.gameObject);
        else if (infiniteScene != null)
            infiniteScene.selectTower(gameObject.transform.parent.gameObject);
    }
    void Update()
    {
        if (!GameData.gameover)
        {
            if (target == null)
                UpdateTarget();
            if (target != null)
                LookAt(target.transform.position);
        }
    }
}
