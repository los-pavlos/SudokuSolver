# Sudoku Solver

## Overview
Sudoku Solver is a WPF (Windows Presentation Foundation) application developed in C#. It allows users to input a Sudoku puzzle, verify its validity, and solve it automatically using a backtracking algorithm. The application highlights invalid entries, providing a user-friendly experience for solving Sudoku puzzles.

## Features
- **Grid-Based Input:** Users can input numbers directly into a 9x9 Sudoku grid.
- **Validation:** Highlights invalid characters and duplicate numbers in rows, columns, and 3x3 subgrids.
- **Automatic Solving:** Uses a recursive backtracking algorithm to solve the puzzle asynchronously.
- **User Interaction:**
  - Solve the Sudoku puzzle
  - Clear the grid
  - Reset cell colors
- **Responsive UI:** Designed with a fixed-size WPF window for simplicity and ease of use.

## Technologies Used
- **C#** (Backend logic)
- **WPF (Windows Presentation Foundation)** (User Interface)
- **XAML** (Layout Design)

## Installation
1. Clone or download the repository:
   ```sh
   git clone https://github.com/los-pavlos/SudokuSolver.git
   ```
2. Open the project in Visual Studio.
3. Build the solution to restore dependencies.
4. Run the application.

## How to Use
1. Enter numbers (1-9) into the Sudoku grid.
2. Click **Solve** to attempt solving the puzzle.
3. Click **Clear** to reset the grid.
4. If invalid inputs exist, they will be highlighted in red.
5. Sudoku entry is highlighted in blue
6. If a solution exists, it will be displayed in the grid.
7. If no solution exists, a message box will notify the user.

---
Enjoy solving Sudoku puzzles with this simple application!

