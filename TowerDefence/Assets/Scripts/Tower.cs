using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage; // ���ݷ�
    public int range; // ��Ÿ�
    public int totalPrice; // Ÿ���� ����
    public int upgradePrice; // ���׷��̵忡 ��� ���
    public GameObject target; // ���� ��ǥ
    public GameObject blaze; // Ÿ���� ������ �� ������ ����
    Animator animator;
    Vector3 firePos;
    void Start()
    {
        animator = GetComponent<Animator>();
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
        {
            // ���ݽ� �ѱ����� �Ҳ��� �������� ó��
            for (int i = 0; i < t.childCount; i++)
            {
                firePos = t.GetChild(i).transform.position;
                if (blaze != null)
                    Instantiate(blaze, firePos, gameObject.transform.rotation);
            }
            // ��� �����
            target.GetComponent<Enemy>().GetDamage(damage);
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
    void Update()
    {
        UpdateTarget();
        if(target != null)
            LookAt(target.transform.position);
    }
}
