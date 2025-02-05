using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuSolver
{
    public partial class MainWindow : Window
    {
        private int[,] board = new int[9, 9];
        private TextBox[,] sudokuCells = new TextBox[9, 9];
        private bool[,] isInitial = new bool[9, 9];
        public MainWindow()
        {
            InitializeComponent();
            InitializeSudokuGrid();
        }

        private void InitializeSudokuGrid()
        {
            SudokuGrid.Children.Clear();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    TextBox cell = new TextBox
                    {
                        Width = 40,
                        Height = 40,
                        FontSize = 20,
                        TextAlignment = TextAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(
                            col % 3 == 0 ? 2 : 1,
                            row % 3 == 0 ? 2 : 1,
                            (col + 1) % 3 == 0 ? 2 : 1,
                            (row + 1) % 3 == 0 ? 2 : 1
                        ),
                        Margin = new Thickness(0),
                    };
                    cell.TextChanged += (s, e) => UpdateBoardFromUI();
                    sudokuCells[row, col] = cell;
                    SudokuGrid.Children.Add(cell);
                }
            }
        }

        private void UpdateBoardFromUI()
        {
            
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    string text = sudokuCells[row, col].Text.Trim();

                    if (text == "")
                    {
                        board[row, col] = 0;
                        sudokuCells[row, col].Background = Brushes.White;
                    }
                    else if (int.TryParse(text, out int num) && num >= 1 && num <= 9)
                    {
                        board[row, col] = num;
                        if(isInitial[row, col] == false)
                            sudokuCells[row, col].Background = Brushes.White;
                        else
                            sudokuCells[row, col].Background = Brushes.LightBlue; // User input
                    }
                    else
                    {
                        sudokuCells[row, col].Background = Brushes.Red; // Invalid input
                    }
                }
            }

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    int num = board[row, col];
                    if (num != 0)
                    {
                        board[row, col] = 0;
                        if (!IsSafe(row, col, num))
                        {
                            sudokuCells[row, col].Background = Brushes.Red;
                        }
                        board[row, col] = num;
                    }
                }
            }
        }


        private void UpdateUIFromBoard()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sudokuCells[row, col].Text = board[row, col] == 0 ? "" : board[row, col].ToString();
                }
            }
        }

        private async void SolveSudoku_Click(object sender, RoutedEventArgs e)
        {
            UpdateBoardFromUI();
            setAllInitials();

            if (await SolveSudokuAsync(0, 0))
            {
                UpdateUIFromBoard();
                MessageBox.Show("Sudoku vyřešeno!");
            }
            else
            {
                MessageBox.Show("Toto sudoku nelze vyřešit.");
            }
        }

        private void ClearSudoku_Click(object sender, RoutedEventArgs e)
        {   
            for(int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sudokuCells[row, col].Text = "";
                }
            }
            
            UpdateUIFromBoard();
        }

        private bool IsSafe(int row, int col, int num)
        {
            //  Check of the current row and column
            for (int x = 0; x < 9; x++)
            {
                if (board[row, x] == num || board[x, col] == num)
                    return false;
            }

            // Check of the current 3x3 box
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i + startRow, j + startCol] == num)
                        return false;
                }
            }

            return true;
        }

        private async Task<bool> SolveSudokuAsync(int row, int col)
        {
            if (row == 8 && col == 9)
                return true;

            if (col == 9)
            {
                row++;
                col = 0;
            }

            if (board[row, col] != 0)
                return await SolveSudokuAsync(row, col + 1);

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(row, col, num))
                {
                    board[row, col] = num;
                    UpdateUIFromBoard();
                    await Task.Delay(10); // Animation
                    if (await SolveSudokuAsync(row, col + 1))
                        return true;
                    board[row, col] = 0;
                    UpdateUIFromBoard();
                }
            }

            return false;
        }

        private void setAllInitials()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] != 0)
                    {
                        isInitial[row, col] = true;
                    }
                }
            }
        }

    }
}
