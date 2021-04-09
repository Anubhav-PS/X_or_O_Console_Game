using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        //variables 
        static Player player1 = new Player();
        static Player player2 = new Player();

        static int player1Turn = 1;
        static string[,] Board2d = new string[3, 3] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        static int[] filledSlots = new int[9];

        static int i = 0;
        static int slot = 0;
        static string symWon = "-1", playerWon;

        static bool isWon = false, isFullFilled = false;
        static Dictionary<int, string> board = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("****** Welcome to Tic Tac Toe ******");
            Console.ResetColor();
            getBoardReady();
            getNames();
            getXorY();
            do
            {
                Console.Clear();
                displayBoard();
                getIndex();
                checkIfWon();
                if (isFullFilled) break;
            } while (!isWon);
            Console.Clear(); 
            displayBoard();
            if (isWon && !isFullFilled)
            {
                playerWon = (symWon == player1.Sym) ? player1.Name : player2.Name;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("******The winner of the match is {0}******", playerWon);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("****** It is a draw ******");
                Console.ResetColor();
            }

        }

        static void getNames()
        {

            Console.Write("Enter player 1 Name : ");
            Console.ForegroundColor = ConsoleColor.Green;
            player1.Name = Console.ReadLine();
            Console.ResetColor();
            Console.Write("Enter player 2 Name : ");
            Console.ForegroundColor = ConsoleColor.Green;
            player2.Name = Console.ReadLine();
            Console.ResetColor();

        }

        static void getXorY()
        {
            string xory;
            bool input = true;
            do
            {
                Console.Write("{0} choose - 'X' or 'O' : ", player1.Name);
                Console.ForegroundColor = ConsoleColor.Green;
                xory = Console.ReadLine();
                Console.ResetColor();
                xory = xory.ToUpper();
                if (xory.Equals("X") || xory.Equals("O")) input = false;
            } while (input);

            player1.Sym = xory;
            player2.Sym = (xory.Equals("X")) ? "O" : "X";

        }

        static void displayBoard()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("------------------TIC-TAC-TOE--------------------");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("***** {0} -> {1} and {2} -> {3} *****", player1.Name, player1.Sym, player2.Name, player2.Sym);
            Console.ResetColor();
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2} ", Board2d[0, 0], Board2d[0, 1], Board2d[0, 2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2} ", Board2d[1, 0], Board2d[1, 1], Board2d[1, 2]);
            Console.WriteLine("_____|_____|_____");
            Console.WriteLine("     |     |     ");
            Console.WriteLine("  {0}  |  {1}  |  {2} ", Board2d[2, 0], Board2d[2, 1], Board2d[2, 2]);
            Console.WriteLine("     |     |     ");
            Console.WriteLine("Enter the index numbers to place your move in that respective slot");

        }

        static void getIndex()
        {
            if (player1Turn % 2 != 0)
            {
                getSlotFromPlayer(player1);
            }
            else if (player1Turn % 2 == 0)
            {
                getSlotFromPlayer(player2);
            }
            i++;
        }

        static void getSlotFromPlayer(Player player)
        {
            string index;
            bool input = true, isValidSlot = false;
            bool isFirstTime = true;
            do
            {
                if (isFirstTime)
                {
                    Console.Write("{0} enter your slot index : ", player.Name);
                    Console.ForegroundColor = ConsoleColor.Green;
                    index = Console.ReadLine();
                    Console.ResetColor();
                    input = int.TryParse(index, out slot);
                    isValidSlot = (slot < 1 || slot > 9) ? false : true;
                    isFirstTime = false;
                }
                else if (input == false)
                {
                    Console.Write("{0} enter a valid slot index : ", player.Name);
                    Console.ForegroundColor = ConsoleColor.Green;
                    index = Console.ReadLine();
                    Console.ResetColor();
                    input = int.TryParse(index, out slot);
                    isValidSlot = (slot < 1 || slot > 9) ? false : true;
                    if (input == true && isValidSlot && checkIfAvail(slot)) break;
                }
                else if (!isValidSlot)
                {
                    Console.Write("{0} enter a valid slot index between 1 and 9 : ", player.Name);
                    Console.ForegroundColor = ConsoleColor.Green;
                    index = Console.ReadLine();
                    Console.ResetColor();
                    input = int.TryParse(index, out slot);
                    isValidSlot = (slot < 1 || slot > 9) ? false : true;
                    if (isValidSlot && input == true && checkIfAvail(slot)) break;
                }
                else if (!checkIfAvail(slot))
                {
                    Console.Write("slot {0} is already filled : ", slot);
                    Console.ForegroundColor = ConsoleColor.Green;
                    index = Console.ReadLine();
                    Console.ResetColor();
                    input = int.TryParse(index, out slot);
                    isValidSlot = (slot < 1 || slot > 9) ? false : true;
                    if (checkIfAvail(slot) && input == true && isValidSlot) break;
                }

            } while (!input || !checkIfAvail(slot) || !isValidSlot);
            board[slot] = player.Sym;
            filledSlots[i] = slot;
            makeChanges(slot, player);
            player1Turn++;
        }

        static bool checkIfAvail(int slot)
        {
            if (player1Turn >= 9) isFullFilled = true;
            foreach (int x in filledSlots) if (x == slot) return false;
            return true;
        }

        static void checkIfWon()
        {
            if (board[1] == board[2] && board[1] == board[3])
            {
                isWon = true;
                symWon = board[1];
            }
            else if (board[4] == board[5] && board[4] == board[6])
            {
                isWon = true;
                symWon = board[4];
            }
            else if (board[7] == board[8] && board[7] == board[9])
            {
                isWon = true;
                symWon = board[7];
            }
            else if (board[1] == board[4] && board[1] == board[7])
            {
                isWon = true;
                symWon = board[1];
            }
            else if (board[2] == board[5] && board[2] == board[8])
            {
                isWon = true;
                symWon = board[2];
            }
            else if (board[3] == board[6] && board[3] == board[9])
            {
                isWon = true;
                symWon = board[3];
            }
            else if (board[1] == board[5] && board[1] == board[9])
            {
                isWon = true;
                symWon = board[1];
            }
            else if (board[3] == board[5] && board[3] == board[7])
            {
                isWon = true;
                symWon = board[3];
            }
        }

        static void makeChanges(int slot, Player player)
        {
            switch (slot)
            {
                case 1:
                    Board2d[0, 0] = player.Sym;
                    break;
                case 2:
                    Board2d[0, 1] = player.Sym;
                    break;
                case 3:
                    Board2d[0, 2] = player.Sym;
                    break;
                case 4:
                    Board2d[1, 0] = player.Sym;
                    break;
                case 5:
                    Board2d[1, 1] = player.Sym;
                    break;
                case 6:
                    Board2d[1, 2] = player.Sym;
                    break;
                case 7:
                    Board2d[2, 0] = player.Sym;
                    break;
                case 8:
                    Board2d[2, 1] = player.Sym;
                    break;
                case 9:
                    Board2d[2, 2] = player.Sym;
                    break;
            }
        }

        static void getBoardReady()
        {
            board.Add(1, "1");
            board.Add(2, "2");
            board.Add(3, "3");
            board.Add(4, "4");
            board.Add(5, "5");
            board.Add(6, "6");
            board.Add(7, "7");
            board.Add(8, "8");
            board.Add(9, "9");
        }

    }
}
