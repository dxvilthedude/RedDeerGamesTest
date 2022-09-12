using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : Gun
{
    [SerializeField] private GameObject effectToSpawn;
    [SerializeField] private float speed;
    [SerializeField] private float damage = 25;
    public override void SpawnVFX(bool autoTargeting)
    {
        if (firePoint != null)
        {
            if (autoTargeting) AutoTarget();           

            if (BulletsPool.transform.childCount > 0)
            {
                var bullet = BulletsPool.transform.GetChild(0);
                bullet.transform.parent = BulletsUsed.transform;
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.rotation = firePoint.transform.rotation;

                bullet.GetComponent<Bullet>().SetUpBullet(damage);
                bullet.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * speed;
                bullet.gameObject.SetActive(true);

                if (bullet.transform.parent == BulletsUsed)
                    StartCoroutine(BackToPool(bullet.gameObject));
            }
        }
    }
    
}
