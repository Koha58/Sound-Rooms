using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    public GameObject selectionImage; // �摜�I�u�W�F�N�g
    public Color normalColor = Color.white; // �ʏ�F
    public Color selectedColor = Color.yellow; // �I�����̐F�i�{�^�����I�΂�Ă���Ƃ��̐F�j

    private Image buttonImage;
    private Image selectionImageComponent;

    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.color = normalColor; // �{�^���̏����F�ݒ�
        }

        // �摜�I�u�W�F�N�g�̐ݒ�
        if (selectionImage != null)
        {
            selectionImage.SetActive(false); // �ŏ��͔�\��
            selectionImageComponent = selectionImage.GetComponent<Image>();
            if (selectionImageComponent != null)
            {
                selectionImageComponent.color = selectedColor; // �I�����̐F��ݒ�
            }
        }
    }

    // �{�^�����I�����ꂽ�Ƃ�
    public void OnSelect(BaseEventData eventData)
    {
        if (selectionImage != null)
        {
            selectionImage.SetActive(true); // �摜��\��
        }

        if (buttonImage != null)
        {
            buttonImage.color = selectedColor; // �I�����Ƀ{�^���F��ύX
        }
    }

    // �{�^���̑I�����������ꂽ�Ƃ�
    public void OnDeselect(BaseEventData eventData)
    {
        if (selectionImage != null)
        {
            selectionImage.SetActive(false); // �摜���\��
        }

        if (buttonImage != null)
        {
            buttonImage.color = normalColor; // �ʏ�F�ɖ߂�
        }
    }
}
