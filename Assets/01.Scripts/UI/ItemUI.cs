using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Scripting;
using System;
public class ItemUI : VisualElement
{
    protected DescriptionManager DescriptionManager
    {
        get
        {
            _descriptionManager ??= GameObject.FindObjectOfType<DescriptionManager>();
            return _descriptionManager;
        }
    }

    protected ItemData _itemData;
    protected int _itemCode; 

    protected Button _button;
    protected VisualElement _itemImage;
    protected Label _itemCost;

    protected DescriptionManager _descriptionManager;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemData"></param>
    /// <param name="isPurchasable">���� �����Ѱ�(���� �������ΰ� ���̺귯�� �������ΰ�)</param>
    /// <param name="buyCheckclickEvent">���� üũ �̺�Ʈ</param>
    public ItemUI(ItemData itemData)
    {
        _itemData = itemData;
        _itemCode = itemData.itemCode; 

        _button = new Button();
        _itemImage = new VisualElement();


        SetColor(itemData); 
        _button.style.display = DisplayStyle.Flex;

        // ��ư �̺�Ʈ ��� 
        //_button.clicked += buyCheckEvent;
        //_button.clicked += librartUpdateEvent; 
        //_button.clicked += PurchasedItem;

        // _button.clicked +=

        this.AddToClassList("shopItem");
        _button.AddToClassList("item-button");
        _itemImage.AddToClassList("item-image");

        this.Add(_button);
        _button.Add(_itemImage);

        DescriptionManager.SetDescriptionClickEvent(_button, _itemCode);

        //        CheckPurchasable(isPurchasable); // ���� ������ ���������� üũ(���̺귯�� �������̸� ���� �Ұ�) 

        //PurchasedItem();


    }

    private void SetColor(ItemData itemData)
    {
        if (itemData.itemType == EItem.Color) // �� �������̸� 
        {
            _itemImage.style.unityBackgroundImageTintColor = new StyleColor(itemData.gradient_1.colorKeys[1].color);
            return;
        }
        // ��� �������̸� 
        _itemImage.style.backgroundImage = new StyleBackground(itemData.texture2D);
        _itemImage.style.unityBackgroundImageTintColor = new StyleColor(Color.white);

    }

    /*
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
    */
}
