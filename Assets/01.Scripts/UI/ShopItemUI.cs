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
        
        _purchasedLabel.text = "구매 완료";

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
    /// 아이템 구매시 UI 변경 
    /// </summary>
    private void PurchasedItem()
    {
        // 가지고 있는 아이템이면 구매 완료 표시
        if (UserSaveDataManager.Instance.UserSaveData.haveItem.Contains(_itemData.itemCode))
        {
            _button.style.display = DisplayStyle.None;
            _purchasedImage.style.display = DisplayStyle.Flex;
            return;
        }
        _button.style.display = DisplayStyle.Flex;
        _purchasedImage.style.display = DisplayStyle.None;
    }
}
