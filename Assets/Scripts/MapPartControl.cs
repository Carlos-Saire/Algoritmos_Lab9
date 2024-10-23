using UnityEngine;
public class MapPartControl : MonoBehaviour
{
    private SpriteRenderer _compSpriteRenderer;
    private void Awake()
    {
        _compSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetPrite(Sprite newSprite)
    {
        _compSpriteRenderer.sprite = newSprite;
    }
}
