using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Scripting;
using System;
public class ItemUI : VisualElement
{
    protected ItemData _itemData;

    protected Button _button;
    protected VisualElement _itemImage;
    protected Label _itemCost;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="isPurchasable">구매 가능한가(상점 아이템인가 라이브러리 아이템인가)</param>
    /// <param name="buyCheckclickEvent">구매 체크 이벤트</param>
    public ItemUI(ItemData itemData)
    {
        _itemData = itemData;

        _button = new Button();
        _itemImage = new VisualElement();
        _itemCost = new Label();

        _itemCost.text = itemData.money.ToString();

        this.style.backgroundImage = new StyleBackground(itemData.texture2D);

        _button.style.display = DisplayStyle.Flex;

        // 버튼 이벤트 등록 
        //_button.clicked += buyCheckEvent;
        //_button.clicked += librartUpdateEvent; 
        //_button.clicked += PurchasedItem;

        // _button.clicked +=

        this.AddToClassList("shopItem");
        _button.AddToClassList("item-button");
        _itemImage.AddToClassList("item-image");
        _itemCost.AddToClassList("item-label");

        this.Add(_button);
        _button.Add(_itemImage);
        _button.Add(_itemCost);

//        CheckPurchasable(isPurchasable); // 구매 가능한 아이템인지 체크(라이브러리 아이템이면 구매 불가) 

        //PurchasedItem();
    }

    /// <summary>
    /// 구매 가능한 아이템인지 체크
    /// </summary>
    private void CheckPurchasable(bool isPurchasable)
    {
        if (isPurchasable == false) // 버튼 클릭 안 되도록 
        {
         //   VisualElement hideElement = new VisualElement();
          //  hideElement.AddToClassList("hide-element");
           // this.Add(hideElement);
           

            return; 
        }
        // 구매 가능하면 구매완료 요소
        _purchasedImage.AddToClassList("purchased-image");
        _purchasedLabel.AddToClassList("purchased-label");
        this.Add(_purchasedImage);
        _purchasedImage.Add(_purchasedLabel);
    }
}
