using System;
using UnityEngine;

/// <summary>
/// The scriptable object used to create a float variable as an asset.
/// </summary>
[CreateAssetMenu(fileName = "IntVariable", menuName = "Game/Variables/Int Variable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int value;

    /// <summary>
    /// Invoked when the variable has a new value.
    /// </summary>
    public event Action<int> OnVariableModified;

    /// <summary>
    /// Gets or sets the value of the float stored in the asset-variable.
    /// </summary>
    public int Value
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
