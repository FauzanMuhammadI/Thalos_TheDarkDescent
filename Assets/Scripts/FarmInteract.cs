using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private Plant plant; 

    public string InteractionPrompt => _prompt;

    bool IInteractable.Interact(Interactor interactor)
    {
        var inventory = interactor.GetComponent<Inventorytest>();
        if (inventory == null) return false;

        if (inventory.HasSeed)
        {
            Debug.Log("Farming");

            if (plant != null)
            {
                plant.StartGrowth(); 
                
            }
            return true;
        }
        else
        {
            Debug.Log("No Seed");
            return false;
        }
    }
}
