using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlots : ISerializationCallbackReceiver//to call back items
{
    [NonSerialized] private InventoryItemData itemData;//data referance
    [SerializeField] private int stackSize;//current stack size of a data
    [SerializeField] private int _itemID = -1;
    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlots(InventoryItemData source, int amount) {//constructor for inventory slot occupation
        
        itemData = source;
        _itemID = itemData.ID;
        stackSize = amount;
    }
     public InventorySlots()//constructor for empty slots
        {
        ClearSlot();
        }


    public void ClearSlot()//clearing a slot
    {
        
        itemData = null;
        _itemID = -1;
        stackSize = -1;

    }
    public void AssignItem(InventorySlots invSlot)//item assingning 
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize);//does the slot contain the same item if yes add
        else//overwriting the slot 
        {
            itemData = invSlot.ItemData;
            _itemID = itemData.ID;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        
        }
    }

    public void AddToStack(int amount)//adding to stack
    {
        stackSize += amount;
    }
    public bool RoomLeftInStack(int amountToAdd , out int amountRemaining)//to check if the stack can be added meaning that if the slot has enough room for the stacked item
    {
        amountRemaining = ItemData.MaxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);

    }

    public void UpdateInventorySlot(InventoryItemData data , int amount)//update slot
    {
        itemData = data;
        _itemID = itemData.ID;
        stackSize = amount;
    }
    public bool RoomLeftInStack(int amountToAdd)
    {
    
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStackSize) return true;
        else return false;

    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;

    }
    public bool SplitStack(out InventorySlots splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;

            return false;
        }
            int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);
        splitStack= new InventorySlots(itemData , halfStack);
        return true;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (_itemID == -1) return;

        var db = Resources.Load<ItemDatabase>("DataBase");//need singleton but not necessary causes to load everytime when game loads
        itemData = db.GetItem(_itemID);
    }
}

