using UnityEditor;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Liquid liquid { get; private set; }

    public bool ContainsLiquid => true;

    private void Awake()
    {
        liquid = Resources.LoadAll<Liquid>("ScriptableObjects")[0];
    }

    public bool TakeMeasurementOfContents(out Liquid liquid)
    {
        liquid = this.liquid;
        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.Label(transform.position, liquid.Name);
    }
#endif
}
