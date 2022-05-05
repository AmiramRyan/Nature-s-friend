using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Request : MonoBehaviour
{
    public Image myImg;
    public TextMeshProUGUI Amount;

    private void OnEnable()
    {
        myImg = GetComponentInChildren<Image>();
        Amount = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetUpRequest(Sprite sp, string am)
    {
        myImg.sprite = sp;
        Amount.text = 'X' + am;
        transform.localScale = new Vector3(1, 1, transform.localScale.z);
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }
}
