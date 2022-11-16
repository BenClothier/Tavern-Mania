using System;
using UnityEngine;

/// <summary>
/// The scriptable object used to create a float variable as an asset.
/// </summary>
[CreateAssetMenu(fileName = "FloatVariable", menuName = "Game/Variables/Float Variable")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value;

    /// <summary>
    /// Invoked when the variable has a new value.
    /// </summary>
    public event Action<float> OnVariableModified;

    /// <summary>
    /// Gets or sets the value of the float stored in the asset-variable.
    /// </summary>
    public float Value
    {
        get => value;
        set
        {
            if (this.value != value)
            {
                OnVariableModified?.Invoke(value);
            }

            this.value = value;
        }
    }
}
