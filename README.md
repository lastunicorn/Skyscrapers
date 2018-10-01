# Skyscrapers

This is the solution for the codewars.com problem "4 By 4 Skyscrapers" ( here is the link: https://www.codewars.com/kata/4-by-4-skyscrapers )

## The main concepts
- SkyscrapersBoard - This is the class that represents the game. It created the Matrix (playing board) and creates the list of rules to be applied on each line in order to solve the game.
- Matrix - It is a n x n board that stores in each cell a list of possible values that can be removed one by one base on the rules until a solution is found.
- Cell - Represents a cell in the Matrix. It can store a list of possible values.
- IRule - The rules that removes from the Matrix values based on some conditions.