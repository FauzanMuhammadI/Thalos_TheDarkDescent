using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] private GameObject state1;
    [SerializeField] private GameObject state2;
    [SerializeField] private GameObject state3;
    [SerializeField] private GameObject state4;
    [SerializeField] private GameObject state5;
    [SerializeField] private GameObject state6;
    [SerializeField] private GameObject state7;

    private int plantAge = 0;
    [SerializeField] private int ageState1 = 1;
    [SerializeField] private int ageState2 = 2;
    [SerializeField] private int ageState3 = 4;
    [SerializeField] private int ageState4 = 6;
    [SerializeField] private int ageState5 = 8;
    [SerializeField] private int ageState6 = 10;
    [SerializeField] private int ageState7 = 11;

    [SerializeField] private Image imageToToggle;

    private bool isGrowing = false;

    private void Start()
    {
        DeactivateAllStates();
    }

    public void StartGrowth()
    {
        if (!isGrowing)
        {
            state1.SetActive(true);
            isGrowing = true;
            StartCoroutine(GrowPlant());
            if (imageToToggle != null)
            {
                imageToToggle.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator GrowPlant()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            plantAge++;
            UpdatePlantModel();
        }
    }

    private void UpdatePlantModel()
    {
        DeactivateAllStates();

        if (plantAge >= ageState7)
        {
            state7.SetActive(true);
        }
        else if (plantAge >= ageState6)
        {
            state6.SetActive(true);
        }
        else if (plantAge >= ageState5)
        {
            state5.SetActive(true);
        }
        else if (plantAge >= ageState4)
        {
            state4.SetActive(true);
        }
        else if (plantAge >= ageState3)
        {
            state3.SetActive(true);
        }
        else if (plantAge >= ageState2)
        {
            state2.SetActive(true);
        }
        else if (plantAge >= ageState1)
        {
            state1.SetActive(true);
        }
    }

    private void DeactivateAllStates()
    {
        state1.SetActive(false);
        state2.SetActive(false);
        state3.SetActive(false);
        state4.SetActive(false);
        state5.SetActive(false);
        state6.SetActive(false);
    }
}
