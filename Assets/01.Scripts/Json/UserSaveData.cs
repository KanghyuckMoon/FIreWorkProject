using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSaveData
{
	//이름
	public string name;

	//재화
	public int money;
	public int happy;

	//리뉴얼
	public int renewal;

	//아이템
	public List<int> haveItem = new List<int> {0,1,2,3,4,100,101,102,103,104};

	//업적
	public List<int> haveAchievement;

	//옵션
	public float bgmVoulume = 0f;
	public float effVoulume = 0f;
	public int grapicQulityIndex = 0;
	public bool isFullScreen = true;
	public int width = 1920;
	public int height = 1080;

	//컷씬을 보았는지 여부
	public List<bool> isViewCutScene = new List<bool> { false, false, false, false, false, false, false, false, false, false };

	//불꽃놀이 저장
	public int further1ColorItemCode = 0;
	public int further2ColorItemCode = 999;
	public int further1ColorLight = 1;
	public int further2ColorLight = 1;
	public int further1TextureItemCode = 0;
	public int further2TextureItemCode = 999;
	public int further1Size = 1;
	public int further2Size = 1;
	public int furtherDirectionCode0 = 0;
	public int furtherDirectionCode1 = 0;
	public int furtherDirectionCode2 = 0;
}
