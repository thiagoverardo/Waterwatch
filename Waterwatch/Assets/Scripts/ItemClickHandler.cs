using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    public Inventory _Inventory;
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>();
        IInventoryItem item = dragHandler.Item;
        if(item != null)
        {
            _Inventory.UseItem(item);
            item.OnUse();
        }
    }
}
