using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    private UIInputActions _uiInputActions;

    [SerializeField] GameObject UICanvas;
    [SerializeField] GameObject SettingPanel1;
    [SerializeField] GameObject SettingPanel2;
    [SerializeField]GameObject[] Cursors;

    [SerializeField] GameObject SettingButton;                 //ゲーム内設定を変更する画面へ遷移するボタン
    [SerializeField] GameObject OperationExplanationButton;    //操作説明画面へ遷移するボタン
    [SerializeField] GameObject TitleButton;                   //タイトル画面へ遷移するボタン

    bool menu;

    int mainSelectCount;
    bool SelectUp;
    bool SelectDown;

    int CursorsCount;
    bool Cursorsin;
    bool CursorsUp;
    bool CursorsDown;

    private Vector2 mainSlect;

    // Start is called before the first frame update
    void Start()
    {
        _uiInputActions = new UIInputActions();
        _uiInputActions.Enable();

        UICanvas.SetActive(false);
        SettingPanel1.SetActive(false);
        SettingPanel2.SetActive(false);

        menu = false;

        mainSelectCount = 0;
        CursorsCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MainImageColor();
        Select1ImageColor();

        if (_uiInputActions.SettingUI.Pouse.triggered)
        {
            UICanvas.SetActive(true);
            menu = true;
            Time.timeScale = 0;
        }

        if (menu)
        {
            if (_uiInputActions.SettingUI.MainSelectUp.triggered)
            {
                SelectUp = true;
            }

            if (_uiInputActions.SettingUI.MainSelectDown.triggered)
            {
                SelectDown = true;
            }

            if (SelectUp == true)
            {
                mainSelectCount += 1;
                if (mainSelectCount > 3)
                {
                    mainSelectCount = 2;
                }
                Debug.Log(mainSelectCount);
                SelectUp = false;
            }

            if (SelectDown == true)
            {
                mainSelectCount -= 1;
                if (mainSelectCount < 0)
                {
                    mainSelectCount = 0;
                }
                Debug.Log(mainSelectCount);
                SelectDown = false;
            }

            if (_uiInputActions.SettingUI.SettingPanel1.triggered)
            {
                Cursorsin = true;
            }

            if(Cursorsin)
            {

            }
        }
    }


    void MainImageColor()
    {
        Color whiteColor = Color.white;
        Color yellowColor = Color.yellow;

        Image imageSettingButton = SettingButton.GetComponent<Image>();
        Image imageOperationExplanationButton = OperationExplanationButton.GetComponent<Image>();
        Image imageTitleButton = TitleButton.GetComponent<Image>();

        if (mainSelectCount==2)
        {
            imageSettingButton.color = yellowColor;
            imageOperationExplanationButton.color = whiteColor;
            imageTitleButton.color = whiteColor;
        }
        else if(mainSelectCount==1)
        {
            imageSettingButton.color = whiteColor;
            imageOperationExplanationButton.color = yellowColor;
            imageTitleButton.color = whiteColor;
        }
        else if (mainSelectCount==0)
        {
            imageSettingButton.color = whiteColor;
            imageOperationExplanationButton.color = whiteColor;
            imageTitleButton.color = yellowColor;
        }
    }

    void Select1ImageColor()
    {
        Color whiteColor = Color.white;
        Color yellowColor = Color.yellow;

        Image Cursors0 = Cursors[0].GetComponent<Image>();
        Image Cursors1 = Cursors[1].GetComponent<Image>();
        Image Cursors2 = Cursors[2].GetComponent<Image>();
        Image Cursors3 = Cursors[3].GetComponent<Image>();

        if (CursorsCount == 3)
        {
            Cursors0.color = yellowColor;
            Cursors1.color = whiteColor;
            Cursors2.color = whiteColor;
            Cursors3.color = whiteColor;
        }
        else if (CursorsCount == 2)
        {
            Cursors0.color = whiteColor;
            Cursors1.color = yellowColor;
            Cursors2.color = whiteColor;
            Cursors3.color = whiteColor;
        }
        else if (CursorsCount == 1)
        {
            Cursors0.color = whiteColor;
            Cursors1.color = whiteColor;
            Cursors2.color = yellowColor;
            Cursors3.color = whiteColor;
        }
        else if (CursorsCount == 0)
        {
            Cursors0.color = whiteColor;
            Cursors1.color = whiteColor;
            Cursors2.color = whiteColor;
            Cursors3.color = yellowColor;
        }
    }

}
