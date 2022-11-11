using UnityEditor;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Liquid liquid;
    private SpriteRenderer sr;

    public Liquid Liquid
    {
        get => liquid;
        set
        {
            liquid = value;
            sr.sprite = value.symbol;
            sr.color = value.colour;
        }
    }

    public bool ContainsLiquid => true;

    public bool TakeMeasurementOfContents(out Liquid liquid)
    {
        liquid = this.Liquid;
        return true;
    }

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Liquid != null)
        {
            Handles.Label(transform.position, Liquid.Name);
        }
    }
#endif
}
