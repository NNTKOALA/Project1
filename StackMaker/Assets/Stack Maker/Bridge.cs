using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] GameObject brick;

    private void Start()
    {
        brick.SetActive(false);
    }

    public void ShowBrick()
    {
        brick.SetActive(true);
    }
}
