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
    /// <param name="isPurchasable">���� �����Ѱ�(���� �������ΰ� ���̺귯�� �������ΰ�)</param>
    /// <param name="buyCheckclickEvent">���� üũ �̺�Ʈ</param>
    public ItemUI(ItemData itemData)
    {
        _itemData = itemData;

        _button = new Button();
        _itemImage = new VisualElement();
        _itemCost = new Label();

        _itemCost.text = itemData.money.ToString();

        this.style.backgroundImage = new StyleBackground(itemData.texture2D);

        _button.style.display = DisplayStyle.Flex;

        // ��ư �̺�Ʈ ��� 
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

//        CheckPurchasable(isPurchasable); // ���� ������ ���������� üũ(���̺귯�� �������̸� ���� �Ұ�) 

        //PurchasedItem();
    }

    /// <summary>
    /// ���� ������ ���������� üũ
    /// </summary>
    private void CheckPurchasable(bool isPurchasable)
    {
        if (isPurchasable == false) // ��ư Ŭ�� �� �ǵ��� 
        {
         //   VisualElement hideElement = new VisualElement();
          //  hideElement.AddToClassList("hide-element");
           // this.Add(hideElement);
           

            return; 
        }
        // ���� �����ϸ� ���ſϷ� ���
        _purchasedImage.AddToClassList("purchased-image");
        _purchasedLabel.AddToClassList("purchased-label");
        this.Add(_purchasedImage);
        _purchasedImage.Add(_purchasedLabel);
    }
}
