using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public int damage;
    public int level;
    public GameObject effect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.GetDamage(damage);
            enemy.ArmorBreak(level * 2);
            Instantiate(effect, gameObject.transform.position, Quaternion.identity); // 폭발이미지 출력
            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Translate(0, 0, 1);
        Destroy(gameObject, 3f);
    }
}
