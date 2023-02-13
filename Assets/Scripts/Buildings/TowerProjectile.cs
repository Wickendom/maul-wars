using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private Transform target;

    private float speed;
    private int damage;
    public void Seek(Transform _target, float moveSpeed,int damage)
    {
        target = _target;
        speed = moveSpeed;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }
    }

    private void HitTarget()
    {
        target.SendMessage("TakeDamage", damage);
        Destroy(gameObject);
    }
}
