using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfItem : MonoBehaviour
{
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
    }

    public void UesItem()
    {
        resourceData.DecreaseAmount();
    }
}
