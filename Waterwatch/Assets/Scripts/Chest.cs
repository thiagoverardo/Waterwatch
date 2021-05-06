using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chest;
    public GameObject openedChest;
    public GameObject flare;

    // Start is called before the first frame update
    public void openChest()
    {
        chest.SetActive(false);
        openedChest.SetActive(true);
        flare.SetActive(true);
        Rigidbody rbItem = flare.GetComponent<Rigidbody>();
        rbItem.AddForce(transform.up * 10.0f, ForceMode.Impulse);
    }
}
