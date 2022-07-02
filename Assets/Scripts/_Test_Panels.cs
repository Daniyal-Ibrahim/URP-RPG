using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class _Test_Panels : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject equipment;

    private bool _isInv;
    private bool _isChar;
    private bool _isEqu;

    public void ShowInventory()
    {
        if (_isInv)
        {
            HideInventory();
            _isInv = false;
        }
        else
        {
             inventory.transform.DOScale(Vector3.one, 0.5f);
             _isInv = true;
        }
       
    }
    
    public void HideInventory()
    {
        inventory.transform.DOScale(Vector3.zero, 0.5f);
    }
    
    public void ShowCharacter()
    {
        if (_isChar)
        {
            HideCharacter();
            _isChar = false;
        }
        else
        {
            character.transform.DOScale(Vector3.one, 0.5f);
            _isChar = true;
        }
    }
    
    public void HideCharacter()
    {
        character.transform.DOScale(Vector3.zero, 0.5f);
    }
    
    public void ShowEquipment()
    {
        if (_isEqu)
        {
            HideEquipment();
            _isEqu = false;
        }
        else
        {
            equipment.transform.DOScale(Vector3.one, 0.5f);
            _isEqu = true;
        }
    }
    
    public void HideEquipment()
    {
        equipment.transform.DOScale(Vector3.zero, 0.5f);
    }
}
