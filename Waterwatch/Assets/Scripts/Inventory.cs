using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int slots = 6;

    private List<IInventoryItem> mItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < slots)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if(collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();

                if(ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
