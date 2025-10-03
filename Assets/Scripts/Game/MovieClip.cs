using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class MovieClip : MonoBehaviour
{
    /// <summary>
    /// アニメーター
    /// </summary>
    public Animator animator = null;

    public int SortingOrder
    {
        get { return spriteRenderer.sortingOrder; }
        set { spriteRenderer.sortingOrder = value; }
    }
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void stop()
    {
        animator.speed = 0;
    }

    public virtual void gotoAndPlay(int param)
    {
        animator.speed = 1;
    }
}
