using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShelfItem : MonoBehaviour
{
    public void InitiateItem(Sprite mySprite)
    {
        GetComponent<Image>().sprite = mySprite;
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}
