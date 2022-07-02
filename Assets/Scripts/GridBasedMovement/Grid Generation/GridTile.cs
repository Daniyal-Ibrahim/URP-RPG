using _Scripts;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private Renderer _meshRenderer;
    private Material _material;
    private Color _defaultColor;
    public Vector2 location;
    private void Start()
    {
        _meshRenderer = GetComponent<Renderer>();
        _material = _meshRenderer.material;
        _defaultColor = _material.color;
        
    }

    private void OnEnable()
    {
        if(!_meshRenderer.enabled)
            _meshRenderer.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collider"))
        {
            _meshRenderer.enabled = false;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        _meshRenderer.enabled = true;
    }

    private void OnMouseOver()
    {
        _material.color = new Color(.8f, 0f, 0f, .25f);
    }

    private void OnMouseExit()
    {
        _material.color = _defaultColor;
    }

    private void OnMouseDown()
    {
        Debug.Log("Destination set via mouse");
        GameManager.Instance.agent.SetDestination(this.transform.position);
        GameManager.Instance.normalGrid.SetActive(false);
        _material.color = _defaultColor;
    }
}
