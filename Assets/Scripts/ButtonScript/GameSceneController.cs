using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InputDeviceManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    //ê›íËëIëñ⁄àÛ
    [SerializeField] GameObject Select;

    private float MainSettingOriginPositionX = -773;
    private float MainSettingOriginPositionY = 317;
    private float MainSettingOriginPositionZ = 0;

    private float MainSettingChangePositionY;

    int MainSelectPosition = 0;
    bool MainSelectPositionSelect;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Controller();
    }

    private void Controller()
    {
        Transform MainSettingSelectTransform = Select.transform;
        MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
        MainSettingChangePositionY = MainSettingOriginPositionY;

        if (Input.GetAxis("Vertical") == 0)
        {
            MainSelectPositionSelect = false;
        }

        if (Input.GetAxisRaw("Vertical") < 0&& !MainSelectPositionSelect)
        {
            if (MainSelectPosition == 0)
            {
                MainSelectPosition++;
                MainSettingChangePositionY = 212;
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
            }
            else if (MainSelectPosition == 1)
            {
                MainSelectPosition++;
                MainSettingChangePositionY = 113;
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
            }
            else if (MainSelectPosition == 2)
            {
                MainSelectPosition = 0;
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
                MainSettingChangePositionY = MainSettingOriginPositionY;
            }

            MainSelectPositionSelect = true;
        }
        else if (Input.GetAxisRaw("Vertical") > 0&& !MainSelectPositionSelect)
        {
            if (MainSelectPosition == 0)
            {
                MainSelectPosition = 2;
                MainSettingChangePositionY = 113;
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
            }
            else if (MainSelectPosition == 1)
            {
                MainSelectPosition--;
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingOriginPositionY, MainSettingOriginPositionZ);
                MainSettingChangePositionY = MainSettingOriginPositionY;
            }
            else if (MainSelectPosition == 2)
            {
                MainSettingChangePositionY = 212;
                MainSettingSelectTransform.transform.localPosition = new Vector3(MainSettingOriginPositionX, MainSettingChangePositionY, MainSettingOriginPositionZ);
            }

            MainSelectPositionSelect = true;
        }
    }

}
