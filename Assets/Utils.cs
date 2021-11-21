using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static void FindAndDestroyAllEnemies() 
    {
        var skeletons = GameObject.FindGameObjectsWithTag("SKELETON");
        var dragons = GameObject.FindGameObjectsWithTag("DRAGON");

        for (int i = 0; i < skeletons.Length; i++) 
        {
            Destroy(skeletons[i], 0f);
        }
        for (int i = 0; i < dragons.Length; i++)
        {
            Destroy(dragons[i], 0f);
        }
    }
}
