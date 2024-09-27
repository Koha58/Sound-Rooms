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
        GameObject obj = GameObject.Find("Player");                               //Player�I�u�W�F�N�g��T��
        PlayerSeen PS = obj.GetComponent<PlayerSeen>();                           //�t���Ă���X�N���v�g���擾

        if (rend != null && PS.onoff == 0)
        {
            for (int i = 0; i < rend.materials.Length; i++)
            {
                Material material = rend.materials[i];
                //���L�R�[�h��Rendering Mode���ύX�ł��邪�A�Ȃ��Ă��������ɂȂ�B
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
                //���L�R�[�h��Rendering Mode���ύX�ł��邪�A�Ȃ��Ă��������ɂȂ�B
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
