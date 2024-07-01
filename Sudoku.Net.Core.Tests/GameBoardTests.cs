namespace Sudoku.Net.Core.Tests;

[TestClass, TestCategory("Unit")]
public class GameBoardTests
{
    [TestMethod]
    public void GetValuesInRowReturnsValuesInRow()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(0, 0, 4);
        board.Set(1, 0, 5);
        board.Set(2, 0, 6);

        // Act
        var values = board.GetValuesInRow(0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 4);
        CollectionAssert.Contains(values.ToList(), 5);
        CollectionAssert.Contains(values.ToList(), 6);
    }

    [TestMethod]
    public void GetValuesInColReturnsValuesInCol()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(0, 0, 4);
        board.Set(0, 1, 5);
        board.Set(0, 2, 6);

        // Act
        var values = board.GetValuesInCol(0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 4);
        CollectionAssert.Contains(values.ToList(), 5);
        CollectionAssert.Contains(values.ToList(), 6);
    }

    [TestMethod]
    public void GetValuesInBoxReturnsValuesInBox()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(0, 0, 1);
        board.Set(1, 0, 2);
        board.Set(2, 0, 3);
        board.Set(0, 1, 4);
        board.Set(1, 1, 5);
        board.Set(2, 1, 6);

        // Act
        var values = board.GetValuesInBox(0, 0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 1);
        CollectionAssert.Contains(values.ToList(), 2);
        CollectionAssert.Contains(values.ToList(), 3);
        CollectionAssert.Contains(values.ToList(), 4);
        CollectionAssert.Contains(values.ToList(), 5);
        CollectionAssert.Contains(values.ToList(), 6);
    }

    [TestMethod]
    public void GetPossibleValuesForRowReturnsValuesNotInRow()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(0, 0, 4);
        board.Set(1, 0, 5);
        board.Set(2, 0, 6);

        // Act
        var values = board.GetPossibleValuesForRow(0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 1);
        CollectionAssert.Contains(values.ToList(), 2);
        CollectionAssert.Contains(values.ToList(), 3);
        CollectionAssert.Contains(values.ToList(), 7);
        CollectionAssert.Contains(values.ToList(), 8);
        CollectionAssert.Contains(values.ToList(), 9);
    }

    [TestMethod]
    public void GetPossibleValuesForColReturnsValuesNotInCol()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(0, 0, 4);
        board.Set(0, 1, 5);
        board.Set(0, 2, 6);

        // Act
        var values = board.GetPossibleValuesForCol(0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 1);
        CollectionAssert.Contains(values.ToList(), 2);
        CollectionAssert.Contains(values.ToList(), 3);
        CollectionAssert.Contains(values.ToList(), 7);
        CollectionAssert.Contains(values.ToList(), 8);
        CollectionAssert.Contains(values.ToList(), 9);
    }

    [TestMethod]
    public void GetPossibleValuesForBoxReturnsValuesNotInBox()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(0, 0, 1);
        board.Set(1, 0, 2);
        board.Set(2, 0, 3);
        board.Set(0, 1, 4);
        board.Set(1, 1, 5);
        board.Set(2, 1, 6);

        // Act
        var values = board.GetPossibleValuesForBox(0, 0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 7);
        CollectionAssert.Contains(values.ToList(), 8);
        CollectionAssert.Contains(values.ToList(), 9);
    }

    [TestMethod]
    public void GetPossibleValuesForCellReturnsValuesNotInRowColOrBox()
    {
        // Arrange
        var board = new GameBoard();
        board.Set(1, 1, 1);
        board.Set(2, 2, 2);  

        board.Set(3, 0, 4);
        board.Set(4, 0, 5);
        board.Set(5, 0, 6);

        board.Set(0, 3, 7);
        board.Set(0, 4, 8);
        board.Set(0, 5, 9);

        // Act
        var values = board.GetPossibleValuesForCell(0, 0);

        // Assert
        CollectionAssert.Contains(values.ToList(), 3);
    }
}