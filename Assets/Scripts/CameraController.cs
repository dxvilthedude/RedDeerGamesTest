using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 targetOffset;
    [SerializeField] private float speed;

    void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        transform.LookAt(player);
        transform.position = Vector3.Lerp(transform.position, player.position + targetOffset, speed * Time.deltaTime);
    }
}
