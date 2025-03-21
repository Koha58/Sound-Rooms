using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public GameObject selectionImage; // 画像オブジェクト
    public Color normalColor = Color.white; // 通常色
    public Color selectedColor = Color.yellow; // 選択時の色（ボタンが選ばれているときの色）

    private Image buttonImage;
    private Image selectionImageComponent;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = normalColor; // ボタンの初期色設定
        }

        // 画像オブジェクトの設定
        if (selectionImage != null)
        {
            selectionImage.SetActive(false); // 最初は非表示
            selectionImageComponent = selectionImage.GetComponent<Image>();
            if (selectionImageComponent != null)
            {
                selectionImageComponent.color = selectedColor; // 選択時の色を設定
            }
        }
    }

    // ボタンが選択されたとき
    public void OnSelect(BaseEventData eventData)
    {
        if (selectionImage != null)
        {
            selectionImage.SetActive(true); // 画像を表示
        }

        if (buttonImage != null)
        {
            buttonImage.color = selectedColor; // 選択時にボタン色を変更
        }
    }

    // ボタンの選択が解除されたとき
    public void OnDeselect(BaseEventData eventData)
    {
        if (selectionImage != null)
        {
            selectionImage.SetActive(false); // 画像を非表示
        }

        if (buttonImage != null)
        {
            buttonImage.color = normalColor; // 通常色に戻す
        }
    }
}
