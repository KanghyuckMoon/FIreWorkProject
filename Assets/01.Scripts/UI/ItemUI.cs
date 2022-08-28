using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Scripting;
using System;
public class ItemUI : VisualElement
{
    private ItemData _itemData;

    private Button _button;
    private VisualElement _itemImage;
    private Label _itemCost;
    private VisualElement _purchasedImage;
    private Label _purchasedLabel;

    public ItemUI(ItemData itemData, Action clickEvent)
    {
        _itemData = itemData;

        _button = new Button();
        _itemImage = new VisualElement();
        _itemCost = new Label();

        _purchasedImage = new VisualElement();
        _purchasedLabel = new Label();

        _itemCost.text = itemData.money.ToString();
        _purchasedLabel.text = "구매 완료";
        // this.style.backgroundImage = 
        _button.clicked += clickEvent;
        _button.clicked += PurchasedItem;

        this.AddToClassList("shopItem");
        _button.AddToClassList("item-button");
        _itemImage.AddToClassList("item-image");
        _itemCost.AddToClassList("item-label");

        _purchasedImage.AddToClassList("purchased-image");
        _purchasedLabel.AddToClassList("purchased-label");

        this.Add(_button);
        _button.Add(_itemImage);
        _button.Add(_itemCost);
        this.Add(_purchasedImage);
        _purchasedImage.Add(_purchasedLabel);

        PurchasedItem();
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
