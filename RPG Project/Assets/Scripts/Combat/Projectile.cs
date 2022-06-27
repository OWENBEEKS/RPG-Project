using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 2f;
    Health target = null;

    void Update()
    {
        if(target == null) return;
        FindTarget();
        MoveToTarget();
    }

    public void SetTarget(Health target)
    {
        this.target = target;
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

}
