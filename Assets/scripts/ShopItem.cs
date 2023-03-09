using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public float price;
    public Button buyButton;

    private void Start()
    {
        if(PlayerPrefs.GetString("boughtItems").Contains(itemName))
        {
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "purchased";
        }
    }

    public void BuyItem()
    {
        if(PlayerPrefs.GetFloat("money") >= price)
        {
            PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") - price);
            if(!PlayerPrefs.GetString("boughtItems").Contains(itemName))
                PlayerPrefs.SetString("boughtItems", PlayerPrefs.GetString("boughtItems") + " " + itemName);
            buyButton.interactable = false;
            buyButton.transform.GetChild(0).GetComponent<Text>().text = "purchased";
        }
    }
}
