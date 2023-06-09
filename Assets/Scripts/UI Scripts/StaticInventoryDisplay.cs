using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{

    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlots_UI[] slots;

    private void OnEnable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged += RefreshStaticDisplay;

    }
    private void OnDisable()
    {
        PlayerInventoryHolder.OnPlayerInventoryChanged -= RefreshStaticDisplay;
    }
    private void RefreshStaticDisplay()
    {
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        AssignSlot(inventorySystem, 0);
    }
    protected override void Start()
    {
        base.Start();
     RefreshStaticDisplay();    
    }
    public override void AssignSlot(InventorySystem invToDisplay, int offset)
    {
        slotDictionary = new Dictionary<InventorySlots_UI, InventorySlots>();
 


        for(int i =0; i<inventoryHolder.Offset; i++) 
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}