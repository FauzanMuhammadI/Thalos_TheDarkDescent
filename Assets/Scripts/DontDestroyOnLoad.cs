using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    [SerializeField] private GameObject persistentObject;

    private void Awake()
    {
        if (persistentObject == null)
        {
            persistentObject = gameObject;
        }

        DontDestroyOnLoad(persistentObject);

        if (FindObjectsOfType<DontDestroyOnLoad>().Length > 1)
        {
            Destroy(persistentObject); 
        }
    }
}
