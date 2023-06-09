using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{

    [SerializeField] protected InventorySlots_UI slotPrefab;
    protected override void Start()
    {
        base.Start();
        
    }
    
    
    
    public void RefreshDynamicInventory(InventorySystem invToDisplay, int offset)
    {
       ClearSlots();
        inventorySystem= invToDisplay;
       if(inventorySystem != null) inventorySystem.OnInventorySlotChanged += UpdateSlot;
        AssignSlot(invToDisplay, offset);
    }
    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        
        slotDictionary = new Dictionary<InventorySlots_UI, InventorySlots>();

        if (invToDisplay == null) return;


        for(int i =offset; i< invToDisplay.InventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateUISlot();
                
        }
    }
    private void ClearSlots()//item pooling eklenebilir buraya
    {
        foreach(var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
        if (slotDictionary!= null) slotDictionary.Clear();
    
    }
    private void OnDisable()
    {
        if (inventorySystem != null) inventorySystem.OnInventorySlotChanged -= UpdateSlot;
    }

  
}


