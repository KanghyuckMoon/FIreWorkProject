using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSaveData
{
	//�̸�
	public string name;

	//��ȭ
	public int money;
	public int happy;

	//������
	public int renewal;

	//������
	public List<int> haveItem = new List<int> {0,1,2,3,4,100,101,102,103,104};

	//����
	public List<int> haveAchievement;

	//�ɼ�
	public float bgmVoulume = 0f;
	public float effVoulume = 0f;
	public int grapicQulityIndex = 0;
	public bool isFullScreen = true;
	public int width = 1920;
	public int height = 1080;

	//�ƾ��� ���Ҵ��� ����
	public List<bool> isViewCutScene = new List<bool> { false, false, false, false, false, false, false, false, false, false };

	//�Ҳɳ��� ����
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
