using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateChanger : MonoBehaviour
{
    public int PlayersCount = 2;                     // ���������� �������

    public PlayerChipsCreator PlayersChipsCreator;   // ������ �������� �����
    public PlayersTurnChanger PlayersTurnChanger;    // ������ ����� ���� �������
    public PlayerChipsMover PlayersChipsMover;       // ������ ����������� �����
    public GameField GameField;                      // ������ �������� ����
    public Button ThrowButton;              // ������ ������� ������

    private void Start() { FirstStartGame(); }
    // ����� ��� ������� ������� ���� 
    private void FirstStartGame() {
        GameField.FillCellsPositions(); // ��������� ������� ������ �� ������� ����
        StartGame();                    // �������� ����� ���� 
    }

    private void StartGame()
    {
        PlayerChip[] playersChips = PlayersChipsCreator.SpawnPlayersChips(PlayersCount);  // ������ ����� ��� ��������� ����� �������

        PlayersTurnChanger.StartGame(playersChips);     // ������� ������� � ������ ����
        PlayersChipsMover.StartGame(playersChips);      // ����� ��������� ������� ����� �������
        //SetScreens(true);                               // ���������� ����� ����
    }

    // ����� ��� ���������� ����
    private void EndGame() {
        //SetScreens(false);    // ���������� ����� ����� ����
    }

    // ����� ��� ����������� �� ������
    public void RestartGame()
    {
        //PlayersChipsCreator.Clear();      // ������� ����� �������
        StartGame();                      // �������� ����� ����
    }
  
    public void DoPlayerTurn(int steps)
    {
      Debug.Log("DoPlayerTurn");
      int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();      // �������� ����� �������� ������
      PlayersChipsMover.MoveChip(currentPlayerId, steps);                 // ������� ����� �������� ������ �� �������� ����� �����
    }

    private void SetThrowButtonInteractable(bool value) {
        ThrowButton.interactable = value;     
    }
    public void ContinueGameAfterChipAnimation()
    {
        int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();                // �������� ����� �������� ������
        bool isPlayerFinished = PlayersChipsMover.CheckPlayerFinished(currentPlayerId); // ����������, ������ �� ����� ������

        // ���� ����� �� ������
        if (isPlayerFinished) {
            //SetWinText(currentPlayerId);          // ������������� ������� � ������
            EndGame();                            // ��������� � ������ ����� ����
        }
        else
        {
            PlayersTurnChanger.MovePlayerTurn();  // ������� ��� ���������� ������
            SetThrowButtonInteractable(true);     // ��������� ��� ������� �����
        }
    }
}
