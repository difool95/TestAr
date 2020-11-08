﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gaze : MonoBehaviour
{
    List<InfoBehaviour> infos = new List<InfoBehaviour>();

    private void Start()
    {
        infos = FindObjectsOfType<InfoBehaviour>().ToList();
    }

    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit)){
            GameObject go = hit.collider.gameObject;
            if (go.CompareTag("hasInfo"))
            {
                OpenInfo(go.GetComponent<InfoBehaviour>());
            }
            //else
            //{
            //    CloseAll();
            //}
        }
    }

    void OpenInfo(InfoBehaviour desiredInfo)
    {
        foreach (var info in infos)
        {
            if(info == desiredInfo)
            {
                info.OpenInfo();
            }
            else
            {
                info.CloseInfo();
            }
        }
    }
    void CloseAll()
    {
        foreach (var info in infos)
        {
            info.CloseInfo();
        }
    }
}
