using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range; // 사거리
    public GameObject target;
    public Animator animator;
    public GameObject blaze; // 타워가 공격할 때 나오는 섬광
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
        float shortestDistance = Mathf.Infinity; // 가장 짧은 거리
        GameObject nearEnemy = null; // 가장 가까운 몬스터
        foreach (GameObject enemy in enemys)
        {
            float DistanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // 몬스터와의 거리
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
    // 타워가 적을 바라보도록 하는 함수
    public void LookAt(Vector3 position)
    {
        // x축 회전이 되지 않도록 y값을 0으로 설정합니다.
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
