  
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemSO : ScriptableObject
{
    [Tooltip("The name of the item")]
    [SerializeField] private string _name = default;

    [Tooltip("A preview image for the item")]
    [SerializeField]
    private Sprite _previewImage = default;

    [Tooltip("A description of the item")]
    [SerializeField]
    private string _description = default;

    [Tooltip("A description of the item")]
    [SerializeField]
    private int _healthResorationValue = default;

    [Tooltip("The type of item")]
    [SerializeField]
    private ItemTypeSO _itemType = default;

    [Tooltip("A prefab reference for the model of the item")]
    [SerializeField]
    private GameObject _prefab = default;


    public string Name => _name;
    public Sprite PreviewImage => _previewImage;
    public string Description => _description;
    public int HealthResorationValue => _healthResorationValue;
    public ItemTypeSO ItemType => _itemType;
    public GameObject Prefab => _prefab;
    public virtual List<ItemStack> IngredientsList { get; }
    public virtual ItemSO ResultingDish { get; }

    public virtual bool IsLocalized { get; }
    public virtual Sprite LocalizePreviewImage { get; }
}

