using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range; // ��Ÿ�
    public GameObject target;
    public Animator animator;
    public GameObject blaze; // Ÿ���� ������ �� ������ ����
    public Vector3 firePos;
    // Start is called before the first frame update
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
        if(t != null)
            for(int i = 0; i < t.childCount; i++)
            {
                firePos = t.GetChild(i).transform.position;
                Instantiate(blaze, firePos, gameObject.transform.rotation);
            }
    }
    // Ÿ���� ���� �ٶ󺸵��� �ϴ� �Լ�
    public void LookAt(Vector3 position)
    {
        // x�� ȸ���� ���� �ʵ��� y���� 0���� �����մϴ�.
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
