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
        player.transform.rotation = Quaternion.Euler(v);
    }

}
