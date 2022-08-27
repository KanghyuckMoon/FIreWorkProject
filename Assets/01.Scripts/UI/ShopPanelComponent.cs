using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class ShopPanelComponent : UIComponent
{
    private Button _shopButton; // ���� ��ư
    private TemplateContainer _shopPanel;

    private Button _shopBackButton; // �ڷΰ��� ��ư 


    private List<ItemUI> _shopItemList = new List<ItemUI>(); 
    public void Init(UIButtonManager uIButtonManager)
    {
        _uiButtonManager = uIButtonManager;

        _shopPanel = _uiButtonManager.RootElement.Q<TemplateContainer>("ShopTemplate");
        _shopButton = _uiButtonManager.RootElement.Q<Button>("shop-button");

        _shopBackButton = _shopPanel.Q<Button>("back-button");

        _shopButton.clicked += () => OpenClosePanel(_shopPanel); 
        _shopBackButton.clicked += () => OpenClosePanel(_shopPanel); 

    }
    public override void UpdateSometing()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// ���� ������ ���� 
    /// </summary>
    private void CreateShopItem()
    {

    }
}
