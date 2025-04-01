using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectManager : MonoBehaviour
{
    // 定数定義
    private const int StageIndex0 = 0;  // チュートリアルのインデックス
    private const int StageIndex1 = 1;  // ステージ1のインデックス
    private const int StageIndex2 = 2;  // ステージ2のインデックス

    // ステージボタンを格納する配列
    [SerializeField] private GameObject[] StageButtons;

    // ステージ選択画面で使用するボタンの参照
    [SerializeField] private GameObject RightButton, LeftButton, GameStartButton, BackStartButton;

    // ステージごとの動画とタイトル
    [SerializeField] private GameObject[] StageVideos;
    [SerializeField] private GameObject[] StageTitles;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      StageSelect();
    }

    // ステージ選択の処理
    void StageSelect()
    {
        // 現在選択されているゲームオブジェクトを取得
        var selectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (selectedGameObject == StageButtons[0])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[0].GetComponent<RawImage>().enabled = true;
            StageVideos[1].GetComponent<RawImage>().enabled = false;
            StageVideos[2].GetComponent<RawImage>().enabled = false;

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageTitles[0].GetComponent<Image>().enabled = true;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = false;

            }
        }
        else if (selectedGameObject == StageButtons[1])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageVideos[0].GetComponent<RawImage>().enabled = false;
            StageVideos[1].GetComponent<RawImage>().enabled = true;
            StageVideos[2].GetComponent<RawImage>().enabled = false;

            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = true;
                StageVideos[2].GetComponent<RawImage>().enabled = false;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = true;
                StageTitles[2].GetComponent<Image>().enabled = false;
            }

        }
        else if (selectedGameObject == StageButtons[2])
        {
            StageButtons[0].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[1].GetComponent<Image>().color = new Color32(255, 255, 255, 45);
            StageButtons[2].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            StageVideos[0].GetComponent<RawImage>().enabled = false;
            StageVideos[1].GetComponent<RawImage>().enabled = false;
            StageVideos[2].GetComponent<RawImage>().enabled = true;
            if (Input.GetKeyDown("joystick button 0"))
            {
                StageVideos[0].GetComponent<RawImage>().enabled = false;
                StageVideos[1].GetComponent<RawImage>().enabled = false;
                StageVideos[2].GetComponent<RawImage>().enabled = true;
                StageTitles[0].GetComponent<Image>().enabled = false;
                StageTitles[1].GetComponent<Image>().enabled = false;
                StageTitles[2].GetComponent<Image>().enabled = true;
            }
        }
        else if (selectedGameObject == GameStartButton)
        {

        }
        else if (selectedGameObject == null)
        {
            // ボタンが選択されていない場合、最初のボタンにフォーカスを当てる
            EventSystem.current.SetSelectedGameObject(StageButtons[0]);
        }
    }
}