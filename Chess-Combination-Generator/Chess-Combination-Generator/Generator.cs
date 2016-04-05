﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Combination_Generator
{
    public static class Generator
    {
        static List<byte> Pieces;

        static void Kings(FieldType[] board)
        {
            Random rnd = new Random();
            var index = rnd.Next(0, 63);
            board[BoardInformations.InsideBoard[index]] = FieldType.WhiteKing;

            var isComplete = false;
            var index2 = rnd.Next(0, 63);
            while (!isComplete)
            {
                if (index2 != index && !PossibleSteps.IsOtherKingNear(board, BoardInformations.InsideBoard[index], BoardInformations.InsideBoard[index2]))
                    isComplete = true;
                else
                    index2 = rnd.Next(0, 63);
            }
            board[BoardInformations.InsideBoard[index2]] = FieldType.BlackKing;
            Pieces.Add(BoardInformations.InsideBoard[index]);
            Pieces.Add(BoardInformations.InsideBoard[index2]);
        }

        static void Rocks(FieldType[] board, int number, bool isWhite = true)
        {
            if (number == 0)
                return;
            var isComplete = false;
            var numberOfRock = 0;
            Random rnd = new Random();
            var index = rnd.Next(0, 63);
            while (!isComplete)
            {
                if (!Pieces.Contains(BoardInformations.InsideBoard[index]))
                {
                    if (isWhite)
                        board[BoardInformations.InsideBoard[index]] = FieldType.WhiteRock;
                    else
                        board[BoardInformations.InsideBoard[index]] = FieldType.BlackRock;
                    Pieces.Add(BoardInformations.InsideBoard[index]);
                    numberOfRock++;
                    if (numberOfRock == number)
                        isComplete = true;
                }
                else
                    index = rnd.Next(0, 63);
            }
        }

        static void Knights(FieldType[] board, int number, bool isWhite = true)
        {
            if (number == 0)
                return;
            var isComplete = false;
            var numberOfKnight = 0;
            Random rnd = new Random();
            var index = rnd.Next(0, 63);
            while (!isComplete)
            {
                if (!Pieces.Contains(BoardInformations.InsideBoard[index]))
                {
                    if (isWhite)
                        board[BoardInformations.InsideBoard[index]] = FieldType.WhiteKnight;
                    else
                        board[BoardInformations.InsideBoard[index]] = FieldType.BlackKnight;
                    Pieces.Add(BoardInformations.InsideBoard[index]);
                    numberOfKnight++;
                    if (numberOfKnight == number)
                        isComplete = true;
                }
                else
                    index = rnd.Next(0, 63);
            }
        }

        static void Bishops(FieldType[] board, int number, bool isWhite = true)
        {
            if (number == 0)
                return;
            var isComplete = false;
            var numberOfKnight = 0;
            Random rnd = new Random();
            var index = rnd.Next(0, 63);
            while (!isComplete)
            {
                if (!Pieces.Contains(BoardInformations.InsideBoard[index]))
                {
                    if (isWhite)
                        board[BoardInformations.InsideBoard[index]] = FieldType.WhiteBishop;
                    else
                        board[BoardInformations.InsideBoard[index]] = FieldType.BlackBishop;
                    Pieces.Add(BoardInformations.InsideBoard[index]);
                    numberOfKnight++;
                    if (numberOfKnight == number)
                        isComplete = true;
                }
                else
                    index = rnd.Next(0, 63);
            }
        }

        static void Queen(FieldType[] board, int number, bool isWhite = true)
        {
            if (number == 0)
                return;
            var isComplete = false;
            var numberOfKnight = 0;
            Random rnd = new Random();
            var index = rnd.Next(0, 63);
            while (!isComplete)
            {
                if (!Pieces.Contains(BoardInformations.InsideBoard[index]))
                {
                    if (isWhite)
                        board[BoardInformations.InsideBoard[index]] = FieldType.WhiteQueen;
                    else
                        board[BoardInformations.InsideBoard[index]] = FieldType.BlackQueen;
                    Pieces.Add(BoardInformations.InsideBoard[index]);
                    numberOfKnight++;
                    if (numberOfKnight == number)
                        isComplete = true;
                }
                else
                    index = rnd.Next(0, 63);
            }
        }

        static void Pawn(FieldType[] board, int number, bool isWhite = true)
        {
            if (number == 0)
                return;
            var isComplete = false;
            var numberOfKnight = 0;
            Random rnd = new Random();
            var index = rnd.Next(0, 63);
            while (!isComplete)
            {
                if (!BoardInformations.RowOneEight.Contains(BoardInformations.InsideBoard[index]) && !Pieces.Contains(BoardInformations.InsideBoard[index]))
                {
                    if (isWhite)
                        board[BoardInformations.InsideBoard[index]] = FieldType.WhitePawn;
                    else
                        board[BoardInformations.InsideBoard[index]] = FieldType.BlackPawn;
                    Pieces.Add(BoardInformations.InsideBoard[index]);
                    numberOfKnight++;
                    if (numberOfKnight == number)
                        isComplete = true;
                }
                else
                    index = rnd.Next(0, 63);
            }
        }





        public static bool Generate(FieldType[] board, out string fen, bool checkIsOk = false, bool isWhite = true, int level = 5,
            int bQueens = 0, int bRocks = 2, int bKnights = 2, int bBishops = 0, int bPawns = 0,
            int wQueens = 0, int wRocks = 2, int wKnights = 2, int wBishops = 0, int wPawns = 0)
        {

            Pieces = new List<byte>();
            Kings(board);

            //White  
            Rocks(board, wRocks);
            Knights(board, wKnights);
            Bishops(board, wBishops);
            Queen(board, wQueens);
            Pawn(board, wPawns);
            //Black
            Rocks(board, bRocks, false);
            Knights(board, bKnights, false);
            Bishops(board, bBishops, false);
            Queen(board, bQueens, false);
            Pawn(board, bPawns, false);

            StepAndValue SAV = new StepAndValue(0, 0, FieldType.Frame, 0, new List<StepAndValue>());
            var value = AI.AlphaBeta(board, level, int.MinValue, int.MaxValue, isWhite, SAV);
            var searchedValue = isWhite ? int.MaxValue : int.MinValue;

            if ((!checkIsOk && (AI.IsCheck(board) || AI.IsCheck(board, false)) || value != searchedValue))//SAV.Children.First(y => y.EvaluatedValue == SAV.Children.Min(x => x.EvaluatedValue)).EvaluatedValue != int.MinValue))
            {
                fen = "";
                return false;
            }
            else
            {
                fen = BoardInformations.GetFEN(board, isWhite);
                return true;
            }
        }
    }
}