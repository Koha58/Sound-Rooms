using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetMode : MonoBehaviour
{
     Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
                
    void Update()
    {
        GameObject obj = GameObject.Find("Player");                               //Playerオブジェクトを探す
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //付いているスクリプトを取得

        if (rend != null && PS.onoff == 0)
        {
            for (int i = 0; i < rend.materials.Length; i++)
            {
                Material material = rend.materials[i];
                //下記コードでRendering Modeが変更できるが、なくても半透明になる。
                //material.SetFloat("_Mode", 2);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                material.SetColor("_Color", new Color(1, 1, 1, 0.05f));
            }
        }
        else
        {
            for (int i = 0; i < rend.materials.Length; i++)
            {
                Material material = rend.materials[i];
                //下記コードでRendering Modeが変更できるが、なくても半透明になる。
                //material.SetFloat("_Mode", 2);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                material.SetColor("_Color", new Color(1, 1, 1, 1f));
            }
        }
    }
}
