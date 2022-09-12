using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] private protected Transform BulletsPool;
    [SerializeField] private protected Transform BulletsUsed;

    [SerializeField] private protected int ammo;
    [SerializeField] private protected float fireRate;
    [SerializeField] private protected Transform firePoint;
    [SerializeField] private protected Transform player;
    [SerializeField] private protected float autoTargetRadious;
    public Transform target;
    private float timeToFire = 0f;
    public float TimeToFire => timeToFire;
    public int Ammo => ammo;

    public void Shoot() 
    {
        timeToFire = Time.time + 1f / fireRate;
        SpawnVFX(MainMenu.autoTargeting);
    }
    public abstract void SpawnVFX(bool autoTargeting);
    public void AutoTarget()
    {
        Debug.Log("Auto Targeting");
        float distance = 0;
        target = default;
        Collider[] hitColliders = Physics.OverlapSphere(player.position, autoTargetRadious);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy")
            {
                Debug.Log("enemy hit");
                float enemyDistance = Vector3.Distance(player.position, hitCollider.transform.position);
                if (enemyDistance > distance)
                {
                    distance = enemyDistance;
                    target = hitCollider.transform;
                }
            }
        }
        if (target != default)
            player.LookAt(target);
    }
    public void BulletBackToPool(GameObject bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.parent = BulletsPool;
    }
    public IEnumerator BackToPool(GameObject bullet)
    {
        yield return new WaitForSeconds(3f);
        BulletBackToPool(bullet);
    }
}
