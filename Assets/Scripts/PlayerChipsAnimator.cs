using UnityEngine;

public class PlayerChipsAnimator : MonoBehaviour
{
    public GameStateChanger GameStateChanger;  // ������ ��������� ��������� ����
    public GameField GameField;                // ������ �������� ����
    public float CellMoveDuration = 0.3f;      // ������������ ����������� ����� ��������

    private PlayerChip _playerChip;           // ������ ����� �������� ������
    private bool isAnimatedNow;          // ����, ������� ���������, ����������� �� ������ ��������
    private int[] _movementCells;        // ������ �����, �� ������� ����� �������������
    private int _currentCellId;        // ������ ������� ������, ������� ���������
    private float _cellMoveTimer;        // ��������� ������� ��� ��������
    private Vector2 _startPosition;        // ��������� ������� �����������
    private Vector2 _endPosition;          // �������� ������� �����������

    // Update is called once per frame
  void Update() { Animation(); }

  public void AnimateChipMovement(PlayerChip playerChip, int[] movementCells)
  {
      _playerChip = playerChip;       // ��������� ���������� ����� � ����������
      _movementCells = movementCells; // �������� ������ �����, ����� ������� ����� ������ ������
      isAnimatedNow = true;            // ����� ����, ������� ���������, ��� �������� ��� ������
      _currentCellId = -1;            // ������������� ��������� �������� ������� ������
      ToNextCell();                   // �������� �������� � ��������� ������
  }

  private void Animation()
  {
      if (!isAnimatedNow) { return; }

      chipArrivedToNextCell();

      _playerChip.SetPosition(Vector2.Lerp(_startPosition, _endPosition, _cellMoveTimer));    // ��������� ������������� ������� ����� ����� ��������� � �������� ��������
      _cellMoveTimer += Time.deltaTime / CellMoveDuration;                                    // ����������� ������ �� ������ ���������� �������
  }

  private void ToNextCell() {
      _currentCellId++;     
      if (_currentCellId >= _movementCells.Length - 1) {
        EndAnimation();     // ��������� ��������
        return;             // ������� �� ������
      }
      newStartAndEndPosition();
      _cellMoveTimer = 0;   // ���������� ������ ����������� �� 0
  }

  private void EndAnimation() {
      isAnimatedNow = false;                               
      GameStateChanger.ContinueGameAfterChipAnimation(); 
  }

  private void chipArrivedToNextCell() {
      if (_cellMoveTimer >= 1) { ToNextCell(); }
  }
    

  private void newStartAndEndPosition()
  {
      _startPosition = GameField.GetCellPosition(_movementCells[_currentCellId]);   
      _endPosition   = GameField.GetCellPosition(_movementCells[_currentCellId + 1]); // �������� �������� ������� ��� �������� �� ������ GameField � ������� ���������� ������ ������
  }

  /*private void animationEnded()
  {
    if (_currentCellId >= _movementCells.Length - 1)
    {
      EndAnimation();     // ��������� ��������
      return;             // ������� �� ������
    }
  }*/
}
