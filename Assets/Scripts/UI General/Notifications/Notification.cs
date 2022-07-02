using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;


public class Notification : MonoBehaviour
{ 
    [SerializeField] protected int showY;
    [SerializeField] protected int HideY;

    [Button]
    public void ShowNotification()
    {
        transform.DOLocalMoveY(showY, 0.25f);
    }

    [Button]
    public void HideNotification()
    {
        transform.DOLocalMoveY(HideY, 0.25f);
    }
}
