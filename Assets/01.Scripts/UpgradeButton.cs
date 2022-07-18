using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
	private Button _button;
	private FireWorkController _fireWorkController;

	private void Start()
	{
		_button = GetComponent<Button>();
		_fireWorkController = FindObjectOfType<FireWorkController>();
		_button.onClick.AddListener(() => _fireWorkController.UpdateRate(0.5f));
	}
}
