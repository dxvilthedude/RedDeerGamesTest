using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileRotation : MonoBehaviour
{
	[SerializeField] private Joystick joystick;
	[SerializeField] private GameObject player;

	void Update()
	{
        float angle = 0;
        if (joystick.Horizontal > 0)
        {
            angle = 90;
            angle -= 90 * joystick.Vertical;
        }
        else
        {
            angle = -90;
            angle += 90 * joystick.Vertical;
        }

        Vector3 v = new Vector3(0, angle, 0);

        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (direction.sqrMagnitude > 0.1f)
        {
            player.transform.rotation = Quaternion.Euler(v);
            player.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, angle, player.transform.eulerAngles.z);
        }
    }

}
