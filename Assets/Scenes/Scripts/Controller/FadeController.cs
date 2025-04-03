using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GetRecorer�V�[������TutorialScene�ɐ؂�ւ����̃t�F�[�h�������s���N���X
/// </summary>
public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;  // �t�F�[�h�p��Image
    [SerializeField] private float fadeDuration = 2f;  // �t�F�[�h�A�E�g�ɂ����鎞��
    private CanvasGroup canvasGroup;
    private float clearAlpha = 0f; // �����ɂȂ�l
    private float visibleAlpha = 1f; // ��������l

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
        canvasGroup.alpha = clearAlpha;  // �����x��0��
    }

    // �t�F�[�h�A�E�g����
    public IEnumerator FadeOut()
    {
        fadeImage.enabled = true;  // Image��\��
        float timeElapsed = clearAlpha;

        // �����x��0����1�Ƀt�F�[�h�C��
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(clearAlpha, visibleAlpha, timeElapsed / fadeDuration);
            yield return null;
        }

        // ���S�ɕs������
        canvasGroup.alpha = visibleAlpha;
    }
}