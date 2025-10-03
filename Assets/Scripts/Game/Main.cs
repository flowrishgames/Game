using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Main : MonoBehaviour
{
    /** 不正防止キーワード */
    static string ACCESS_CHECK_KEY = "55f5275f14";

    // 画面サイズ
    /** 画面サイズ：W */
    public static int DISP_W = 480;
    /** 画面サイズ：H */
    public static int DISP_H = 480;
    /** 画面周りの余白サイズ */
    static int DISP_FRAME = 100;

    // キー処理関係
    /** キープレスイベント */
    static int KEY_PRESSED = 1;
    /** キーリリースイベント */
    static int KEY_RELEASED = 0;
    // 対応ビット位置
    /** KEY_0　*/
    static int KEY_0 = 0x00;
    /** KEY_1　*/
    static int KEY_1 = 0x01;
    /** KEY_2　*/
    static int KEY_2 = 0x02;
    /** KEY_3　*/
    static int KEY_3 = 0x03;
    /** KEY_4　*/
    static int KEY_4 = 0x04;
    /** KEY_5　*/
    static int KEY_5 = 0x05;
    /** KEY_6　*/
    static int KEY_6 = 0x06;
    /** KEY_7　*/
    static int KEY_7 = 0x07;
    /** KEY_8　*/
    static int KEY_8 = 0x08;
    /** KEY_9　*/
    static int KEY_9 = 0x09;
    /** 方向キー */
    static int KEY_ARROW = 0x10;
    /** KEY_SELECT　*/
    static int KEY_SELECT = 0x14;



    /** キーリスナー */
    [SerializeField]
    EventTrigger m_key_listener;
    /** キー状態 */
    static int m_key = 0;
    /**　キー押し続け判定フラグ */
    static int m_hold_key_flg = 0;

    /** ルーチン番号 */
    static int m_r0 = 0;
    static int m_r1 = 0;

    // debug:デバック用
    /** 時間表示用 */
    //static var end_time = 0;


    //static private Image		img[] = new Image[16];
    //static int time = 0;
    static int ct = 0;
    //static int touroku = 0;
    //static int jikoshin = 0;
    //static int retry = 0;
    //static int n = 0;
    //static int n2 = 0;
    [SerializeField]
    int game = 0;

    static int go_time = 0;
    static int st_time = 0;
    int score = 0;
    static int gm_off = 0;
    private int plOffsetX = 4;
    private int plOffsetY = 7;
    //static int char_p = 0;
    /// <summary>
    /// ゲームレベル
    /// </summary>
    [SerializeField]
    int level = 0;
    /// <summary>
    /// コンベアスピードレベル(低/高)
    /// </summary>
    [SerializeField]
    int cmbSpeedlevel = 0;
    /// <summary>
    /// コンベア高速タイマー
    /// </summary>
    int u_timer = 0;
    /// <summary>
    /// レベルアップタイマー
    /// </summary>
    static int l_time = 0;
    static int clc = 0;
    static int tubure = 0;
    [SerializeField]
    int ikigire = 0;
    [SerializeField]
    int ikigire_on = 0;
    /// <summary>
    /// エネルギー増減値
    /// </summary>
    [SerializeField]
    int iki_pls = 0;
    /// <summary>
    /// プレイヤー位置
    /// </summary>
    [SerializeField]
    int pl_x = 0;
    static int pl_y = 0;
    //static int opl_x = 0;
    //static int opl_y = 0;
    static int start_time = 0;
    [SerializeField]
    int forward_hashiru = 0;
    /// <summary>
    /// 後ろに引っ張られるスピード
    /// </summary>
    [SerializeField]
    int back_ugoku = 0;
    /// <summary>
    /// ボタンを押下して走っている時間
    /// </summary>
    [SerializeField]
    int running_time = 0;
    /// <summary>
    /// 走っているカウント
    /// </summary>
    static int running_counter = 0;
    static int cmb_x = 0;
    static int cmb_x2 = 0;
    //static int ocmb_x = 0;
    //static int ocmb_x2 = 0;

    static int[] em_x = new int[4];
    static int[] em_y = new int[4];
    //static int[] oem_x = new int[4];
    //static int[] oem_y = new int[4];
    static int[] em_buru = new int[4];
    [SerializeField]
    int[] em_time = new int[4];


    // ムービークリップ
    // 親クリップ：swfRoot
    [SerializeField]
    Transform swfRoot;
    /** タイトル */
    static GameObject mc_title;
    /** ゲーム中 */
    static GameObject mc_game;

    // 親クリップ：mc_game
    /** ベルト */
    static GameObject mc_band_up;
    static GameObject mc_band_down;
    /** ギア */
    static MovieClip mc_gear_l;
    static MovieClip mc_gear_r;
    /** ワイヤー */
    static GameObject[] mc_wire = new GameObject[4];
    /** おもり */
    static GameObject[] mc_weight = new GameObject[4];
    /** コブン */
    static GameObject mc_kobun;
    static GameObject mc_player;
    /** メーター */
    static GameObject mc_energy;
    /** ゲームオーバー */
    static GameObject mc_game_over;
    /** READY */
    static GameObject mc_ready;
    /** スコア表示 */
    //static GameObject[] mc_score = new GameObject[6];

    [SerializeField]
    Text mc_score;

    /** コブンの現在のアニメNo */
    [SerializeField]
    int m_kobun_ani = 0;
    /// <summary>
    /// フレームレート
    /// </summary>
    private const float frameRate = 10f;
    /// <summary>
    /// フレームレート調整でで待つ時間
    /// </summary>
    private float frameInterval = 1000f;
    /// <summary>
    /// 待っている時間
    /// </summary>
    private float frameWaitingTime = 0;

    //[SerializeField]
    //private GameObject kobunObject;
    private Animator kobunAnim;

    [SerializeField]
    private GameObject playerObject;
    private Animator playerAnim;
    private int plState = 0;

    private SpriteRenderer energyGauge;
    /// <summary>
    /// 動作しているおもり
    /// </summary>
    private int em_move_counter = 0;
    /// <summary>
    /// おもり動作させるフラグ
    /// </summary>
    private bool is_em_move = false;

    //-----------------------------------
    // 調整パラメータ
    //-----------------------------------
    public const int IKIGIRE_LIMIT = 20;
    public const int HASHIRU_SPEED = 5;
    public const int RUNNING_COUNT = 1;
    public const int ENERGY_RECOVER = 1;
    public const int ENERGY_EXPEND = -1;
    public const int ENERGY_EXPEND_IKIGIRE = -2;

    /** コンストラクタ */
    private void Start()
    {
        //フレームレート
        frameInterval = 1 / frameRate;

        //押下
        EventTrigger.Entry entry00 = new EventTrigger.Entry();
        entry00.eventID = EventTriggerType.PointerDown;
        entry00.callback.AddListener(call =>
        {
            processEvent(Main.KEY_PRESSED, KEY_SELECT);

            // ゲームオーバー中
            if ((game == 2))
            {
                if ((m_key & ((1 << Main.KEY_SELECT) | (1 << Main.KEY_5))) != 0)
                {
                    if (go_time == 0)
                    {
                        //タイトルへ
                        game = 0;
                    }
                }
            }

        });
        m_key_listener.triggers.Add(entry00);


        EventTrigger.Entry entry01 = new EventTrigger.Entry();
        entry01.eventID = EventTriggerType.PointerUp;
        entry01.callback.AddListener(call =>
        {
            processEvent(Main.KEY_RELEASED, KEY_SELECT);
        });
        m_key_listener.triggers.Add(entry01);

        // ムービークリップの作成
        // 親クリップ：swfRoot
        //mc_title = swfRoot.createEmptyMovieClip("mc_title", 10);
        mc_title = new GameObject("mc_title");
        mc_title.transform.SetParent(swfRoot);
        //mc_game = swfRoot.createEmptyMovieClip("mc_game", 9);
        //mc_game._visible = false;
        mc_game = new GameObject("mc_game");
        mc_game.transform.SetParent(swfRoot);
        mc_game.SetActive(false);

        // 親クリップ：mc_title
        // 背景
        //mc_title.attachMovie("title", "mc_title", 0);
        createGameObject("title", "mc_title", 0, mc_title.transform, 0, 0);
        // 親クリップ：mc_game
        // 背景
        //mc_game.attachMovie("game_bg", "mc_game", 0);
        createGameObject("game_bg", "mc_game", 0, mc_game.transform, 0, 0);

        // ベルト
        //mc_band_up = mc_game.attachMovie("band", "mc_band_up", 1, { _x: 214, _y: 192 } );
        //mc_band_down = mc_game.attachMovie("band", "mc_band_down", 2, { _x: 20, _y: 204 } );
        mc_band_up = createGameObject("band", "mc_band_up", 1, mc_game.transform, 214, 192);
        mc_band_down = createGameObject("band", "mc_band_down", 2, mc_game.transform, 20, 204);

        // ギア
        //mc_gear_l = mc_game.attachMovie("gear", "mc_gear_l", 3, { _x: 16, _y: 192 } );
        //mc_gear_r = mc_game.attachMovie("gear", "mc_gear_r", 4, { _x: 16, _y: 192 } );
        mc_gear_l = attachMovieClip("gear/gear", 3, mc_game.transform, 16, 192);
        mc_gear_r = attachMovieClip("gear/gear", 4, mc_game.transform, 16, 192);

        // ワイヤー
        //mc_wire[0] = mc_game.createEmptyMovieClip("mc_wire_00", 5);
        //mc_wire[1] = mc_game.createEmptyMovieClip("mc_wire_01", 6);
        //mc_wire[2] = mc_game.createEmptyMovieClip("mc_wire_02", 7);
        //mc_wire[3] = mc_game.createEmptyMovieClip("mc_wire_03", 8);

        // おもり
        //mc_weight[0] = mc_game.attachMovie("weight", "mc_weight_00", 9, { _x: 50, _y: 60 } );
        //mc_weight[1] = mc_game.attachMovie("weight", "mc_weight_01", 10, { _x: 96, _y: 60 } );
        //mc_weight[2] = mc_game.attachMovie("weight", "mc_weight_02", 11, { _x: 142, _y: 60 } );
        //mc_weight[3] = mc_game.attachMovie("weight", "mc_weight_03", 12, { _x: 188, _y: 60 } );
        mc_weight[0] = createGameObject("weight", "mc_weight_00", 9, mc_game.transform, 50, 60);
        mc_weight[1] = createGameObject("weight", "mc_weight_01", 10, mc_game.transform, 96, 60);
        mc_weight[2] = createGameObject("weight", "mc_weight_02", 11, mc_game.transform, 142, 60);
        mc_weight[3] = createGameObject("weight", "mc_weight_03", 12, mc_game.transform, 188, 60);
        // コブン
        //mc_kobun = attachMovie("kobun", "mc_kobun", 13, mc_game.transform, 188, 60);
        //mc_kobun = attachObject(kobunObject, 13, mc_game.transform, 188, 60);
        //kobunAnim = mc_kobun.GetComponent<Animator>();

        mc_player = attachObject(playerObject, 13, mc_game.transform, 188, 60);
        playerAnim = mc_player.GetComponent<Animator>();
        plState = 0;

        setKobunAnime(1);
        mc_energy = createGameObject("energy", "mc_energy", 14, mc_game.transform, 120, 220);
        energyGauge = mc_energy.GetComponent<SpriteRenderer>();
        // ゲームオーバー
        mc_game_over = createGameObject("game_over", "mc_game_over", 14, mc_game.transform, 28, 100, _visible: false);
        // READY
        mc_ready = createGameObject("ready", "mc_ready", 15, mc_game.transform, 49, 113, _visible: false);

        // スコア
        //mc_score[0] = mc_game.attachMovie("number", "mc_score_00", 17, { _x: (134 + 17 * 0), _y: 4 } );
        //mc_score[1] = mc_game.attachMovie("number", "mc_score_01", 18, { _x: (134 + 17 * 1), _y: 4 } );
        //mc_score[2] = mc_game.attachMovie("number", "mc_score_02", 19, { _x: (134 + 17 * 2), _y: 4 } );
        //mc_score[3] = mc_game.attachMovie("number", "mc_score_03", 20, { _x: (134 + 17 * 3), _y: 4 } );
        //mc_score[4] = mc_game.attachMovie("number", "mc_score_04", 21, { _x: (134 + 17 * 4), _y: 4 } );
        //mc_score[5] = mc_game.attachMovie("number", "mc_score_05", 22, { _x: (134 + 17 * 5), _y: 4 } );
    }

    // メインループ
    void Update()
    {

        if (frameInterval > frameWaitingTime)
        {
            frameWaitingTime += Time.deltaTime;
            return;
        }
        frameWaitingTime = 0;
        run();

        /*
        // debug:表示
        var my_fmt:TextFormat = new TextFormat();
        my_fmt.color = 0x000000;
        // FPS
        var time:Date = new Date();
        var process_time = time.getTime() - Main.end_time;
        process_time = Math.floor(1000 / process_time);
        swfRoot.fps.text = "" + process_time;
        swfRoot.fps.setTextFormat(my_fmt);
        Main.end_time = time.getTime();
        // ヒープ
        var stats = FSCommand2("GetFreePlayerMemory");
        swfRoot.heap.text = "" + stats + "KB";
        swfRoot.heap.setTextFormat(my_fmt);
        */
    }

    /**
	 * キーイベント処理
	 * [各キーコード]
	 * キー		 0	 1	 2	 3	  4	  5	  6	  7	  8	  9
	 * キーボード	48	49	50	51	 52	 53	 54	 55	 56	 57
	 * テンキー	96	97	98	99	100	101	102	103	104	105
	 * @param	type イベント種類
	 * @param	param キーパラメータ
	 */
    public void processEvent(int type, int param)
    {
        //Debug.Log("KEY=" + param);

        // キーリリース
        if (type == KEY_RELEASED)
        {
            m_hold_key_flg = 0;

            forward_hashiru = 0;
            running_time = 0;
            running_counter = 0;
            iki_pls = ENERGY_RECOVER;

        }
        // キープレス
        else if (type == KEY_PRESSED)
        {
            // キーを押し続けていると、何度もKEY_PRESSEDイベントが発生するため、
            // 最初の一回だけKEY_PRESSEDイベントを処理するための条件処理。
            if (m_hold_key_flg == 0)
            {
                if (param == 48 || param == 96) m_key = 1 << Main.KEY_0;
                //else if (param == 49 || param == 97) m_key = 1 << Main.KEY_1;
                //else if (param == 50 || param == 98) m_key = 1 << Main.KEY_2;
                //else if (param == 51 || param == 99) m_key = 1 << Main.KEY_3;
                //else if (param == 52 || param == 100) m_key = 1 << Main.KEY_4;
                //else if (param == 53 || param == 101) m_key = 1 << Main.KEY_5;
                //else if (param == 54 || param == 102) m_key = 1 << Main.KEY_6;
                //else if (param == 55 || param == 103) m_key = 1 << Main.KEY_7;
                //else if (param == 56 || param == 104) m_key = 1 << Main.KEY_8;
                //else if (param == 57 || param == 105) m_key = 1 << Main.KEY_9;
                //else if (param == Key.LEFT || param == 37) m_key = 1 << Main.KEY_ARROW;
                //else if (param == Key.UP || param == 38 || param == 9) m_key = 1 << Main.KEY_ARROW;
                //else if (param == Key.RIGHT || param == 37) m_key = 1 << Main.KEY_ARROW;
                //else if (param == Key.DOWN || param == 40 || param == 9) m_key = 1 << Main.KEY_ARROW;
                else if (param == 20) m_key = 1 << Main.KEY_SELECT;

            }

            m_hold_key_flg = 1;

            // ゲーム中
            if (game == 1)
            {
                if (ikigire_on == 0)
                {
                    forward_hashiru = HASHIRU_SPEED;
                    running_counter = RUNNING_COUNT;
                    iki_pls = ENERGY_EXPEND;
                }
                else
                {
                    forward_hashiru = 0;
                    running_time = 0;
                    running_counter = 0;
                    iki_pls = ENERGY_EXPEND_IKIGIRE;
                }
            }
            // デモ中
            else if ((game == 0))
            {
                if ((Main.m_key & ((1 << Main.KEY_SELECT) | (1 << Main.KEY_5))) != 0)
                {
                    game = 1;
                    st_time = 15;
                    score = 0;
                    //init_print = 0;
                    pl_x = 50;
                    pl_y = 80;
                    back_ugoku = -2;
                    cmbSpeedlevel = 0;
                    u_timer = 0;
                    forward_hashiru = 0;
                    tubure = 0;
                    running_time = 0;
                    running_counter = 0;
                    ikigire = 50;
                    ikigire_on = 0;
                    iki_pls = 0;
                    level = 0;
                    l_time = 0;
                    cmb_x = 107;
                    cmb_x2 = 10;
                    em_x[0] = 25;
                    em_x[1] = 48;
                    em_x[2] = 71;
                    em_x[3] = 94;
                    em_y[0] = 0;
                    em_y[1] = 0;
                    em_y[2] = 0;
                    em_y[3] = 0;
                    em_buru[0] = 0;
                    em_buru[1] = 0;
                    em_buru[2] = 0;
                    em_buru[3] = 0;
                    em_time[0] = 0;
                    em_time[1] = 0;
                    em_time[2] = 0;
                    em_time[3] = 15;

                    //mc_title._visible = false;
                    //mc_game._visible = true;
                    //mc_game_over._visible = false;
                    mc_title.SetActive(false);
                    mc_game.SetActive(true);
                    mc_game_over.SetActive(false);

                    //mc_gear_l.gotoAndStop(1);
                    //mc_gear_r.gotoAndStop(1);
                    plState = 0;
                    setKobunAnime(1);
                    //drawImage(mc_kobun, pl_x + gm_off, pl_y - 1);
                    drawImage(mc_player, pl_x + gm_off + plOffsetX, pl_y + plOffsetY - 1);
                    drawTotalScore();
                }
            }
        }
    }


    /** メインループ */
    void run()
    {
        // キーパラメータをセット
        var key_param = m_key;
        // キー状態の初期化
        m_key = 0;


        switch (game)
        {
            // タイトル
            case 0:
                {
                    //mc_title._visible = true;
                    //mc_game._visible = false;
                    mc_title.SetActive(true);
                    mc_game.SetActive(false);

                    break;
                }
            // ゲーム中
            case 1:
                {
                    //opl_x = pl_x;
                    //opl_y = pl_y;
                    //ocmb_x = cmb_x;
                    //ocmb_x2 = cmb_x2;
                    //for (ct = 0; ct < 4; ct++)
                    //{
                    //    oem_x[ct] = em_x[ct] + em_buru[ct];
                    //    oem_y[ct] = em_y[ct];
                    //}

                    if (st_time != 0)
                    {
                        // ゲームスタート時
                        st_time--;
                        if (st_time == 0)
                        {
                            //oto0 = 1;
                            setKobunAnime(2);
                            mc_gear_l.gotoAndPlay(1);
                            mc_gear_r.gotoAndPlay(1);
                            //mc_ready._visible = false;
                            mc_ready.SetActive(false);
                        }
                        else
                        {
                            plState = 0;
                            setKobunAnime(1);
                            //mc_ready._visible = true;
                            mc_ready.SetActive(true);
                        }
                    }
                    else
                    {
                        // ゲームレベル加算				
                        l_time++;
                        if (l_time == 100)
                        {
                            level++;
                            l_time = 0;
                        }
                        //	コンベア移動
                        cmb_x = cmb_x + back_ugoku;
                        if (cmb_x <= 9)
                        {
                            cmb_x = 107;
                        }
                        cmb_x2 = cmb_x2 - (back_ugoku);
                        if (cmb_x2 >= 108)
                        {
                            cmb_x2 = 10;
                        }
                        // 息切れチェック
                        ikigire = ikigire + iki_pls;
                        if (ikigire_on == 1)
                        {
                            if (ikigire >= IKIGIRE_LIMIT)
                            {
                                ikigire_on = 0;
                            }
                        }
                        if (ikigire <= 0)
                        {
                            ikigire = 0;
                            ikigire_on = 1;
                        }
                        if (ikigire >= 50)
                        {
                            ikigire = 50;
                        }
                        // プレイヤーパターン替えタイミング		
                        //char_p = (int)Mathf.Floor((char_p + 1) % 2);
                        //---------------------------------
                        // 重り 作動判定
                        //---------------------------------
                        em_move_counter = 0;
                        //おもり動作チェック
                        for (ct = 0; ct < 4; ct++)
                        {
                            if (em_time[ct] > 0)
                            {
                                em_move_counter++;
                            }
                        }
                        switch (level)
                        {
                            case int i when i == 0: //レベル０
                                is_em_move = (em_move_counter == 0);
                                break;
                            case int i when i <= 1://レベル１まで
                                is_em_move = (em_move_counter >= 1);
                                break;
                            default:    //レベル１以上
                                is_em_move = true;
                                break;
                        }
                        if (is_em_move)
                        {
                            for (ct = 0; ct < 4; ct++)
                            {
                                if (em_time[ct] == 0)
                                {
                                    //if ( 0 == ((int)(R.nextInt()%16))){
                                    if (0 == Mathf.Floor(Random.value * 100 % 16))
                                    {
                                        em_time[ct] = 20;
                                    }
                                }
                            }
                        }

                        //---------------------------------
                        //	重り 動作
                        //---------------------------------
                        for (ct = 0; ct < 4; ct++)
                        {
                            if (em_time[ct] > 0)
                            {
                                em_time[ct]--;

                                if (em_time[ct] >= 16)
                                {
                                    //落下予兆
                                    em_buru[ct] = (em_buru[ct] + 1) % 2;
                                }
                                else if (em_time[ct] >= 12)
                                {
                                    //落下中
                                    em_buru[ct] = 0;
                                    em_y[ct] = em_y[ct] + 10;
                                }
                                else if (em_time[ct] >= 7)
                                {
                                    //落下済み
                                    em_y[ct] = 42;
                                }
                                else if (em_time[ct] > 0)
                                {
                                    //巻き上げ
                                    em_y[ct] = em_y[ct] - 6;
                                }
                                else
                                {
                                    //停止
                                    em_y[ct] = 0;
                                }
                                //画面演出
                                if (em_time[ct] == 11)
                                {
                                    // ドシンと画面ゆらす
                                    mc_game.transform.position = new Vector2(mc_game.transform.position.x, -2);
                                }
                                else if (em_time[ct] == 10)
                                {
                                    mc_game.transform.position = new Vector2(mc_game.transform.position.x, 0);
                                }
                            }
                        }

                        //	コンベア スピード変化					
                        if (cmbSpeedlevel == 0)
                        {
                            if (level > 2)
                            {
                                //clc = ((int)(R.nextInt()%16));
                                clc = (int)Mathf.Floor((Random.value * 100) % 16);
                            }
                            else
                            {
                                //clc = ((int)(R.nextInt()%64));
                                clc = (int)Mathf.Floor((Random.value * 100) % 64);
                            }
                            if (0 == clc)
                            {
                                //コンベア高速
                                back_ugoku = -4;
                                u_timer = 20;
                                cmbSpeedlevel = 1;

                                mc_gear_l.gotoAndPlay(4);
                                mc_gear_r.gotoAndPlay(4);
                            }
                        }
                        if (u_timer > 0)
                        {
                            u_timer--;
                        }
                        if (u_timer == 0)
                        {
                            cmbSpeedlevel = 0;
                            mc_gear_l.gotoAndPlay(1);
                            mc_gear_r.gotoAndPlay(1);
                            back_ugoku = -2;
                        }
                        //------------------------------------------
                        //	プレイヤー移動
                        //------------------------------------------
                        if (ikigire_on == 1)
                        {
                            forward_hashiru = 0;
                            running_time = 0;
                            running_counter = 0;
                        }
                        running_time = running_time + running_counter;
                        if (running_time >= 8)
                        {
                            forward_hashiru = HASHIRU_SPEED;
                        }
                        //プレイヤーのポジション計算
                        pl_x = pl_x + back_ugoku + forward_hashiru;
                        //死亡（串刺し）
                        if (pl_x <= 19)
                        {
                            go_time = 15;
                            game = 2;
                        }
                        //前方の壁にぶち当たる状態
                        if (pl_x >= 98)
                        {
                            pl_x = 98;
                        }
                        //	当り判定	
                        for (ct = 0; ct < 4; ct++)
                        {
                            if ((em_time[ct] == 12))
                            {
                                if (pl_x < 19)
                                {
                                    clc = 0;
                                }
                                else
                                {
                                    clc = (int)Mathf.Floor((pl_x - 19) / 22);
                                }
                                if (clc >= 4)
                                {
                                    clc = 3;
                                }
                                if (clc == ct)
                                {
                                    clc = (int)Mathf.Floor(((pl_x - 14) / (16 * (ct + 1))));
                                    if ((2 >= clc) && (-2 <= clc))
                                    {
                                        go_time = 15;
                                        game = 2;
                                        tubure = 1;
                                    }
                                }
                            }
                        }
                        // スコア加算
                        score = score + (int)Mathf.Floor(ikigire / 5);
                        //debug
                        //score = 888888;
                        if (score > 999999)
                        {
                            score = 999999;
                        }
                    }

                    /***************/
                    //   表示
                    /***************/
                    // 初期表示
                    //init_print = 1;
                    // 重り
                    for (ct = 0; ct < 4; ct++)
                    {
                        //drawLine(mc_wire[ct], 0xffffff, em_x[ct] + 8 + gm_off, 22, em_x[ct] + 8 + gm_off, em_y[ct] + 30);
                        drawImage(mc_weight[ct], em_x[ct] + em_buru[ct] + gm_off, em_y[ct] + 30);
                    }
                    // プレイヤー
                    if (game == 2)
                    {
                        if (tubure == 0)
                        {
                            plState = 4;
                            //drawImage(g,img[3],pl_x+gm_off,pl_y);
                            setKobunAnime(4);
                            //drawImage(mc_kobun, pl_x + gm_off, pl_y - 1);
                            drawImage(mc_player, pl_x + gm_off + plOffsetX, pl_y + plOffsetY - 1);
                        }
                        else
                        {
                            plState = 5;
                            //drawImage(g,img[12],pl_x+gm_off,pl_y+5);
                            setKobunAnime(5);
                            //drawImage(mc_kobun, pl_x + gm_off, pl_y + 5);
                            drawImage(mc_player, pl_x + gm_off + plOffsetX, pl_y + plOffsetY + 5);
                        }

                        // ギア停止
                        mc_gear_l.stop();
                        mc_gear_r.stop();
                    }
                    else
                    {
                        if (st_time != 0)
                        {
                            plState = 0;
                            setKobunAnime(1);
                        }
                        else if (forward_hashiru == HASHIRU_SPEED)
                        {
                            //drawImage(g,img[3+char_p],pl_x+gm_off,pl_y);
                            plState = 1;
                            setKobunAnime(3);
                            //drawImage(mc_kobun, pl_x + gm_off, pl_y - 1);
                            drawImage(mc_player, pl_x + gm_off + plOffsetX, pl_y + plOffsetY - 1);
                        }
                        else
                        {
                            plState = 0;
                            //drawImage(g,img[1+char_p],pl_x+gm_off,pl_y);
                            setKobunAnime(2);
                            //drawImage(mc_kobun, pl_x + gm_off, pl_y - 1);
                            drawImage(mc_player, pl_x + gm_off + plOffsetX, pl_y + plOffsetY - 1);
                        }
                    }

                    // 歯車
                    drawImage(mc_band_up, cmb_x + gm_off, 96);
                    drawImage(mc_band_down, cmb_x2 + gm_off, 102);

                    if (u_timer == 0)
                    {
                        drawImage(mc_gear_l, 8 + gm_off, 96);
                        drawImage(mc_gear_r, 106 + gm_off, 96);
                    }
                    else
                    {
                        drawImage(mc_gear_l, 8 + gm_off, 96);
                        drawImage(mc_gear_r, 106 + gm_off, 96);
                    }
                    // 息切れメーター
                    if (ikigire >= 20)
                    {
                        fillEnergyGauge(mc_energy, 0x0000ff, 0 + 60 + gm_off, 110, ikigire, 7);
                    }
                    else
                    {
                        fillEnergyGauge(mc_energy, 0xff0000, 0 + 60 + gm_off, 110, ikigire, 7);
                    }
                    // スコア
                    drawTotalScore();

                    break;
                }
            case 2:
                {
                    //	ゲームオーバー時				
                    //	ゲームオーバー表示
                    if (go_time != 0) go_time--;

                    if (go_time == 11)
                    {
                        //oto1 = 1;
                    }
                    else if (go_time == 10)
                    {
                        //mc_game_over._visible = true;
                        mc_game_over.SetActive(true);
                        go_time = 0;
                    }
                    break;
                }
            default:
                {
                    //trace("game error = " + game);
                    break;
                }
        }

    }


    /** ルーチン番号のセット */
    public static void setRoutineNo(int set_r0)
    {
        setRoutineNo2(set_r0, 0);
    }
    public static void setRoutineNo2(int set_r0, int set_r1)
    {
        m_r0 = set_r0;
        m_r1 = set_r1;
    }

    public static void drawImage(MovieClip im, int x, int y)
    {
        drawImage(im.gameObject, x, y);
    }

    /** イメージの描画 */
    public static void drawImage(GameObject im, int x, int y)
    {
        x = x << 2;
        y = y << 2;

        //if (im._visible == false) im._visible = true;
        if (!im.activeInHierarchy) im.SetActive(true);
        //im._x = x;
        //im._y = y;
        im.transform.localPosition = new Vector3(x, -y);
    }
    /**　矩形の塗りつぶし */
    public void fillEnergyGauge(GameObject g, int color, int x, int y, int w, int h)
    {
        x = x << 2;
        y = y << 2;
        w = w << 2;
        h = h << 2;

        //if (g._visible == false) g._visible = true;
        //g.clear();
        //g.beginFill(color);
        //g.moveTo(x, y);
        //g.lineTo(x + w, y);
        //g.lineTo(x + w, y + h);
        //g.lineTo(x, y + h);
        //g.endFill();
        energyGauge.transform.localScale = new Vector3(w / 200f, energyGauge.transform.localScale.y);
    }
    /** 線の描画 */
    //public static void drawLine(g:MovieClip, color: Number, x: Number, y: Number, x0: Number, y0: Number)
    //{
    //    x = x << 1;
    //    y = y << 1;
    //    x0 = x0 << 1;
    //    y0 = y0 << 1;

    //    if (g._visible == false) g._visible = true;
    //    g.clear();
    //    g.lineStyle(2, color, 100);
    //    g.moveTo(x, y + 1);
    //    g.lineTo(x0, y0);
    //}


    /**
     * コブンのムービークリップのフレームをセットする。（アニメーションさせる。）
     * @param	ani_no フレームNo
     */
    public void setKobunAnime(int ani_no)
    {
        if (m_kobun_ani != ani_no)
        {
            m_kobun_ani = ani_no;
            //mc_kobun.gotoAndPlay(ani_no);
            //kobunAnim.SetInteger("aniNo", ani_no);
            playerAnim.SetInteger("plState", plState);
        }
    }


    /**
     * 合計スコア表示を更新する。
     */
    public void drawTotalScore()
    {
        mc_score.text = score.ToString();
    }

    private MovieClip attachMovieClip(string path, int order, Transform parent, int x, int y, bool _visible = true)
    {
        var prefab = Resources.Load<GameObject>(path);
        var clip = Instantiate(prefab, parent);
        clip.transform.localPosition = new Vector2(x, -y);
        clip.SetActive(_visible);

        var movie = clip.GetComponent<MovieClip>();
        movie.SortingOrder = order;

        return movie;
    }


    private static GameObject createGameObject(string spriteName, string objectName, int order, Transform parent, int x, int y, bool _visible = true)
    {
        x = x << 1;
        y = y << 1;

        var obj = new GameObject(objectName);
        obj.transform.SetParent(parent);
        obj.transform.localPosition = new Vector2(x, -y);
        obj.SetActive(_visible);
        var sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>(spriteName);
        sr.sortingOrder = order;

        return obj;
    }

    private GameObject attachObject(GameObject objectName, int order, Transform parent, int x, int y, bool _visible = true)
    {
        x = x << 1;
        y = y << 1;

        var obj = Instantiate(objectName, parent);
        obj.transform.localPosition = new Vector2(x, -y);
        obj.SetActive(_visible);
        var sr = obj.GetComponent<SpriteRenderer>();
        sr.sortingOrder = order;

        return obj;
    }
}
