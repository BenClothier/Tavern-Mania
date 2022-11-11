using UnityEditor;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Liquid liquid { get; set; }

    public bool ContainsLiquid => true;

    public bool TakeMeasurementOfContents(out Liquid liquid)
    {
        liquid = this.liquid;
        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (liquid != null)
        {
            Handles.Label(transform.position, liquid.Name);
        }
    }
#endif
}
