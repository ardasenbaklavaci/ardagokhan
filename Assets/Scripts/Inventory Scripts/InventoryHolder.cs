using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]

public abstract class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;    
    [SerializeField] protected InventorySystem primaryInventorySystem;
    [SerializeField] protected int offset = 10;

    public int Offset => offset;
    public InventorySystem InventorySystem => primaryInventorySystem;
 
    public static UnityAction<InventorySystem , int> OnDynamicInventoryDisplayRequested;//InvSystem to Display to offset


    protected virtual  void Awake()
    {
        primaryInventorySystem = new InventorySystem(inventorySize);
    }

    protected abstract void LoadInventory(SaveData saveData);
    

    
}

[System.Serializable]
public struct InventorySaveData
{
    public InventorySystem InvSystem;
    public Vector3 position;
    public Quaternion rotation;

    public InventorySaveData(InventorySystem _invSystem, Vector3 _position, Quaternion _rotation)
    {

        InvSystem = _invSystem;
        position = _position;
        rotation = _rotation;

    }
    public InventorySaveData(InventorySystem _invSystem)
    {
        InvSystem = _invSystem;
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }
}