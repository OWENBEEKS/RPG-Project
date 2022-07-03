using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] bool isHomingProjectile = false;
    [SerializeField] GameObject hitEffect = null;
    Health target = null;
    float damage = 0;
    
    private void Start()
    {
        FindTarget();
    }
    void Update()
    {
        if(target == null) return;
        if (isHomingProjectile && !target.IsDead())
        {
            FindTarget();
        }
        MoveToTarget();
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
    private void FindTarget()
    {
        transform.LookAt(GetAimLocation());
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != target)
        {
            return;
        }
        if(target.IsDead()) return;
        target.TakeDamage(damage);
        if(hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }
        Destroy(gameObject);  
    }

}
