using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ShelfItem : MonoBehaviour
{
    public static Action highlightStatusChanged;
    public bool highlighted = false;
    public GenericInventoryResource resourceData;
    public void InitiateItem(GenericInventoryResource thisResource)
    {
        resourceData = thisResource;
        GetComponent<Image>().sprite = resourceData.itemSprite;
    }
    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        highlighted = !highlighted;
        if (highlighted)
        {
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
        highlightStatusChanged?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cauldronPanelCollider"))
        {
            //play splash effect
            //GameObject effect = Instantiate(resourceData.splashEffect,transform.position,Quaternion.identity) as GameObject;
            //effect.transform.SetParent(this.transform, false);
            //destroy the item
            DestroyItem();
        }
    }
}
