using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq; 

public class ShopPanelComponent : UIComponent
{
    // 외부 참조 변수 
    private HaveItemManager _haveItemManager;

    private Button _shopButton; // 상점 버튼
    private TemplateContainer _shopPanel;

    private Button _shopBackButton; // 뒤로가기 버튼 

    private List<ItemUI> _shopItemList = new List<ItemUI>();

    [SerializeField]
    private ItemDataSO _itemDataSO;

    public void Init(UIButtonManager uIButtonManager, HaveItemManager haveItemManager)
    {
        _uiButtonManager = uIButtonManager;
        _haveItemManager = haveItemManager;

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
    /// 상점 아이템 생성 
    /// </summary>
    private void CreateShopItem()
    {
        ArrayList l = _haveItemManager.ColorItemCodeList;
        _haveItemManager.ShapeItemCodeList; 
    }
}
