using UnityEngine;

public class KeyboardInput
{
    public Vector2 SetInputDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            direction += Vector2.left;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            direction += Vector2.right;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            direction += Vector2.up;

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            direction += Vector2.down;

        return direction;
    }

    public bool IsSkipButtonPressed()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.KeypadEnter))
            return true;
        else
            return false;
    }

    public bool IsDashButtonPressed()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse1))
            return true;
        else
            return false;
    }
}
