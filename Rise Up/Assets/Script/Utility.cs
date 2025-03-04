using Unity.VisualScripting;
using UnityEngine;

public static class Utility
{
    public static bool IsInScreen(Vector2 position)
    {
        Vector2 maxBound = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 minBound = maxBound * -1;

        bool xCheck = (position.x < maxBound.x) && (position.x > minBound.x);
        bool yCheck = (position.y > minBound.y);

        return xCheck && yCheck;
    }
}
