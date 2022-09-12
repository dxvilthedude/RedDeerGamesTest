using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMovement : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float movementSpeed;
    [SerializeField] private FixedJoystick movementJoystick;
    [SerializeField] private GameObject player;

    void Update()
    {
        if (gameManager.GameOn)
        {
            MovementInput();
        }
    }

    private void MovementInput()
    {
        float horizontal = movementJoystick.Horizontal;
        float vertical = movementJoystick.Vertical;

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        player.transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }
}
