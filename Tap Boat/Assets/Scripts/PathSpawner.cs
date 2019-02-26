using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour {

    private const float MAX_PATH_SIZE = 15f;
    private const float MIN_PATH_SIZE = 10f;
    private const string LEFTPATHTAG = "LEFTPATH";
    private const string RIGHTPATHTAG = "RIGHTPATH";

    public GameObject path;
    public GameObject firstPath;
    public GameObject spike;

    private GameManager gm;
    private List<GameObject> pathList ;
    private List<GameObject> spikeList;
    private float sign;

    private void Start()
    {
        gm = GameManager.instance;
        Restart();
    }


    public void SpawnPath(bool spawnSpikes)
    {
        if (pathList.Count > 20)
        {
            RemoveLastPathAndDecreaseSize();
        }
        GameObject path1 = Instantiate(path) as GameObject;
        path1.transform.SetParent(transform);
        Vector3 newPosition;
        Transform lastPath = pathList[0].transform;
        float size = GetPathSize();
        if (gm.spawnLeft)
        {
            sign = GetSign();
            Vector3 scale = new Vector3(size, 1f, 1f);
            path1.transform.localScale = scale;
            newPosition = new Vector3(sign * (size / 2 - 0.5f) + lastPath.position.x, 0f, (lastPath.localScale.z/2 - 0.5f) + lastPath.position.z);
            if (sign == 1)
            {
                path1.tag = RIGHTPATHTAG;
            } else
            {
                path1.tag = LEFTPATHTAG;
            }
        } else
        {
            Vector3 scale = new Vector3(1f, 1f, size);
            newPosition = new Vector3(sign * (lastPath.localScale.x / 2 - 0.5f)  + lastPath.position.x, 0f, (size / 2 - 0.5f) + lastPath.position.z);
            path1.transform.localScale = scale;
            
        }

        path1.transform.position = newPosition;
        pathList.Insert(0, path1);
        if (size >= 12 && spawnSpikes)
        {
            SpawnSpike(path1.transform);
        }
        gm.spawnLeft = !gm.spawnLeft;

    }

    private float GetSign()
    {

        if (Random.Range(0f, 2f) > 1)
        {
            return -1f;
        } else
        {
            return 1f;
        }
    }

    private float GetPathSize()
    {
        return Random.Range(MIN_PATH_SIZE, MAX_PATH_SIZE);
    }

    private void RemoveLastPathAndDecreaseSize()
    {
        GameObject pathToDestroy = pathList[pathList.Count - 2];
        if (pathToDestroy.transform.childCount > 0)
        {
            Destroy(pathToDestroy.transform.GetChild(0).gameObject);
        }
        Destroy(pathToDestroy);
        pathList.Remove(pathToDestroy);


    }

    public void Restart()
    {

        RemovePathAndSpikeList();
        pathList = new List<GameObject>();
        spikeList = new List<GameObject>();

        pathList.Insert(0, firstPath);
        for (int i = 0; i < 3; i++)
        {
            SpawnPath(false);
        }
        for (int i = 0; i < 7; i++)
        {
            SpawnPath(true);
        }
    }

    private void RemovePathAndSpikeList()
    {
        if (pathList == null) return;

        for (int i = 0; i < pathList.Count-1; i++) 
        {
            Destroy(pathList[i]);
        }

        for (int i = 0; i < spikeList.Count; i++)
        {
            Destroy(spikeList[i]);
        }
    }

    private void SpawnSpike(Transform path)
    {
        GameObject spikeGO =  Instantiate(spike) as GameObject;
        spikeGO.transform.SetParent(path);
        spikeList.Add(spikeGO);
        if (gm.spawnLeft)
        {
            spikeGO.transform.position = new Vector3(path.position.x + GetPositionSpike(path.localScale.x), path.position.y + 0.6f, path.position.z);
        }
        else
        {
            spikeGO.transform.position = new Vector3(path.position.x, path.position.y + 0.6f, path.position.z - GetPositionSpike(path.localScale.z));
        }

    }

    private float GetPositionSpike(float size)
    {
        return 0;
    }

    public int GetPathPosition(GameObject currentPath)
    {
        foreach(GameObject path in pathList)
        {


            if (path.Equals(currentPath))
            {
                
                if (path.transform.localScale.z > 1f)
                {
                    return 0;
                }
                if (path.CompareTag(LEFTPATHTAG))
                {
                    return 1;
                }
                if (path.CompareTag(RIGHTPATHTAG))
                {
                    return -1;
                }
            }
        }
        return 0;
    }
}
