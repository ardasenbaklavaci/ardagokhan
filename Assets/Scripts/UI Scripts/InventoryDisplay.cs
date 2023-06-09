using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseInventoryItem;  
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlots_UI , InventorySlots> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlots_UI, InventorySlots>SlotDictionary=> slotDictionary;

    protected virtual void Start()
    {

    }
    public abstract void AssignSlot(InventorySystem invToDisplay , int offset);
    protected virtual void UpdateSlot(InventorySlots updatedSlot)
    {
        foreach (var slot in SlotDictionary) 
        {
            if (slot.Value == updatedSlot)//slot value which is the inventory slot 
            {
                slot.Key.UpdateUISlot(updatedSlot);//slot key respect to UI value
            }
        }
    }
    public void SlotClicked(InventorySlots_UI clickedUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlots.ItemData == null)
        {
            if(isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlots halfStackSlot)) 
            {
            mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            
            }
            else
            {
                mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }            

        }
        if(clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlots.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlots);
            clickedUISlot.UpdateUISlot();
        
            mouseInventoryItem.ClearSlot();
            return;
        }
        if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlots.ItemData != null)
        {
            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlots.ItemData;

            if (isSameItem && clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlots.StackSize))

            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlots);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            }
            else if (isSameItem && !clickedUISlot.AssignedInventorySlot.RoomLeftInStack(mouseInventoryItem.AssignedInventorySlots.StackSize, out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickedUISlot); //stack full so swap line
                else
                {
                    int remainingOnMouse = mouseInventoryItem.AssignedInventorySlots.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlots(mouseInventoryItem.AssignedInventorySlots.ItemData, remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                    return;
                
                }

            }


            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
}

    private void SwapSlots(InventorySlots_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlots(mouseInventoryItem.AssignedInventorySlots.ItemData, mouseInventoryItem.AssignedInventorySlots.StackSize);
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot() ;
    
    }


}
