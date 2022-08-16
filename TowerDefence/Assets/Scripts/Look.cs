using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public Enemy enemy;
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void LookAt()
    {
        Vector3 direction = enemy.target.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }
    void Update()
    {
        if(enemy.target != null)
            LookAt();
    }
}
