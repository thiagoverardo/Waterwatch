using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }
    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public Vector3 PickPosition;
    public Vector3 PickRotation;

    public virtual void OnDrop()
    {
        // Caso queira dropar o item aonde o ponteiro do mouse está
        // RaycastHit hit = new RaycastHit();
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // if(Physics.Raycast(ray, out hit, 2000))
        // {
        //     gameObject.SetActive(true);
        //     gameObject.transform.position = hit.point;
        // }
    }

    public virtual void OnUse()
    {
        
    }
}
