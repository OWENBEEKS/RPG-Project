using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] Transform target = null;

    void Update()
    {
        if(target == null) return;
        FindTarget();
        MoveToTarget();
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
            return target.position;
        }
        return target.position + Vector3.up * targetCapsule.height / 2;
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

}
