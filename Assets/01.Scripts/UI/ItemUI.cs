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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="isPurchasable">구매 가능한가(상점 아이템인가 라이브러리 아이템인가)</param>
    /// <param name="buyCheckclickEvent">구매 체크 이벤트</param>
    public ItemUI(ItemData itemData,bool isPurchasable,Action buyCheckEvent = null, Action librartUpdateEvent =null)
    {
        _itemData = itemData;

        _button = new Button();
        _itemImage = new VisualElement();
        _itemCost = new Label();

        _purchasedImage = new VisualElement();
        _purchasedLabel = new Label();

        _itemCost.text = itemData.money.ToString();
        _purchasedLabel.text = "구매 완료";
        this.style.backgroundImage = new StyleBackground(itemData.texture2D);
        
        // 버튼 이벤트 등록 
        _button.clicked += buyCheckEvent;
        _button.clicked += librartUpdateEvent; 
        _button.clicked += PurchasedItem;

        this.AddToClassList("shopItem");
        _button.AddToClassList("item-button");
        _itemImage.AddToClassList("item-image");
        _itemCost.AddToClassList("item-label");

        this.Add(_button);
        _button.Add(_itemImage);
        _button.Add(_itemCost);

        CheckPurchasable(isPurchasable); // 구매 가능한 아이템인지 체크(라이브러리 아이템이면 구매 불가) 

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

    /// <summary>
    /// 구매 가능한 아이템인지 체크
    /// </summary>
    private void CheckPurchasable(bool isPurchasable)
    {
        if (isPurchasable == false) // 버튼 클릭 안 되도록 
        {
            VisualElement hideElement = new VisualElement();
            hideElement.AddToClassList("hide-element");
            this.Add(hideElement);
            return; 
        }
        // 구매 가능하면 구매완료 요소
        _purchasedImage.AddToClassList("purchased-image");
        _purchasedLabel.AddToClassList("purchased-label");
        this.Add(_purchasedImage);
        _purchasedImage.Add(_purchasedLabel);
    }
}
