using UnityEngine;

public class GameCube : MonoBehaviour
{
    public Vector3[] CubeSidesEulers; // ������ � ������� ������ ��� ������ ������� ������

    public int ThrowCube()
    {
        ShowCube();                                                    // ���������� �����
        int randomCubeValue = Random.Range(0, CubeSidesEulers.Length); // ����� ��������� �������� ��� ������ ������� ������
        RotateCube(CubeSidesEulers[randomCubeValue]);                  // ������������ ����� � ����� ���� ��� ��������� �������
        return randomCubeValue + 1;                     // ���������� � ������ �������� ������ + 1
    }

    public void ShowCube()
    {
        SetActiveCube(true); // ������ ����� �������
    }

    public void HideCube()
    {
        SetActiveCube(false);  // ������ ����� ���������
    }

    private void SetActiveCube(bool value)
    {
        gameObject.SetActive(value);  // �������� ��� ��������� ����������� ������ � ����������� �� ����������� ��������
    }

    private void RotateCube(Vector3 cubeEuler)
    {
        transform.eulerAngles = cubeEuler;  // ������������� ���� �������� ������
    }
}