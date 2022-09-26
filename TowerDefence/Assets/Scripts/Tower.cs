using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int damage; // 공격력
    public int range; // 사거리
    public int totalPrice; // 타워의 가격
    public int upgradePrice; // 업그레이드에 드는 비용
    public string towerType; // 타워의 종류
    public int towerLevel; // 타워의 레벨
    public GameObject blaze; // 타워가 공격할 때 나오는 섬광
    GameObject target; // 공격 목표
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
        if (t != null)
        {// 즉발 데미지
         // 공격시 총구에서 불꽃이 나오도록 처리
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
        {// 즉발 데미지
         // 공격시 총구에서 불꽃이 나오도록 처리
            for (int i = 0; i < t.childCount; i++)
            {
                firePos = t.GetChild(i).transform.position;
                if (blaze != null)
                    Instantiate(blaze, firePos, gameObject.transform.rotation);
            }
            if (target != null)
                target.GetComponent<Enemy>().GetTrueDamage(damage); // 고정대미지
        }
    }
    void Plasma()
    {
        Transform t = GetComponent<Transform>();
        if (t != null)
        {// 투사체
         // 공격시 총구에서 불꽃이 나오도록 처리
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
    // 타워가 적을 바라보도록 하는 함수
    public void LookAt(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }
    private void OnMouseUp()
    {// 타워를 클릭했을때 선택된 상태로 변경
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
