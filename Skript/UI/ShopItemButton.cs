using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ShopItemButton : MonoBehaviour
{
    public GameObject priceRoot, lockedImage, selectedImage;
    public TMP_Text priceText;
    public int price, skinIndex;

    void Start()
    {
        // ������� ���� (skinIndex == 0): ������ ������ � ������ �� ���������
        if (skinIndex == 0)
        {
            PlayerPrefs.SetInt("skin_0", 1); // ����������, ��������� ������
            SetSkinLocked(false);

            // ���� �� ������ ������ ���� � ������� ��������� �� ���������
            if (!PlayerPrefs.HasKey("current_skin") || PlayerPrefs.GetInt("current_skin", 0) == 0)
            {
                SetSkinSelected(true);
            }
            return;
        }

        // ��� ��������� ������ � ����������� ������
        if (PlayerPrefs.GetInt("skin_" + skinIndex, 0) == 0)
        {
            SetSkinLocked(true);
        }
        else
        {
            SetSkinLocked(false);
            if (PlayerPrefs.GetInt("current_skin", 0) == skinIndex)
                SetSkinSelected(true);
        }
    }

    public void OnClick()
    {
        if (PlayerPrefs.GetInt("skin_" + skinIndex, 0) == 1)
        {
            SetSkinSelected(true);
        }
        else
        {
            if (Count_coin.Instance._numderOfCoins >= price)
            {
                UIManager.Instance.confirmPurchaseWindow.SetActive(true);
                UIManager.Instance.confirmPurchaseWindow.GetComponent<ConfirmWindow>().onConfirm = () =>
                {
                    Count_coin.Instance.SpendMoney(price);
                    Count_coin.Instance.SaveToProgress(); // <--- ��������� ������!
                    SetSkinLocked(false);
                    SetSkinSelected(true);
                };
            }
            else
            {
                UIManager.Instance.noCoinsWindow.SetActive(true);
            }
        }
    }

    private void SetSkinLocked(bool locked)
    {
        priceRoot.SetActive(locked);
        lockedImage.SetActive(locked);
        if (!locked)
            PlayerPrefs.SetInt("skin_" + skinIndex, 1);
    }

    private void SetSkinSelected(bool selected)
    {
        foreach (var shopButton in FindObjectsOfType<ShopItemButton>())
        {
            shopButton.selectedImage.SetActive(false);
        }
        selectedImage.SetActive(selected);
        PlayerPrefs.SetInt("current_skin", skinIndex);
        SkinManager.Instance.ChangeSkin(skinIndex);
    }
}
