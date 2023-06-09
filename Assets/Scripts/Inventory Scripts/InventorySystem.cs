using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlots> inventorySlots;

    private int inventorySize;

    public List<InventorySlots> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;
    public UnityAction<InventorySlots> OnInventorySlotChanged;
    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlots>(size);

        for (int i = 0; i < size; i++)
        {
            InventorySlots.Add(new InventorySlots());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
       if (ContainsItem(itemToAdd, out List<InventorySlots> invSlot))//if item exist chechs this 
        {
            foreach (var slot in invSlot)
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
           
        
        }
        if (HasFreeSlot(out InventorySlots FreeSlot))//gets the first available slot in the inventory
        {
            if (FreeSlot.RoomLeftInStack(amountToAdd))
            {
                FreeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(FreeSlot);
                return true;
            }
        }
        return false;
    }
    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlots> invSlot)
    {
       invSlot =InventorySlots.Where(i=> i.ItemData== itemToAdd).ToList();
        return invSlot == null ? false : true;
    }
    public bool HasFreeSlot(out InventorySlots freeSlot) 
    {
    
        freeSlot= InventorySlots.FirstOrDefault(i=>i.ItemData== null);

        return freeSlot == null ? false : true; 

    }

}