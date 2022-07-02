using _Scripts.Inventory_System;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationItem : Notification
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;

    public new void ShowNotification()
    {
        transform.DOScale(Vector3.one, 0.5f);
        transform.DOLocalMoveY(showY, 0.25f);
    }

    public new void HideNotification()
    {
        transform.DOLocalMoveY(HideY, 0.5f);
        transform.DOScale(Vector3.zero, 0.5f);
    }
    public void SetItemData(ItemSlot obj)
    {
        icon.sprite = obj.item.Icon;

        var c = ColorUtility.ToHtmlStringRGB(obj.item.rarity.value);
        var builder = new StringBuilder();
        builder.Append("<size=30>").Append($"<color=#{c}>").Append(obj.item.name).Append("</color>").Append(" x").Append(obj.quantity);

        text.text = builder.ToString();
        ShowNotification();
    }
}
