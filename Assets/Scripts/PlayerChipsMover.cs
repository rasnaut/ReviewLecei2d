using UnityEngine;

public class PlayerChipsMover : MonoBehaviour
{
    public GameField GameField;                        // ������ �������� ����
    public TransitionSettings TransitionSettings;      // ������ � ����������� ���������
    public PlayerChipsAnimator PlayersChipsAnimator;  // ������ �������� �������� �����

    private PlayerChip[] _playersChips;   // ������ ����� �������
    private int[] _playersChipsCellsIds;  // ������ ������� ������� �����

    public void StartGame(PlayerChip[] playersChips)
    {
        _playersChips = playersChips;                           // ����������� ������ ����� �������
        _playersChipsCellsIds = new int[playersChips.Length];   // ������ ������ ��� �������� ������� ������� �����
        RefreshChipsPositions();
    }

    public void RefreshChipsPositions()
    {
        // �������� � ����� �� ���� ������ �������
        for (int i = 0; i < _playersChips.Length; i++)
        {
            RefreshChipPosition(i);   // �������� ����� ��� ���������� ������� ����� � ������� i
        }
    }

    public void MoveChip(int playerId, int steps)
    {
        int startCellId = _playersChipsCellsIds[playerId];     // ��������� ������� ������� �����
        _playersChipsCellsIds[playerId] += steps;              // ����������� ������� ������� ����� �� �������� ����� �����

        // ���� ������� ������� ����� ��������� ���������� ����� �� ������� ����
        if (_playersChipsCellsIds[playerId] >= GameField.CellsCount)
        {
            _playersChipsCellsIds[playerId] = GameField.CellsCount - 1;   // ������������� ����� �� ��������� ������
        }

        int lastCellId = _playersChipsCellsIds[playerId];            // ��������� ����� ������� �����

        TryApplyTransition(playerId);                                // ��������� ����� ������� �����
        int afterTransitionCellId = _playersChipsCellsIds[playerId]; // ��������� ������� ����� ����� ���������� ��������

        int[] movementCells = GetMovementCells(startCellId, lastCellId, afterTransitionCellId);  // �������� ������ �����, �� ������� ����� �����

        PlayersChipsAnimator.AnimateChipMovement(_playersChips[playerId], movementCells);   // ��������� �������� �������� �����
                                                                                            //RefreshChipPosition(playerId);    // �������� ����� ��� ���������� ������� �����
    }

    private int[] GetMovementCells(int startCellId, int lastCellId, int afterTransitionCellId)
    {
        int cellsCount = lastCellId - startCellId + 1;            // ��������� ���������� �����, ������� ������ �������� �����
        bool hasTransition = lastCellId != afterTransitionCellId;     // ���������, ���� �� ������� �� ���� ��� �������� � ���������� ��������� ������ � ������ ����� ��������

        // ���� ���� ������� �� ���� ��� ��������
        if (hasTransition) { cellsCount++; } // ����������� ���������� ����� �� 1

        int[] movementCells = new int[cellsCount];      // ������ ������ � ��������� ����������� �����

        // �������� �� ����� ������� �����
        for (int i = 0; i < movementCells.Length; i++)
        {
            // ���� ��� ��������� ������ � �� ��� ���� �������
            if (i == movementCells.Length - 1 && hasTransition) { movementCells[i] = afterTransitionCellId; } // ���������� ����� ������ ����� ��������
            else { movementCells[i] = startCellId + i; }       // ���������� ����� ������� ������
        }

        return movementCells;     // ���������� ������ � �������� ����� ��� ��������
    }

    private void RefreshChipPosition(int playerId)
    {
        Vector2 chipPosition = GameField.GetCellPosition(_playersChipsCellsIds[playerId]);    // �������� ������� ������ �� ������� ����, ������� ������������� ������� ������� �����
        _playersChips[playerId].SetPosition(chipPosition);                                    // ������������� ����� �� ���������� �������
    }

    private void TryApplyTransition(int playerId)
    {
        // �������� ����� ����� ������ ����� ���� ������
        int resultCellId = TransitionSettings.GetTransitionResultCellId(_playersChipsCellsIds[playerId]);

        // ���� ����� ������ 0
        if (resultCellId < 0)
        {
            return;  // ������� �� ���� ��� �������� �� �����
        }
        _playersChipsCellsIds[playerId] = resultCellId;  // ������������� ����� ����� ������
    }

    public bool CheckPlayerFinished(int playerId)
    {
        // ���������� true, ���� ����� ������� ������ ���������� ������ ������ ��� ����� ���������� ������ �� ������� ���� - 1
        return _playersChipsCellsIds[playerId] >= GameField.CellsCount - 1;
    }
}