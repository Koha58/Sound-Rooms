using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;  // �t�F�[�h�p��Image
    [SerializeField] private float fadeDuration = 2f;  // �t�F�[�h�A�E�g�ɂ����鎞��
    private CanvasGroup canvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        // Image��CanvasGroup�������Ă��Ȃ���΁A�ǉ�
        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();
        }

        // �ŏ��͔�\���ɂ��Ă���
        fadeImage.enabled = false;  // Image���\��
        canvasGroup.alpha = 0f;  // �����x��0��
    }

    // �t�F�[�h�A�E�g����
    public IEnumerator FadeOut()
    {
        fadeImage.enabled = true;  // Image��\��
        float timeElapsed = 0f;

        // �����x��0����1�Ƀt�F�[�h�C��
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            yield return null;
        }

        // ���S�ɕs������
        canvasGroup.alpha = 1f;
    }
}