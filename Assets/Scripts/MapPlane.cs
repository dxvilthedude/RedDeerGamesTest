using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapPlane : MonoBehaviour
{
    [SerializeField] private MapManager mapManager;

    public void SetNewTile(MapManager manager)
    {
        mapManager = manager;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            mapManager.MapChange(gameObject);
    }
}
