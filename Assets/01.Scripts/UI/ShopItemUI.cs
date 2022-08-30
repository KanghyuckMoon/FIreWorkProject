using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements; 

public class ShopItemUI : ItemUI
{

    protected VisualElement _purchasedImage;
    protected Label _purchasedLabel;

    public ShopItemUI(ItemData itemData, Action buyCheckEvent = null, Action librartUpdateEvent = null) 
        : base(itemData)
    {

        _purchasedImage = new VisualElement();
        _purchasedLabel = new Label();
        
        _purchasedLabel.text = "���� �Ϸ�";


        _button.clicked += buyCheckEvent;
        _button.clicked += librartUpdateEvent;
        _button.clicked += PurchasedItem;

        PurchasedItem();

        _purchasedImage.AddToClassList("purchased-image");
        _purchasedLabel.AddToClassList("purchased-label");

        _purchasedImage.style.display = DisplayStyle.None;

        this.Add(_purchasedImage);
        _purchasedImage.Add(_purchasedLabel);
    }

    /// <summary>
    /// ������ ���Ž� UI ���� 
    /// </summary>
    private void PurchasedItem()
    {
        // ������ �ִ� �������̸� ���� �Ϸ� ǥ��
        if (UserSaveDataManager.Instance.UserSaveData.haveItem.Contains(_itemCode))
        {
            //_button.style.display = DisplayStyle.None;
            _purchasedImage.style.display = DisplayStyle.Flex;
            return;
        }
        _button.style.display = DisplayStyle.Flex;
        _purchasedImage.style.display = DisplayStyle.None;
    }
}