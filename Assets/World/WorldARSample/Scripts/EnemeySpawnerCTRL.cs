using System.Collections.Generic;
using HuaweiARUnitySDK;
using UnityEngine;

public class EnemeySpawnerCTRL : MonoBehaviour
{
    public GameObject skeletonPrefab;
    public GameObject dragonPrefab;

    private static ARAnchor anchorForSpawning;
    private readonly static int numOfSkeletons = 6;
    private static int skeletonsAlive = numOfSkeletons;

    public static void  decrementNumOfSkeletons() { skeletonsAlive--; }

    public void InitSpawner(ARAnchor anchor)
    {
        anchorForSpawning = anchor;

        Debug.LogError("SPAWNER 29");
        var anchorPos = anchorForSpawning.GetPose().position;
        SpawnDragon(anchorPos);
        //Spawn three skeletons
        SpawnSkeletons(anchorPos);
        Debug.LogError("SPAWNER 34");

    }

    void SpawnDragon(Vector3 anchorPos)
    {
        Debug.LogError("SPAWNER 23");
        //lift the dragon higher and spawn it further away.
        Vector3 dragonPos = new Vector3(anchorPos.x, anchorPos.y + 1, anchorPos.z -4);

        //Spawn one dragon.
        Instantiate(dragonPrefab, dragonPos, anchorForSpawning.GetPose().rotation);
        //var skeleton = Instantiate(skeletonPrefab, anchorPos, anchorForSpawning.GetPose().rotation);
    }

    private void SpawnSkeletons(Vector3 anchorPos)
    {
        //Spawn three skeletons.
        //for (int i = 0; i < numOfSkeletons; i++)
        //{
        //    Debug.LogError("SPAWNER 41");

        //    Vector3 skeletonPos = new Vector3(anchorPos.x + i , anchorPos.y, anchorPos.z +i );
        //    Instantiate(skeletonPrefab, skeletonPos, anchorForSpawning.GetPose().rotation);

        //}
        Instantiate(skeletonPrefab, new Vector3(anchorPos.x -3 , anchorPos.y, anchorPos.z -4), anchorForSpawning.GetPose().rotation);
        Instantiate(skeletonPrefab, new Vector3(anchorPos.x - 2, anchorPos.y, anchorPos.z - 3), anchorForSpawning.GetPose().rotation);
        Instantiate(skeletonPrefab, new Vector3(anchorPos.x - 1, anchorPos.y, anchorPos.z - 2), anchorForSpawning.GetPose().rotation);
        Instantiate(skeletonPrefab, new Vector3(anchorPos.x + 1, anchorPos.y, anchorPos.z - 2), anchorForSpawning.GetPose().rotation);
        Instantiate(skeletonPrefab, new Vector3(anchorPos.x +2, anchorPos.y, anchorPos.z - 3), anchorForSpawning.GetPose().rotation);
        Instantiate(skeletonPrefab, new Vector3(anchorPos.x +3, anchorPos.y, anchorPos.z - 4), anchorForSpawning.GetPose().rotation);
    }
    public void Restart()
    {
        var anchorPos = anchorForSpawning.GetPose().position;
        SpawnDragon(anchorPos);
        //Spawn three skeletons
        SpawnSkeletons(anchorPos);
        skeletonsAlive = numOfSkeletons;
        Debug.LogError("RESTARTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT");
    }

    // Update is called once per frame
    void Update()
    {
        if(skeletonsAlive == 0)
        {
            skeletonsAlive = numOfSkeletons;
            //All skeletons died, respone them.
            SpawnSkeletons(anchorForSpawning.GetPose().position);

        }

    }
}
