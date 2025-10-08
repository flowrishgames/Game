using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Config")]
public class GameConfig : ScriptableObject
{
    //-----------------------------------
    // 調整パラメータ
    //-----------------------------------
    public const int IKIGIRE_LIMIT = 20;
    public const int HASHIRU_SPEED = 5;
    public const int RUNNING_COUNT = 1;
    public const int ENERGY_RECOVER = 1;
    public const int ENERGY_EXPEND = -1;
    public const int ENERGY_EXPEND_IKIGIRE = -2;

    // 画面サイズ
    /** 画面サイズ：W */
    public const int DISP_W = 480;
    /** 画面サイズ：H */
    public const int DISP_H = 480;
    /** 画面周りの余白サイズ */
    int DISP_FRAME = 100;
    // 設定値を集約
}
