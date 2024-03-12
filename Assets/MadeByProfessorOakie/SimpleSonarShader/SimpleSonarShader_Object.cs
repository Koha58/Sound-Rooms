// SimpleSonarShader scripts and shaders were written by Drew Okenfuss.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSonarShader_Object : MonoBehaviour
{

    // All the renderers that will have the sonar data sent to their shaders.(ソナーデータがシェーダーに送られるすべてのレンダラー)
    private Renderer[] ObjectRenderers;

    // Throwaway values to set position to at the start.(スタート時にポジションを設定するための捨て値)
    private static readonly Vector4 GarbagePosition = new Vector4(-5000, -5000, -5000, -5000);

    // The number of rings that can be rendered at once.(一度にレンダリングできるリングの数)
    // Must be the samve value as the array size in the shader.(シェーダの配列サイズと同じ値でなければならない)
    private static int QueueSize = 20;

    // Queue of start positions of sonar rings.(ソナーリングの開始位置のキュー)
    // The xyz values hold the xyz of position.(xyz値は位置のxyzを保持する)
    // The w value holds the time that position was started.(w値は、ポジションが開始された時間を保持する)
    private static Queue<Vector4> positionsQueue = new Queue<Vector4>(QueueSize);

    // Queue of intensity values for each ring.(各リングの強度値のキュー)
    // These are kept in the same order as the positionsQueue.(これらは、positionsQueueと同じ順序で保持される)
    private static Queue<float> intensityQueue = new Queue<float>(QueueSize);

    // Make sure only 1 object initializes the queues.(キューを初期化するオブジェクトが1つだけであることを確認する)
    private static bool NeedToInitQueues = true;

    // Will call the SendSonarData for each object.(各オブジェクトに対して SendSonarData を呼び出す)
    private delegate void Delegate();
    private static Delegate RingDelegate;

    private void Start()
    {
        // Get renderers that will have effect applied to them(エフェクトが適用されるレンダラーを取得する)
        ObjectRenderers = GetComponentsInChildren<Renderer>();

        if(NeedToInitQueues)
        {
            NeedToInitQueues = false;
            // Fill queues with starting values that are garbage values(ゴミ値である開始値でキューを埋める)
            for (int i = 0; i < QueueSize; i++)
            {
                positionsQueue.Enqueue(GarbagePosition);
                intensityQueue.Enqueue(-5000f);
            }
        }

        // Add this objects function to the static delegate(このオブジェクト関数を静的デリゲートに追加する)
        RingDelegate += SendSonarData;
    }

    /// <summary>
    /// Starts a sonar ring from this position with the given intensity.(この位置から、指定された強度でソナーリングを開始する)
    /// </summary>
    public void StartSonarRing(Vector4 position, float intensity)
    {
        // Put values into the queue(値をキューに入れる)
        position.w = Time.timeSinceLevelLoad;
        positionsQueue.Dequeue();
        positionsQueue.Enqueue(position);

        intensityQueue.Dequeue();
        intensityQueue.Enqueue(intensity);

        RingDelegate();
    }

    /// <summary>
    /// Sends the sonar data to the shader.(ソナーデータをシェーダーに送る)
    /// </summary>
    private void SendSonarData()
    {
        // Send updated queues to the shaders(更新されたキューをシェーダーに送る)
        foreach (Renderer r in ObjectRenderers)
        {
            r.material.SetVectorArray("_hitPts", positionsQueue.ToArray());
            r.material.SetFloatArray("_Intensity", intensityQueue.ToArray());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Start sonar ring from the contact point(接点からソナーリングをスタート)
        StartSonarRing(collision.contacts[0].point, collision.impulse.magnitude / 7.0f);
    }

    private void OnDestroy()
    {
        RingDelegate -= SendSonarData;
    }

}
