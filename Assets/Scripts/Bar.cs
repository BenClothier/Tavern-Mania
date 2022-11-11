using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

    public bool IsVacant { get; private set; }

    public bool TakeVacancy()
    {
        if (IsVacant = true)
        {
            IsVacant = false;
            return true;
        } else
        {
            return false;
        }
    }

    public void Leave()
    {
        IsVacant = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsVacant = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
