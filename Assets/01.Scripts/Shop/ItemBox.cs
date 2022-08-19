using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private ItemChangeManager ItemChangeManager
	{
        get
		{
            _itemChangeManager ??= FindObjectOfType<ItemChangeManager>();
            return _itemChangeManager;
		}
	}

    [SerializeField] private int _itemCode;
    [SerializeField] private ItemChangeManager _itemChangeManager;
    [SerializeField] private ItemDataSO _itemDataSO;


    private ItemData _itemData;
    void Start()
    {
        _itemData = _itemDataSO.GetItemData(_itemCode);
    }

    [ContextMenu("DebugChangeItemData")]
    public void DebugChangeItemData()
    {
        _itemData = _itemDataSO.GetItemData(_itemCode);
    }

	[ContextMenu("ChangeFirework")]
    public void ChangeFirework()
	{
        ItemChangeManager.ChangeFirework(_itemData);
    }

}
