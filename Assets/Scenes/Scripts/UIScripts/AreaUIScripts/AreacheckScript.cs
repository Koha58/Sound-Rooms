using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameSceneでエリアごとのUIの表示を管理するクラス
/// </summary>
public class AreacheckScript : MonoBehaviour
{
    // UIの状態を管理するSlideUIControllのインスタンスを格納する変数
    [SerializeField]
    private SlideUIControll uiAreaA;  // エリアA用UI
    [SerializeField]
    private SlideUIControll uiAreaB;    // エリアB用UI
    [SerializeField]
    private SlideUIControll uiAreaC;    // エリアC用UI
    [SerializeField]
    private SlideUIControll uiAreaD;    // エリアD用UI
    [SerializeField]
    private SlideUIControll uiAreaE;    // エリアE用UI
    [SerializeField]
    private SlideUIControll uiAreaF;    // エリアF用UI
    [SerializeField]
    private SlideUIControll uiAreaG;    // エリアG用UI
    [SerializeField]
    private SlideUIControll uiAreaH;    // エリアH用UI

    // Start is called before the first frame update
    void Start()
    {
        // 初期状態として、エリアA用UIを表示
        ShowUI(uiAreaA);
    }

    // トリガーに入ったときに呼ばれるメソッド
    void OnTriggerEnter(Collider other)
    {
        // 入ったエリアに対応するUIを表示
        if (other.CompareTag("AreaAcheck"))
        {
            ShowUI(uiAreaA);  // エリアA用UIを表示
        }
        else if (other.CompareTag("AreaBcheck"))
        {
            ShowUI(uiAreaB);  // エリアB用UIを表示
        }
        else if (other.CompareTag("AreaCcheck"))
        {
            ShowUI(uiAreaC);  // エリアC用UIを表示
        }
        else if (other.CompareTag("AreaDcheck"))
        {
            ShowUI(uiAreaD);  // エリアD用UIを表示
        }
        else if (other.CompareTag("AreaEcheck"))
        {
            ShowUI(uiAreaE);  // エリアE用UIを表示
        }
        else if (other.CompareTag("AreaFcheck"))
        {
            ShowUI(uiAreaF);  // エリアF用UIを表示
        }
        else if (other.CompareTag("AreaGcheck"))
        {
            ShowUI(uiAreaG);  // エリアG用UIを表示
        }
        else if (other.CompareTag("AreaHcheck"))
        {
            ShowUI(uiAreaH);  // エリアH用UIを表示
        }
    }

    // 指定されたUIを表示し、他のUIは非表示にするメソッド
    private void ShowUI(SlideUIControll activeUI)
    {
        // 全てのUIを非表示にする
        uiAreaA.state = SlideUIControll.State.Initial;
        uiAreaB.state = SlideUIControll.State.Initial;
        uiAreaC.state = SlideUIControll.State.Initial;
        uiAreaD.state = SlideUIControll.State.Initial;
        uiAreaE.state = SlideUIControll.State.Initial;
        uiAreaF.state = SlideUIControll.State.Initial;
        uiAreaG.state = SlideUIControll.State.Initial;
        uiAreaH.state = SlideUIControll.State.Initial;

        // 指定されたUIを表示
        activeUI.state = SlideUIControll.State.SlideIn;
    }
}
