using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInventoryHolder : InventoryHolder
{

   

    public static UnityAction OnPlayerInventoryChanged;

    private void Start()
    {
        SaveGameManager.data.playerInventory = new InventorySaveData(primaryInventorySystem);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) OnDynamicInventoryDisplayRequested?.Invoke(primaryInventorySystem ,offset);
    }
        public bool AddToInventory(InventoryItemData data,int amount)
    {
        if(primaryInventorySystem.AddToInventory(data,amount))
        {
            return true;
        }
        return false;
    }

    protected override void LoadInventory(SaveData data)
    {
        if (data.playerInventory.InvSystem != null)
        {
            this.primaryInventorySystem = data.playerInventory.InvSystem;
            OnPlayerInventoryChanged?.Invoke();

        }
    }
}
