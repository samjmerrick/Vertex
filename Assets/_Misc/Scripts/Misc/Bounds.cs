using UnityEngine;

public class Bounds {

	public static Vector2 Get()
    {
        Vector2 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
        bounds.x += .5f;
        return bounds;
    }
}
