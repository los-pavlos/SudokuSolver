using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SudokuSolver
{
    public partial class MainWindow : Window
    {
        private TextBox[,] sudokuCells = new TextBox[9, 9];

        public MainWindow()
        {
            InitializeComponent();
            InitializeSudokuGrid();
        }

        private void InitializeSudokuGrid()
        {
            SudokuGrid.Children.Clear();
            SudokuGrid.Rows = 9;
            SudokuGrid.Columns = 9;

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
                    sudokuCells[row, col] = cell;
                    SudokuGrid.Children.Add(cell);
                }
            }
        }

        private async void SolveSudoku_Click(object sender, RoutedEventArgs e)
        {   
            if (checkValidCharacters())
            {

                if (await solveSudokuAsync(0, 0))
                {
                    MessageBox.Show("Sudoku solved!");
                }
                else
                {
                    MessageBox.Show("No solution exists for the given Sudoku.");
                }
            }
            else
            {
                MessageBox.Show("Invalid input. Please check the highlighted cells.");
            }
        }

        private void ClearSudoku_Click(object sender, RoutedEventArgs e)
        {   
            clearBackground();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sudokuCells[row, col].Text = string.Empty;
                }
            }
        }

        private void ResetColors_Click(object sender, RoutedEventArgs e)
        {
            clearBackground();
        }

        private bool checkValidCharacters()
        {
            clearBackground();
            bool valid = true;
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudokuCells[row, col].Text != "")
                    {
                        if (sudokuCells[row, col].Text.Length > 1 || !int.TryParse(sudokuCells[row, col].Text, out int num) || num < 1 || num > 9)
                        {
                            sudokuCells[row, col].Background = Brushes.Salmon;
                            valid = false;
                        }
                    }
                }
            }

            if (valid)
            {
                HighlightDuplicates();
            }

            return valid;
        }

        private void HighlightDuplicates()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (sudokuCells[row, col].Text != "")
                    {
                        if (!isSafe(row, col, int.Parse(sudokuCells[row, col].Text)))
                        {
                            sudokuCells[row, col].Background = Brushes.LightBlue;
                        }
                    }
                }
            }
        }

        private void clearBackground()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sudokuCells[row, col].Background = Brushes.White;
                }
            }
        }

        private bool isSafe(int row, int col, int num)
        {
            // Check row
            for (int x = 0; x < 9; x++)
            {
                if (sudokuCells[row, x].Text == num.ToString())
                {
                    return false;
                }
            }

            // Check column
            for (int x = 0; x < 9; x++)
            {
                if (sudokuCells[x, col].Text == num.ToString())
                {
                    return false;
                }
            }

            // Check 3x3 submatrix
            int startRow = row - row % 3;
            int startCol = col - col % 3;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (sudokuCells[i + startRow, j + startCol].Text == num.ToString())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private async Task<bool> solveSudokuAsync(int row, int col)
        {
            if (row == 8 && col == 9)
                return true;

            if (col == 9)
            {
                row++;
                col = 0;
            }

            if (sudokuCells[row, col].Text.Length > 0)
                return await solveSudokuAsync(row, col + 1);

            for (int num = 1; num <= 9; num++)
            {
                if (isSafe(row, col, num))
                {
                    sudokuCells[row, col].Text = num.ToString();
                    await Task.Delay(1); // delay for animation
                    if (await solveSudokuAsync(row, col + 1))
                        return true;
                    sudokuCells[row, col].Text = string.Empty;
                }
            }

            return false;
        }

   
    }
}