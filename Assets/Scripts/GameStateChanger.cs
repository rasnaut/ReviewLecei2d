using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameStateChanger : MonoBehaviour
{
    public int PlayersCount = 2;                     // Количество игроков

    public PlayerChipsCreator PlayersChipsCreator;   // Скрипт создания фишек
    public PlayersTurnChanger PlayersTurnChanger;    // Скрипт смены хода игроков
    public PlayerChipsMover PlayersChipsMover;       // Скрипт перемещения фишек
    public GameField GameField;                      // Скрипт игрового поля
    public Button ThrowButton;              // Кнопка бросков кубика

    private void Start() { FirstStartGame(); }
    // Метод для первого запуска игры 
    private void FirstStartGame() {
        GameField.FillCellsPositions(); // Заполняем позиции клеток на игровом поле
        StartGame();                    // Начинаем новую игру 
    }

    private void StartGame()
    {
        PlayerChip[] playersChips = PlayersChipsCreator.SpawnPlayersChips(PlayersCount);  // Создаём фишки для заданного числа игроков

        PlayersTurnChanger.StartGame(playersChips);     // Готовим игроков к началу игры
        PlayersChipsMover.StartGame(playersChips);      // Задаём начальную позицию фишек игроков
        //SetScreens(true);                               // Показываем экран игры
    }

    // Метод для завершения игры
    private void EndGame() {
        //SetScreens(false);    // Показываем экран конца игры
    }

    // Метод для перезапуска по кнопке
    public void RestartGame()
    {
        //PlayersChipsCreator.Clear();      // Удаляем фишки игроков
        StartGame();                      // Начинаем новую игру
    }
  
    public void DoPlayerTurn(int steps)
    {
      Debug.Log("DoPlayerTurn");
      int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();      // Получаем номер текущего игрока
      PlayersChipsMover.MoveChip(currentPlayerId, steps);                 // Двигаем фишку текущего игрока на заданное число шагов
    }

    private void SetThrowButtonInteractable(bool value) {
        ThrowButton.interactable = value;     
    }
    public void ContinueGameAfterChipAnimation()
    {
        int currentPlayerId = PlayersTurnChanger.GetCurrentPlayerId();                // Получаем номер текущего игрока
        bool isPlayerFinished = PlayersChipsMover.CheckPlayerFinished(currentPlayerId); // Определяем, достиг ли игрок финиша

        // Если игрок на финише
        if (isPlayerFinished) {
            //SetWinText(currentPlayerId);          // Устанавливаем надпись о победе
            EndGame();                            // Переходим к экрану конца игры
        }
        else
        {
            PlayersTurnChanger.MovePlayerTurn();  // Передаём ход следующему игроку
            SetThrowButtonInteractable(true);     // Разрешаем ему бросить кубик
        }
    }
}
