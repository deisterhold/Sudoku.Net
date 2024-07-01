using System.Collections.ObjectModel;

namespace Sudoku.Net.Core;

public class GameBoard
{
    public const int Width = 9;
    public const int Height = 9;
    private readonly ReadOnlyCollection<int> _values = new ReadOnlyCollection<int>([1, 2, 3, 4, 5, 6, 7, 8, 9]);
    private readonly int?[][] _board = new int?[Height][]
    {
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
        new int?[Width]{null, null, null, null, null, null, null, null, null},
    };

    public GameBoard() { }

    public GameBoard(int?[][] board)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(board.Length, nameof(board));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(board.Length, Height, nameof(board));

        var y = 0;
        foreach(var row in board)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(row.Length, nameof(board));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(row.Length, Width, nameof(board));
            var x = 0;
            foreach(var col in row)
            {
                Set(x, y, board[y][x]);
                x++;
            }
            y++;
        }
    }

    public void Set(int x, int y, int? value)
    {
        _board[y][x] = value;
    }

    public int? Get(int x, int y)
    {
        return _board[y][x];
    }

    internal IEnumerable<int> GetValuesInRow(int row)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Height);

        for (var col = 0; col < Width; col++)
        {
            if (Get(col, row) is int value) yield return value;
        }
    }

    internal IEnumerable<int> GetValuesInCol(int col)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(col);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(col, Width);

        for (var row = 0; row < Height; row++)
        {
            if (Get(col, row) is int value) yield return value;
        }
    }

    internal IEnumerable<int> GetValuesInBox(int boxCol, int boxRow)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(boxCol);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(boxCol, 3);
        ArgumentOutOfRangeException.ThrowIfNegative(boxRow);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(boxRow, 3);

        var minRow = boxRow * 3;
        var maxRow = (boxRow * 3) + 3;
        var minCol = boxCol * 3;
        var maxCol = (boxCol * 3) + 3;

        for (var row = minRow; row < maxRow; row++)
        for (var col = minCol; col < maxCol; col++)
        {
            if (Get(col, row) is int value) yield return value;
        }
    }

    internal IEnumerable<int> GetPossibleValuesForRow(int row)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Height);

        return _values.Except(GetValuesInRow(row));
    }

    internal IEnumerable<int> GetPossibleValuesForCol(int col)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(col);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(col, Width);

        return _values.Except(GetValuesInCol(col));
    }

    internal IEnumerable<int> GetPossibleValuesForBox(int boxCol, int boxRow)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(boxCol);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(boxCol, 3);
        ArgumentOutOfRangeException.ThrowIfNegative(boxRow);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(boxRow, 3);

        return _values.Except(GetValuesInBox(boxCol, boxRow));
    }

    public IEnumerable<int> GetPossibleValuesForCell(int col, int row)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Height);
        ArgumentOutOfRangeException.ThrowIfNegative(col);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(col, Width);

        return _values
                .Except(GetValuesInRow(row))
                .Except(GetValuesInCol(col))
                .Except(GetValuesInBox(row % 3, col % 3));
    }

    internal bool IsValidRow(int row)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(row);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, Height);

        var values = new HashSet<int>(_values);

        for (var col = 0; col < Width; col++)
        {
            if (Get(col, row) is int value)
            {
                if (values.Contains(value))
                {
                    values.Remove(value);
                    continue;
                }
                else
                {
                    // Duplicate
                    return false;
                }
            }
        }

        return true;
    }

    internal bool IsValidCol(int col)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(col);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(col, Width);

        var values = new HashSet<int>(_values);

        for (var row = 0; row < Height; row++)
        {
            if (Get(col, row) is int value)
            {
                if (values.Contains(value))
                {
                    values.Remove(value);
                    continue;
                }
                else
                {
                    // Duplicate
                    return false;
                }
            }
        }

        return true;
    }

    internal bool IsValidBox(int boxCol, int boxRow)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(boxCol);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(boxCol, 3);
        ArgumentOutOfRangeException.ThrowIfNegative(boxRow);
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(boxRow, 3);

        var values = new HashSet<int>(_values);

        foreach (var value in GetValuesInBox(boxCol, boxRow))
        {
            if (values.Contains(value))
            {
                values.Remove(value);
                continue;
            }
            else
            {
                // Duplicate
                return false;
            }
        }

        return true;
    }

    public bool IsValid()
    {
        for (var col = 0; col < Width; col++)
        {
            if (!IsValidCol(col)) return false;
        }

        for (var row = 0; row < Height; row++)
        {
            if (!IsValidRow(row)) return false;
        }

        for (var boxRow = 0; boxRow < 3; boxRow++)
        for (var boxCol = 0; boxCol < 3; boxCol++)
        {
            if (!IsValidBox(boxCol, boxRow)) return false;
        }

        return true;
    }

    public bool IsFilled()
    {
        for (var row = 0; row < Height; row++)
        for (var col = 0; col < Width; col++)
        {
            if (Get(col, row) == null) return false;
        }

        return true;
    }

    public bool IsComplete()
    {
        return IsFilled() && IsValid();
    }
}
