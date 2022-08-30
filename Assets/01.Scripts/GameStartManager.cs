using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.InputSystem;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _warrningText;
    [SerializeField] private TextMeshProUGUI _warrningContentText;
    [SerializeField] private Image _controlUI1;
    [SerializeField] private Image _controlUI2;
    [SerializeField] private Image _controlUI3;
    [SerializeField] private TextMeshProUGUI _controlTextUI1;
    [SerializeField] private TextMeshProUGUI _controlTextUI2;
    [SerializeField] private TextMeshProUGUI _controlTextUI3;
    [SerializeField] private TextMeshProUGUI _gameStartText;
    private bool _isCanStart = false;

    // Start is called before the first frame update
    void Start()
    {
        Animation();
    }

	//private void Update()
	//{
	//	if(_isCanStart)
	//	{
 //           if(Input.GetMouseButtonDown(0))
	//		{
 //               SceneManager.LoadScene("InGame");
	//		}
	//	}
	//}

    public void OnUI(InputAction.CallbackContext context)
    {
        if (_isCanStart)
        {
            SceneManager.LoadScene("InGame");
        }
    }

	private void Animation()
	{
        _warrningText.gameObject.SetActive(false);
        _warrningContentText.gameObject.SetActive(false);
        _controlUI1.gameObject.SetActive(false);
        _controlUI2.gameObject.SetActive(false);
        _controlUI3.gameObject.SetActive(false);
        _controlTextUI1.gameObject.SetActive(false);
        _controlTextUI2.gameObject.SetActive(false);
        _controlTextUI3.gameObject.SetActive(false);
        _gameStartText.gameObject.SetActive(false);

        AnimationFade(_warrningText, 1f, 0.5f);
        AnimationFade(_warrningContentText, 1f, 1.5f);
        AnimationFade(_controlUI1, 1f, 3f);
        AnimationFade(_controlUI2, 1f, 3f);
        AnimationFade(_controlUI3, 1f, 3f);
        AnimationFade(_controlTextUI1, 1f, 3f);
        AnimationFade(_controlTextUI2, 1f, 3f);
        AnimationFade(_controlTextUI3, 1f, 3f);
        AnimationFade(_gameStartText, 1f, 5f);
        Invoke("SetOnIsCanStart", 5.2f);
    }

    private void SetOnIsCanStart()
	{
        _isCanStart = true;
	}

    private void AnimationFade(TextMeshProUGUI text, float duration, float delay)
	{
        Color color = text.color;
        color.a = 0;
        text.color = color;
        text.gameObject.SetActive(true);
        text.DOFade(1, duration).SetDelay(delay);
    }

    private void AnimationFade(Image image, float duration, float delay)
    {
        Color color = image.color;
        color.a = 0;
        image.color = color;
        image.gameObject.SetActive(true);
        image.DOFade(1, duration).SetDelay(delay);
    }
}
