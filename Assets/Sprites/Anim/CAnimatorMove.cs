using UnityEngine;

public class CAnimatorMove : MonoBehaviour
{
    public enum EAnimDirection
    {
        Down,
        Left,
        Right,
        Up
    }

    [SerializeField] private Animator Animator;
    [SerializeField] private EAnimDirection AnimDirection;


    public void AnimUpdate(Vector2 direction)
    {
        EAnimDirection animdir;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            animdir = EAnimDirection.Left;
            if (direction.x > 0) animdir = EAnimDirection.Right;
        } else {
            animdir = EAnimDirection.Down;
            if (direction.y > 0) animdir = EAnimDirection.Up;
        }
        Switch(animdir);
    }


    public void Switch(EAnimDirection animdirection)
    {
        if (AnimDirection != animdirection) Play(animdirection);
    }

    public void Play(EAnimDirection animdirection)
    {
        AnimDirection = animdirection;
        switch (AnimDirection)
        {
            case EAnimDirection.Down: Animator.Play("Down"); break;
            case EAnimDirection.Left: Animator.Play("Left"); break;
            case EAnimDirection.Right: Animator.Play("Right"); break;
            case EAnimDirection.Up: Animator.Play("Up"); break;
        }
    }

}