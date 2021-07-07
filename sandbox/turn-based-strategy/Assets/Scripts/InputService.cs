using UnityEngine;

public class InputService
{
    public static Vector3 GetInputWorldPosition()
    {
        Vector3 vec = GetInputWorldPosition(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }

    private static Vector3 GetInputWorldPosition(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPos = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPos;
    }
}
