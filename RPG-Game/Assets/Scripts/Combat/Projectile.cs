using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] Health target = null;
    [SerializeField] float speed = 10;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float maxLifeTime =7;
    [SerializeField] GameObject[] destroyOnHit;
    float lifeAfterImpact = 0.2f;
    float damage = 0;
    [SerializeField] bool isHoming = true;

    private void Start()
    {
        if (target == null) return;
        transform.LookAt(GetAimLocation());
    }
    // Update is called once per frame
    void Update()
    {
        if(target == null) return;

        if (isHoming)
        {
            transform.LookAt(GetAimLocation());
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public void SetTarget(Health target,float mydamage)
    {
        this.target = target;
        this.damage = mydamage;
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target ) return;
        if(target.IsDead()) return;

        if(hitEffect != null)
        Instantiate(hitEffect, GetAimLocation(), Quaternion.identity);

        target.TakeDamage(damage);

        foreach(var toDestroy in destroyOnHit)
        {
            Destroy(toDestroy);
        }

        Destroy(gameObject,lifeAfterImpact);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null)
        {
            return target.transform.position;
        }
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }
}
