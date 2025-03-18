using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// オプション画面表示時のコントローラーでのカーソルを管理するクラス
/// </summary>
public class GameSceneController : MonoBehaviour
{
    // 設定選択目印
    [SerializeField] GameObject Select;

    // 設定項目のY座標（選択肢の位置を管理）
    private const float MAIN_SETTING_ORIGIN_POSITION_X = -773f;  // 初期位置X
    private const float MAIN_SETTING_ORIGIN_POSITION_Y = 317f;  // 初期位置Y
    private const float MAIN_SETTING_ORIGIN_POSITION_Z = 0f;    // 初期位置Z

    // 各選択肢のY座標（縦の位置）
    private const float SECOND_SETTING_POSITION_Y = 212f;   // 2番目の選択肢のY座標
    private const float THIRD_SETTING_POSITION_Y = 113f;    // 3番目の選択肢のY座標

    // 選択位置（0: 1番目, 1: 2番目, 2: 3番目）
    private const int FIRST_SETTING = 0;
    private const int SECOND_SETTING = 1;
    private const int THIRD_SETTING = 2;

    // 設定選択の現在位置
    private int mainSelectPosition = FIRST_SETTING;

    // 設定が選択されているかどうかのフラグ
    private bool mainSelectPositionSelect;


    void Update()
    {
        // コントローラーの入力に基づいて設定選択処理を呼び出す
        Controller();
    }

    // 設定の選択を管理するメソッド
    private void Controller()
    {
        // 設定選択の目印となるオブジェクトのTransformを取得
        Transform mainSettingSelectTransform = Select.transform;

        // 初期位置を設定
        mainSettingSelectTransform.transform.localPosition = new Vector3(MAIN_SETTING_ORIGIN_POSITION_X, MAIN_SETTING_ORIGIN_POSITION_Y, MAIN_SETTING_ORIGIN_POSITION_Z);

        // 設定変更時のY座標を初期位置に設定
        float mainSettingChangePositionY = MAIN_SETTING_ORIGIN_POSITION_Y;

        // 上下入力が0の場合、選択状態を解除
        if (Input.GetAxis("Vertical") == 0)
        {
            mainSelectPositionSelect = false;
        }

        // 下方向の入力（Verticalが負の値）で選択肢を下に移動
        if (Input.GetAxisRaw("Vertical") < 0 && !mainSelectPositionSelect)
        {
            switch (mainSelectPosition)
            {
                case FIRST_SETTING:
                    // 1番目の選択肢から2番目に移動
                    mainSelectPosition = SECOND_SETTING;
                    mainSettingChangePositionY = SECOND_SETTING_POSITION_Y;
                    break;
                case SECOND_SETTING:
                    // 2番目の選択肢から3番目に移動
                    mainSelectPosition = THIRD_SETTING;
                    mainSettingChangePositionY = THIRD_SETTING_POSITION_Y;
                    break;
                case THIRD_SETTING:
                    // 3番目の選択肢から1番目に戻る
                    mainSelectPosition = FIRST_SETTING;
                    mainSettingChangePositionY = MAIN_SETTING_ORIGIN_POSITION_Y;
                    break;
            }

            // 設定選択目印の位置を更新
            mainSettingSelectTransform.transform.localPosition = new Vector3(MAIN_SETTING_ORIGIN_POSITION_X, mainSettingChangePositionY, MAIN_SETTING_ORIGIN_POSITION_Z);
            mainSelectPositionSelect = true;
        }
        // 上方向の入力（Verticalが正の値）で選択肢を上に移動
        else if (Input.GetAxisRaw("Vertical") > 0 && !mainSelectPositionSelect)
        {
            switch (mainSelectPosition)
            {
                case FIRST_SETTING:
                    // 1番目の選択肢から3番目に移動
                    mainSelectPosition = THIRD_SETTING;
                    mainSettingChangePositionY = THIRD_SETTING_POSITION_Y;
                    break;
                case SECOND_SETTING:
                    // 2番目の選択肢から1番目に戻る
                    mainSelectPosition = FIRST_SETTING;
                    mainSettingChangePositionY = MAIN_SETTING_ORIGIN_POSITION_Y;
                    break;
                case THIRD_SETTING:
                    // 3番目の選択肢から2番目に移動
                    mainSelectPosition = SECOND_SETTING;
                    mainSettingChangePositionY = SECOND_SETTING_POSITION_Y;
                    break;
            }

            // 設定選択目印の位置を更新
            mainSettingSelectTransform.transform.localPosition = new Vector3(MAIN_SETTING_ORIGIN_POSITION_X, mainSettingChangePositionY, MAIN_SETTING_ORIGIN_POSITION_Z);
            mainSelectPositionSelect = true;
        }
    }
}