using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Gun currentWeapon;
    [SerializeField] private GameManager gameManager;

    public void CollectWeapon(Gun newWeapon)
    {
        currentWeapon = newWeapon;
    }
    private void UpdateWeaponUI(Gun newWeapon)
    { 
        //Collecting new weapon
        //visual updated
    }
    void Update()
    {
        if (gameManager.GameOn)
        {
            if (Input.GetMouseButton(0))
                Shoot();
        }
    }

    public void Shoot()
    {
        if (currentWeapon == null)
            return;
        if (currentWeapon.Ammo != 0 && Time.time >= currentWeapon.TimeToFire)
        {
            currentWeapon.Shoot();
        }
    }
}
