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
    /// <param name="isPurchasable">���� �����Ѱ�(���� �������ΰ� ���̺귯�� �������ΰ�)</param>
    /// <param name="buyCheckclickEvent">���� üũ �̺�Ʈ</param>
    public ItemUI(ItemData itemData,bool isPurchasable,Action buyCheckEvent = null, Action librartUpdateEvent =null)
    {
        _itemData = itemData;

        _button = new Button();
        _itemImage = new VisualElement();
        _itemCost = new Label();

        _purchasedImage = new VisualElement();
        _purchasedLabel = new Label();

        _itemCost.text = itemData.money.ToString();
        _purchasedLabel.text = "���� �Ϸ�";
        this.style.backgroundImage = new StyleBackground(itemData.texture2D);
        
        // ��ư �̺�Ʈ ��� 
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

        CheckPurchasable(isPurchasable); // ���� ������ ���������� üũ(���̺귯�� �������̸� ���� �Ұ�) 

        PurchasedItem();
    }

    /// <summary>
    /// ������ ���Ž� UI ���� 
    /// </summary>
    private void PurchasedItem()
    {
        // ������ �ִ� �������̸� ���� �Ϸ� ǥ��
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
    /// ���� ������ ���������� üũ
    /// </summary>
    private void CheckPurchasable(bool isPurchasable)
    {
        if (isPurchasable == false) // ��ư Ŭ�� �� �ǵ��� 
        {
            VisualElement hideElement = new VisualElement();
            hideElement.AddToClassList("hide-element");
            this.Add(hideElement);
            return; 
        }
        // ���� �����ϸ� ���ſϷ� ���
        _purchasedImage.AddToClassList("purchased-image");
        _purchasedLabel.AddToClassList("purchased-label");
        this.Add(_purchasedImage);
        _purchasedImage.Add(_purchasedLabel);
    }
}
