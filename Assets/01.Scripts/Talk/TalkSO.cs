using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalkSO", menuName = "ScriptableObject/TalkSO")]
public class TalkSO : ScriptableObject
{
	public List<string> content = new List<string>();
}
