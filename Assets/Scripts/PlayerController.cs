using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private WeaponManager gun;
    [SerializeField] private GameManager gameManager;
    void Update()
    {
        if (gameManager.GameOn)
        {
            MovementInput();
            RotationInput();
        }
    }

    private void MovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement * movementSpeed * Time.deltaTime,Space.World);
    }
    private void RotationInput()
    {
        if (!MainMenu.autoTargeting)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }
}
