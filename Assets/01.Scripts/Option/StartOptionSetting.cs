using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOptionSetting : MonoBehaviour
{
	private void Start()
	{
		SoundSetting();
		GrapicSetting();
	}

	private void SoundSetting()
	{
		SoundSetting soundSetting = FindObjectOfType<SoundSetting>();
		soundSetting.ApplySettingSound(UserSaveDataManager.Instance.UserSaveData.bgmVoulume, UserSaveDataManager.Instance.UserSaveData.effVoulume);
	}

	private void GrapicSetting()
	{
		GrapicSetting grapicetting = FindObjectOfType<GrapicSetting>();
		grapicetting.IsFoolScreen = UserSaveDataManager.Instance.UserSaveData.isFullScreen;
		grapicetting.Width = UserSaveDataManager.Instance.UserSaveData.width;
		grapicetting.Height = UserSaveDataManager.Instance.UserSaveData.height;
		grapicetting.ChangeGrapicSetting(UserSaveDataManager.Instance.UserSaveData.grapicQulityIndex);
		grapicetting.ApplySettingScreen();
	}
}
