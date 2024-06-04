using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//カメラが壁の向こうを映さないようにする
public class WallCheckCamera : MonoBehaviour
{
    private GameObject Parent;

    private Vector3 Position;
    //光線投射する
    private RaycastHit Hit;//レイキャストによる情報を得るための構造体

    private float Distance;

    private int Mask;

    void Start()
    {
        Parent = transform.root.gameObject;//一番親のオブジェクトを取得(Player)

        Position = transform.localPosition; //相対座標値
        //Vector3.Distance:Playerと カメラの間の距離を返す               
        Distance = Vector3.Distance(Parent.transform.position, transform.position);//カメラとプレイヤーの距離

        Mask = ~(1 << LayerMask.NameToLayer("Player"));
        // LayerMask.NameToLayer("Player"): 定義されているレイヤーのインデックスを返す。今回のレイヤーはPlayer
    }

    void Update()
    {
        if (Physics.CheckSphere(Parent.transform.position, 0.3f, Mask))
        {
            transform.position = Vector3.Lerp(transform.position, Parent.transform.position, 1);
            //絶対座標軸上の座標値
            //Vector3.Lerp:直線上にある２つのベクトル間を補間する関数
            //https://qiita.com/aimy-07/items/ad0d99191da21c0adbc3
        }
        else if (Physics.SphereCast(Parent.transform.position, 0.3f, (transform.position - Parent.transform.position).normalized, out Hit, Distance, Mask))
        {
            transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * Hit.distance;
            //絶対座標軸上の座標値
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Position, 1);
            //transform.localPosition:相対座標値
        }
    }
}
