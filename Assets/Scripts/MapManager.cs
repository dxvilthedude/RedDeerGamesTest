using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private float planeSizeLength;
    [SerializeField] private List<GameObject> currentMapPlanes;
    [SerializeField] private GameObject mapPlanePrefab;
    [SerializeField] private GameObject currentMapPlane;
    void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        var currentPos = currentMapPlane.transform.position;

        Vector3 rightPlanePos = new Vector3(currentPos.x + planeSizeLength, currentPos.y, currentPos.z);
        GenerateMapTile(rightPlanePos);

        Vector3 leftPlanePos = new Vector3(currentPos.x - planeSizeLength, currentPos.y, currentPos.z);
        GenerateMapTile(leftPlanePos);

        Vector3 topPlanePos = new Vector3(currentPos.x, currentPos.y, currentPos.z + planeSizeLength);
        GenerateMapTile(topPlanePos);

        Vector3 bottomPlanePos = new Vector3(currentPos.x, currentPos.y, currentPos.z - planeSizeLength);
        GenerateMapTile(bottomPlanePos);

        Vector3 topRightPlanePos = new Vector3(currentPos.x + planeSizeLength, currentPos.y, currentPos.z + planeSizeLength);
        GenerateMapTile(topRightPlanePos);

        Vector3 topLeftPlanePos = new Vector3(currentPos.x - planeSizeLength, currentPos.y, currentPos.z + planeSizeLength);
        GenerateMapTile(topLeftPlanePos);

        Vector3 bottomRightPlanePos = new Vector3(currentPos.x + planeSizeLength, currentPos.y, currentPos.z - planeSizeLength);
        GenerateMapTile(bottomRightPlanePos);

        Vector3 bottomLeftPlanePos = new Vector3(currentPos.x - planeSizeLength, currentPos.y, currentPos.z - planeSizeLength);
        GenerateMapTile(bottomLeftPlanePos);

        
    }
    public void MapChange(GameObject newMapPlane)
    {
        currentMapPlane = newMapPlane;
        StartCoroutine(Generate());      
    }
    private bool MapPlaneExists(Vector3 newMapPlanePos)
    {
        foreach (var plane in currentMapPlanes)
        {
            if (plane.transform.position == newMapPlanePos)
                return true;
        }
        return false;
    }
    private void GenerateMapTile(Vector3 newTilePos)
    {
        if (!MapPlaneExists(newTilePos))
        {
            var newPlane = Instantiate(mapPlanePrefab, newTilePos, currentMapPlane.transform.rotation, transform);
            newPlane.GetComponent<MapPlane>().SetNewTile(this);
            currentMapPlanes.Add(newPlane);
        }
    }

    private bool CheckIfNear(GameObject mapPlane)
    {
        var planeX = mapPlane.transform.position.x;
        var planeY = mapPlane.transform.position.y;

        return !(Mathf.Abs(planeX - currentMapPlane.transform.position.x) > 50 || Mathf.Abs(planeY - currentMapPlane.transform.position.y) > 50);
    }

    private void RemoveFarPlanes()
    {
        currentMapPlanes = new List<GameObject>();
        foreach (Transform child in gameObject.transform)
        {
            if (CheckIfNear(child.gameObject)) currentMapPlanes.Add(child.gameObject);
        }
    }
    IEnumerator Generate()
    {
        GenerateMap();
        yield return new WaitForSeconds(1f);
       // RemoveFarPlanes();
    }
}
