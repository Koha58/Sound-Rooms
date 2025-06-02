using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// シーン読み込み後に自動でボタンを一時的に無効にする例
public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private Button myButton;

    private void Start()
    {
        StartCoroutine(PreventAccidentalClick());
    }

    private IEnumerator PreventAccidentalClick()
    {
        myButton.interactable = false;
        yield return new WaitForSeconds(0.2f); // 200msだけ無効化
        myButton.interactable = true;
    }
}
