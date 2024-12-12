using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorytest : MonoBehaviour
{
    public bool HasSeed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) HasSeed = !HasSeed;
        
    }
}
