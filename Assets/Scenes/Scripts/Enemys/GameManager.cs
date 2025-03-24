using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
     public static GameManager instance { get; private set; }

    private Dictionary<int, List<Transform>> routes = new Dictionary<int, List<Transform>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    // ルートを登録
    public void RegisterRoute(int characterID, List<Transform> route)
    {
        if (!routes.ContainsKey(characterID))
        {
            routes.Add(characterID, route);
            Debug.Log($"Route registered for Character {characterID}.");
        }
        else
        {
            Debug.LogWarning($"Character {characterID} already has a registered route.");
        }
    }

    // ルートを取得
    public List<Transform> GetRoute(int characterID)
    {
        if (routes.TryGetValue(characterID, out List<Transform> route))
        {
            return route;
        }
        Debug.LogWarning($"No route found for Character {characterID}.");
        return null;
    }
}
