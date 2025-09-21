using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    private Vector3 defaultScale;
    private bool isShrunk = false;

    void Start()
    {
        defaultScale = transform.localScale; // store original size once
    }

    public void Shrink(float shrinkFactor)
    {
        if (!isShrunk)
        {
            transform.localScale *= shrinkFactor;
            isShrunk = true;
        }
    }

    public void Expand()
    {
        if (isShrunk)
        {
            transform.localScale = defaultScale;
            isShrunk = false;
        }
    }

    public bool IsShrunk()
    {
        return isShrunk;
    }
}
