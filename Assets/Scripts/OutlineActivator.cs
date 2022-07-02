using System;
using UnityEngine;

public enum OutlineType
{
    Player, Hostile, Ally, Neutral
}

[RequireComponent(typeof(Outline))]
public class OutlineActivator : MonoBehaviour
{
    private Outline _outline;

    [SerializeField] private OutlineType outlineType;
    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    private void Start()
    {
        switch (outlineType)
        {
            case OutlineType.Player:
                _outline.OutlineColor = Color.black;
                break;
            case OutlineType.Hostile:
                _outline.OutlineColor = Color.red;
                break;
            case OutlineType.Ally:
                _outline.OutlineColor = Color.green;
                break;
            case OutlineType.Neutral:
                _outline.OutlineColor = Color.white;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _outline.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _outline.enabled = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _outline.enabled = false;
        }
    }
}
