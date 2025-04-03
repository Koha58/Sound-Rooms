using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// GetRecorerシーンからTutorialSceneに切り替え時のフェード処理を行うクラス
/// </summary>
public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;  // フェード用のImage
    [SerializeField] private float fadeDuration = 2f;  // フェードアウトにかかる時間
    private CanvasGroup canvasGroup;
    private float clearAlpha = 0f; // 透明になる値
    private float visibleAlpha = 1f; // 可視化する値

    // Start is called before the first frame update
    void Start()
    {
        // ImageがCanvasGroupを持っていなければ、追加
        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();
        }

        // 最初は非表示にしておく
        fadeImage.enabled = false;  // Imageを非表示
        canvasGroup.alpha = clearAlpha;  // 透明度を0に
    }

    // フェードアウト処理
    public IEnumerator FadeOut()
    {
        fadeImage.enabled = true;  // Imageを表示
        float timeElapsed = clearAlpha;

        // 透明度を0から1にフェードイン
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(clearAlpha, visibleAlpha, timeElapsed / fadeDuration);
            yield return null;
        }

        // 完全に不透明に
        canvasGroup.alpha = visibleAlpha;
    }
}