using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RelationsManager : GenericSingletonClass_Relation<MonoBehaviour>
{
    [Header("UIText%")]
    public TextMeshProUGUI fireText;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI earthText;

    [Header("UICircleImg")]
    public Image nautral;
    public Image conlfict1;
    public Image conlfict2;

    [Header("UICircleImg")]
    public Element neutralType;
    public Element conlfict1Type;
    public Element conlfict2Type;

    [Header("ElementsSprites")]
    private Sprite[] elementSpritesArr;
    public Sprite fireSp;
    public Sprite waterSp;
    public Sprite earthSp;


    [Header("ElementsTypes")]
    public Element[] elementTypeArr;

    public int currentFirePrecent;
    public int currentWaterPrecent;
    public int currentEarthPrecent;

    private void OnEnable()
    {
        currentFirePrecent = 50;
        currentWaterPrecent = 50;
        currentEarthPrecent = 50;
        fireText.text = currentFirePrecent + "%";
        waterText.text = currentWaterPrecent + "%";
        earthText.text = currentEarthPrecent + "%";
    }

    public void GenerateRelationForToday() //randomise the conflicts
    {
        int con1Index;
        int naturalIndex = Random.Range(0, elementTypeArr.Length - 1);
        Element[] whatsLeftArr = new Element[2];
        int whatsLeftIndex = 0;
        for (int i = 0; i < elementTypeArr.Length; i++)
        {
            if(i != naturalIndex) //dont need this
            {
                whatsLeftArr[whatsLeftIndex] = elementTypeArr[i];
                whatsLeftIndex++;
            }
        }
        int con2Index = Random.Range(0, 1);
        Debug.Log(con2Index);
        if(con2Index == 0)
        {
            con1Index = 1;
        }
        else
        {
            con1Index = 0;
        }

        //Set conflicts
        conlfict1Type = elementTypeArr[con1Index];
        conlfict2Type = whatsLeftArr[con2Index];
        neutralType = whatsLeftArr[naturalIndex];
        //Set UI view for conflicts
        SetSprites(conlfict1Type, conlfict1);
        SetSprites(conlfict2Type, conlfict2);
        SetSprites(neutralType, nautral);
    } 

    public void AdjustRelationShips(Element elementOfOrder, int increaceAmount, int decreaceAmount)
    {
        if(elementOfOrder == conlfict1Type)
        {
            AdujstElement(elementOfOrder, increaceAmount, true);
            AdujstElement(conlfict2Type, decreaceAmount, false);
        }
        else if(elementOfOrder == conlfict2Type)
        {
            AdujstElement(elementOfOrder, increaceAmount, true);
            AdujstElement(conlfict1Type, decreaceAmount, false);
        }
        else //natural
        {
            AdujstElement(elementOfOrder, increaceAmount, true);
        }
    } // adjust relationships based on element


    private void AdujstElement(Element elementOfOrder, int Amount, bool increace)
    {
        switch (elementOfOrder)
        {
            case Element.fire:
                if (increace)
                {
                    fireText.text = currentFirePrecent + Amount + "%";
                }
                else
                {
                    fireText.text = currentFirePrecent - Amount + "%";
                }
                break;
            case Element.water:
                if (increace)
                {
                    waterText.text = currentWaterPrecent + Amount + "%";
                }
                else
                {
                    waterText.text = currentWaterPrecent - Amount + "%";
                }
                break;
            case Element.earth:
                if (increace)
                {
                    earthText.text = currentEarthPrecent + Amount + "%";
                }
                else
                {
                    earthText.text = currentEarthPrecent - Amount + "%";
                }
                break;
            default:
                break;
        }
    }

    private void SetSprites(Element element, Image image)
    {
        switch (element)
        {
            case Element.fire:
                image.sprite = fireSp;
                break;
            case Element.water:
                image.sprite = waterSp;
                break;
            case Element.earth:
                image.sprite = earthSp;
                break;
            default:
                break;
        }
    }

}
