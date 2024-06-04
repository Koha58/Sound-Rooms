using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�J�������ǂ̌��������f���Ȃ��悤�ɂ���
public class WallCheckCamera : MonoBehaviour
{
    private GameObject Parent;

    private Vector3 Position;
    //�������˂���
    private RaycastHit Hit;//���C�L���X�g�ɂ����𓾂邽�߂̍\����

    private float Distance;

    private int Mask;

    void Start()
    {
        Parent = transform.root.gameObject;//��Ԑe�̃I�u�W�F�N�g���擾(Player)

        Position = transform.localPosition; //���΍��W�l
        //Vector3.Distance:Player�� �J�����̊Ԃ̋�����Ԃ�               
        Distance = Vector3.Distance(Parent.transform.position, transform.position);//�J�����ƃv���C���[�̋���

        Mask = ~(1 << LayerMask.NameToLayer("Player"));
        // LayerMask.NameToLayer("Player"): ��`����Ă��郌�C���[�̃C���f�b�N�X��Ԃ��B����̃��C���[��Player
    }

    void Update()
    {
        if (Physics.CheckSphere(Parent.transform.position, 0.3f, Mask))
        {
            transform.position = Vector3.Lerp(transform.position, Parent.transform.position, 1);
            //��΍��W����̍��W�l
            //Vector3.Lerp:������ɂ���Q�̃x�N�g���Ԃ��Ԃ���֐�
            //https://qiita.com/aimy-07/items/ad0d99191da21c0adbc3
        }
        else if (Physics.SphereCast(Parent.transform.position, 0.3f, (transform.position - Parent.transform.position).normalized, out Hit, Distance, Mask))
        {
            transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * Hit.distance;
            //��΍��W����̍��W�l
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Position, 1);
            //transform.localPosition:���΍��W�l
        }
    }
}
