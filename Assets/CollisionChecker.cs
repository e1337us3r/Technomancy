using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding");
    }
}
