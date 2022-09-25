using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Napalm : MonoBehaviour
{
    Enemy enemy;
    Tower tower;
    [SerializeField]
    int damage;
    void Start()
    {
        tower = transform.parent.parent.GetComponent<Tower>();
        damage = tower.damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetDamage(damage);
                enemy.Burning();
            }
        }
    }
}
