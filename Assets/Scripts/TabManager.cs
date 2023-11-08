using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tabs;
   public void OnButtonSelect(int index)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if(index == i)
            {
                tabs[i].gameObject.SetActive(true);
            } else
            {
                tabs[i].gameObject.SetActive(false);
            }

        }
    }
}
