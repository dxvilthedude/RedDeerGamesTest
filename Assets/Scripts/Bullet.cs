using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    [SerializeField] private Gun currentGun;
    [SerializeField] private Collider co;

    private void Start()
    {
        currentGun = FindObjectOfType<Gun>();
    }
    public void SetUpBullet(float dmg)
    {
        damage = dmg;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

        currentGun.BulletBackToPool(gameObject);
        //gameObject.SetActive(false);
       
    }

}
