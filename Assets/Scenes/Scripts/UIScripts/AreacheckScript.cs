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
    private SlideUIControll uiCount;    // 最初のUI
    [SerializeField]
    private SlideUIControll uiCount2;   // 次のUI
    [SerializeField]
    private SlideUIControll uiCount3;   // 次のUI
    [SerializeField]
    private SlideUIControll uiCount4;   // 次のUI
    [SerializeField]
    private SlideUIControll uiCount5;   // 次のUI
    [SerializeField]
    private SlideUIControll uiCount6;   // 次のUI
    [SerializeField]
    private SlideUIControll uiCount7;   // 次のUI
    [SerializeField]
    private SlideUIControll uiCount8;   // 次のUI

    // Start is called before the first frame update
    void Start()
    {
        // 初期状態として、uiCountの状態を1に設定
        uiCount.state = 1;  // 最初のUIを表示
    }

    // トリガーに入ったときに呼ばれるメソッド
    void OnTriggerEnter(Collider other)
    {
        // 他のオブジェクトが指定したエリアに入った場合、そのエリアに対応するUIを変更

        // "AreaAcheck"というタグがついているオブジェクトが入った場合
        if (other.CompareTag("AreaAcheck"))
        {
            // 全てのUIを非表示にし、uiCountのみを表示
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount.state = 1;  // uiCountを表示
        }
        // "AreaBcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaBcheck"))
        {
            // 全てのUIを非表示にし、uiCount2のみを表示
            uiCount.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount2.state = 1;  // uiCount2を表示
        }
        // "AreaCcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaCcheck"))
        {
            // 全てのUIを非表示にし、uiCount3のみを表示
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount3.state = 1;  // uiCount3を表示
        }
        // "AreaDcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaDcheck"))
        {
            // 全てのUIを非表示にし、uiCount4のみを表示
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount4.state = 1;  // uiCount4を表示
        }
        // "AreaEcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaEcheck"))
        {
            // 全てのUIを非表示にし、uiCount5のみを表示
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount5.state = 1;  // uiCount5を表示
        }
        // "AreaFcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaFcheck"))
        {
            // 全てのUIを非表示にし、uiCount6のみを表示
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 0;
            uiCount6.state = 1;  // uiCount6を表示
        }
        // "AreaGcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaGcheck"))
        {
            // 全てのUIを非表示にし、uiCount7のみを表示
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount8.state = 0;
            uiCount7.state = 1;  // uiCount7を表示
        }
        // "AreaHcheck"というタグがついているオブジェクトが入った場合
        else if (other.CompareTag("AreaHcheck"))
        {
            // 全てのUIを非表示にし、uiCount8のみを表示
            uiCount.state = 0;
            uiCount2.state = 0;
            uiCount3.state = 0;
            uiCount4.state = 0;
            uiCount5.state = 0;
            uiCount6.state = 0;
            uiCount7.state = 0;
            uiCount8.state = 1;  // uiCount8を表示
        }
    }
}
