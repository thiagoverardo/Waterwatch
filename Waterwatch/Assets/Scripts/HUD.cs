using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public Inventory inventory;
    public GameObject MessagePanel;
    public GameObject ScrollPanel;
    public TMP_Text txtDate;
    public TMP_Text txtMessage;
    public TMP_Text txtAlert;

    // Start is called before the first frame update
    void Start()
    {
        inventory.ItemAdded += InventoryScript_ItemAdded;
        inventory.ItemRemoved += Inventory_ItemRemoved;
    }
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                itemDragHandler.Item = e.Item;
                break;
            }
        }
    }
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach(Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
            if(itemDragHandler.Item != null  && e.Item != null){
                if(itemDragHandler.Item.Equals(e.Item))
                {
                    image.enabled = false;
                    image.sprite = null;
                    itemDragHandler.Item = null;
                    break;
                }
            }
        }
    }

    public void OpenMessagePanel(string text)
    {
        txtAlert.text = text;
        MessagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }

    public void OpenScrollPanel(string date, string text)
    {
        txtDate.text = date;
        txtMessage.text = text;
        ScrollPanel.SetActive(true);
    }

    public void CloseScrollPanel()
    {
        ScrollPanel.SetActive(false);
    }
}
