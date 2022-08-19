using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonManager : MonoBehaviour
{
	[SerializeField] private Button _ratebutton;
	[SerializeField] private Button _countbutton;
	[SerializeField] private Button _furtherbutton1;
	[SerializeField] private Button _furtherbutton2;
	[SerializeField] private Button _furtherbutton3;
	[SerializeField] private Button _furtherbutton4;

	private FireWorkController _fireWorkController;

	private void Start()
	{
		_fireWorkController = FindObjectOfType<FireWorkController>();
		_ratebutton.onClick.AddListener(() => _fireWorkController.UpdateRate(0.5f));
		_countbutton.onClick.AddListener(() => _fireWorkController.UpdateCount(1));
		_furtherbutton1.onClick.AddListener(() => _fireWorkController.UpdateFurtherCount1(1));
		_furtherbutton2.onClick.AddListener(() => _fireWorkController.UpdateFurtherCount2(1));
		_furtherbutton3.onClick.AddListener(() => _fireWorkController.UpdateFurtherCount3(1));
		_furtherbutton4.onClick.AddListener(() => _fireWorkController.UpdateFurtherCount4(1));
	}
}
