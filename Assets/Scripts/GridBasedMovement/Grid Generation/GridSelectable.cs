using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class GridSelectable : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        gameObject.GetComponent<Outline>().enabled = true;
        GameManager.Instance.normalGrid.SetActive(true);
        GameManager.Instance.normalGrid.transform.position = new Vector3(transform.position.x,0.001f,transform.position.z);
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }
    
    /*private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().enabled = false;

        if (GameManager.Instance.normalGrid.activeInHierarchy)
        {
            GameManager.Instance.normalGrid.SetActive(false);
        }
    }*/
}
