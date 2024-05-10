using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCubeThrower : MonoBehaviour
{
    public GameStateChanger GameStateChanger;  // ������ ��������� ��������� ����
    public GameCube GameCubePrefab;    // ������ ��� �������� ������
    private GameCubeAnimator CubeThrowAnimator; // ������ �������� ������

    private int _cubeValue;    // ��������, ������� ������ �� ������
    private GameCube _gameCube;     // ��������� ������ ������

    void Start()
    {
        CubeThrowAnimator = GetComponent<GameCubeAnimator>();
        CreateGameCube();   // ������ ����� ����� ��� ������� ����
    }

    private void CreateGameCube()
    {
        Transform GameCubePoint = CubeThrowAnimator.GetTransform();
        _gameCube = Instantiate(GameCubePrefab, GameCubePoint.position, GameCubePoint.rotation, GameCubePoint);    // ������ ����� ����� � ��������� ������� � � ��������� ����� ��������
        _gameCube.HideCube();                                                                                      // �������� �����, ����� ��� �� ���� ����� � ������ ����
    }

    public void ThrowCube()
    {
        _cubeValue = _gameCube.ThrowCube();       // �������� ��������� �������� ������ ������
        CubeThrowAnimator.PlayAnimation(this);    // ����������� �������� ������
    }

    public void ContinueAfterCubeAnimation()
    {
        GameStateChanger.DoPlayerTurn(_cubeValue);      // ������� ��������, ������� ������ �� ������, � ������ ��������� ��������� ����
    }
}
