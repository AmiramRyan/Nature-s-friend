using UnityEngine;
using UnityEngine.UI;

public class Request : MonoBehaviour
{
    public Image sprite;
    public Text Amount;

    public void SetUpRequest(Sprite sp,int am)
    {
        sprite.sprite = sp;
        Amount.text = "X"+am;
    }
}
