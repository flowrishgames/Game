using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenPos : MonoBehaviour
{
    [SerializeField]
    Canvas canvas = null;

    // Start is called before the first frame update
    void Start()
    {
        var aspectRate = GameConfig.DISP_W / (float)Screen.width;
        var screenHeight = Screen.height / 2 * aspectRate;
        Camera.main.orthographicSize = Screen.height / 2 * aspectRate;
        Camera.main.transform.position = new Vector3(GameConfig.DISP_W / 2, -GameConfig.DISP_H / 2, -10);

        if (canvas != null)
        {
            var rect = canvas.gameObject.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0, 1);
        }
    }
}
