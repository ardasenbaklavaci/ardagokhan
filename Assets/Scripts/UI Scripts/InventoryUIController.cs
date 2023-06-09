using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InventoryUIController : MonoBehaviour
{
    [FormerlySerializedAs("chestPanel")] public DynamicInventoryDisplay inventoryPanel; 
    public DynamicInventoryDisplay playerBackpackPanel;
    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;//subscribe to display
        
    }
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;//unsubscribe form display
   
    }

    void Update()
    {       
        
        if(inventoryPanel.gameObject.activeInHierarchy)
        {
           if( Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))//for backpack  to be closed use E or B now and all other inventory holders like chests
            inventoryPanel.gameObject.SetActive(false);
        
        }
           
            

        if (playerBackpackPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.B))//because of newly implemented dynamic structure this line does not work anymore
            playerBackpackPanel.gameObject.SetActive(false);

    }
    void DisplayInventory(InventorySystem invToDisplay, int offset)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay, offset);
    
    }
    

    
}
