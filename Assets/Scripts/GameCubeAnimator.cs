using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCubeAnimator : MonoBehaviour
{
    private GameCubeThrower GameCubeThrower;        // ������ ������� ������
    private Animation CubeAnimation = null;         // ������ �� ��������� �������� ������

    void Start()
    {
        CubeAnimation = GetComponent<Animation>();
    }
    // ����������� ��������
    public void PlayAnimation(GameCubeThrower gameCubeThrower)
    {
        GameCubeThrower = gameCubeThrower;
        CubeAnimation.Play();
    }

    // ���� ����� �� ������� ����� ������ ��������
    public void OnAnimationEnd() { GameCubeThrower.ContinueAfterCubeAnimation(); }  // ���������� �������� ����� ��������

    public AnimationClip GetAnimationClip() { return CubeAnimation.clip; }
    public void SetAnimationClip(AnimationClip animationClip) { CubeAnimation.clip = animationClip; }
    public Transform GetTransform() { return transform; }
}
