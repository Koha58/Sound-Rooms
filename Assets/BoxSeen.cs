using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxSeen : MonoBehaviour
{
    public GameObject Box;
    // Start is called before the first frame update
    void Start()
    {
        Box = GameObject.FindWithTag("Box");
        Box.SetActive(false);
    }
}
