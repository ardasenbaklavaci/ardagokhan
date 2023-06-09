using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Item DataBase")]
public class ItemDatabase : ScriptableObject//items will not change and save datat can load and save them much easier
{
    [SerializeField] private List<InventoryItemData> _itemDataBase;

    [ContextMenu("Set IDs")]
    public void setItemIDs()
    {
    _itemDataBase= new List<InventoryItemData>();//=> is equivalence of terms like these delegate (Person p) { return p.Name; };


        var foundItems = UnityEngine.Resources.LoadAll<InventoryItemData>("ItemData").OrderBy(i => i.ID).ToList();//for each item 

        var hasIDRange = foundItems.Where(i=>i.ID != -1 && i.ID< foundItems.Count).OrderBy(i=>i.ID).ToList(); //to validate the items
        var hasIDNotInRange=foundItems.Where(i => i.ID != -1 && i.ID >= foundItems.Count).OrderBy(i => i.ID).ToList(); //and default them
        var noID = foundItems.Where(i => i.ID <= -1).ToList(); //will have id if no 

        var index = 0;
        for (int i = 0; i < foundItems.Count; i++)
        {
            InventoryItemData itemToAdd;
            itemToAdd = hasIDRange.Find(d => d.ID == i);

            if (itemToAdd != null)
            {
                _itemDataBase.Add(itemToAdd);

            }
            else if (index < noID.Count)
            {
                noID[index].ID = i;
                itemToAdd = noID[index];
                index++;
                _itemDataBase.Add(itemToAdd);
            }
        }
            
            foreach(var item in hasIDNotInRange)//item testing to check how many items of the same kind or not we have
            {
                _itemDataBase.Add(item);
            }                       
        }
    public InventoryItemData GetItem(int id)
    {
        return _itemDataBase.Find(i=> i.ID == id);
    }
}
