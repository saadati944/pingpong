using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pingpong
{
    class Program
    {
        //variables :
        static bool autoMovePlayer2 = false;
        static bool game_started = false;
        static consoleCharacter _ball;
        static consoleCharacter _player_1;
        static consoleCharacter _player_2;
        static consoleCharacter _ball_last;
        static consoleCharacter _player_1_last;
        static consoleCharacter _player_2_last;
        static int player1_score, player2_score;
        static int Last_player1_score, Last_player2_score;
        static bool plater1winds = false;
        static int lastw, lasth;
        static int last_scoreLength;

        //top left, top right, bottom left, bottom right. ╱╲┗┛┏┓╏╍
        static char[] boxedges ={ '┌', '┐', '└', '┘' };
        //vertical, horizontal.
        static char[] boxlines = { '│', '─' };




        static void Main(string[] args)
        {
            assignCharacters();
            string[] modes = new string[] { "Exit","P1 vs. PC", "P1 vs. P2" };
            string type = menu(modes, "Welcome to Terminal pingpong !!!\n select game modes :\n");
            if (type == modes[0])
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            else if (type == modes[1])
                autoMovePlayer2 = true;

            Console.Write("Press any key to start the game.");
            Console.ReadKey();
            /*drawBox();
            writeMiddleofLine(0, "< 1 : 0 >");
            Console.ReadKey();*/

            for (int i = 0; i < 1000; i++)
             {
                 Draw();
                 System.Threading.Thread.Sleep(30);
                 player1_score = i / 20;
                 player2_score = i / 15;
             }
            lasth = Console.WindowHeight; lastw = Console.WindowWidth;
        }
        static void Draw()
        {
            doMoves();
            if (lasth != Console.WindowHeight || lastw != Console.WindowWidth)
            {
                drawBox();
                drawScoreboard(true) ;
                lasth = Console.WindowHeight; lastw = Console.WindowWidth;
            }
            drawScoreboard();

            //drawCharacter(_player_1);
            //drawCharacter(_player_2);
            //drawCharacter(_ball);
            _player_1_last = _player_1;
            _player_2_last = _player_2;
            check();
        }
        
        static void check()
        {

        }
        static void doMoves()
        {
            while (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        break;
                    case ConsoleKey.DownArrow:
                        break;
                    case ConsoleKey.W:
                        break;
                    case ConsoleKey.S:
                        break;
                    case ConsoleKey.Escape:
                        break;
                    default:
                        break;
                }
            }
        }
        static void assignCharacters()
        {
            int h = Console.WindowHeight, w = Console.WindowWidth;
            _ball = new consoleCharacter(0, 0, "╔═╗\n╚═╝");
            _player_1 = new consoleCharacter(1, 1, "╔╗\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╚╝");
            _player_2 = new consoleCharacter(0, 0, "╔╗\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╚╝");
        }
        static void drawScoreboard(bool forceWrite=false)
        {
            if (forceWrite)
            {
                string lscoreBoard = string.Format("< {0} : {1} >", player1_score, player2_score);
                Last_player1_score = player1_score;
                Last_player2_score = player2_score;
                writeMiddleofLine(0, lscoreBoard);
                last_scoreLength = lscoreBoard.Length;
                return;
            }
            if (Last_player1_score == player1_score
            && Last_player2_score == player2_score)
                return;
            string scoreBoard = string.Format("< {0} : {1} >", player1_score, player2_score);
            Last_player1_score = player1_score;
            Last_player2_score = player2_score;
            if (scoreBoard.Length != last_scoreLength)
                writeMiddleofLine(0, stringMultiply(boxlines[1], last_scoreLength));
            writeMiddleofLine(0, scoreBoard);
            last_scoreLength = scoreBoard.Length;
        }
        static void drawCharacter(consoleCharacter ch)
        {
            Console.SetCursorPosition(ch.Left, ch.Top);
            int i = 0;
            foreach (char x in ch.Character)
            {
                if (x == '\n')
                {
                    ++i;
                    Console.SetCursorPosition(ch.Left, ch.Top + i);
                }
                else Console.Write(x);
            }
        }
        static void clearCharacter(consoleCharacter ch)
        {
            Console.SetCursorPosition(ch.Left, ch.Top);
            int i = 0;
            foreach (char x in ch.Character)
            {
                if (x == '\n')
                {
                    ++i;
                    Console.SetCursorPosition(ch.Left, ch.Top + i);
                }
                else Console.Write(' ');
            }
        }
        static void drawBox()
        {
            string Box = "";
            int w = Console.WindowWidth, h = Console.WindowHeight;
            Box += boxedges[0];
            string bottom= stringMultiply(boxlines[1], w - 2);
            Box += bottom;
            Box += boxedges[1];

            string verts = boxlines[0] + stringMultiply(' ', w - 2) + boxlines[0];
            for (int i = 0; i < h - 2; i++) Box += verts;
            Box += boxedges[2];
            Box += bottom;
            Box += boxedges[3];
            Console.SetCursorPosition(0, 0);
            Console.Write(Box);
            Console.SetCursorPosition(0, 0);
        }
        static void writeMiddleofLine(int line, string text)
        {
            int w = Console.WindowWidth;
            if (text.Length > w - 2)
                Console.SetCursorPosition(0, line) ;
            else
                Console.SetCursorPosition((w-text.Length)/2,line);
            Console.Write(text);
        }
        static string stringMultiply(string str, int v)
        {
            string output = "";
            for (int i = 0; i < v; i++)
                output += str;
            return output;
        }
        static string stringMultiply(char str, int v)
        {
            string output = "";
            for (int i = 0; i < v; i++)
                output += str;
            return output;
        }





        static string menu(string[] items, string title)
        {
            Console.Clear();
            Console.WriteLine(title);
            int enterCount = entersCount(title) + 1;
            int selected = 0, last = 0;
            for (int i = 0; i < items.Length; i++)
                Console.WriteLine((i == selected ? "  -->  " : "") + items[i] + (i == selected ? "  <--" : ""));
            while (true)
            {
                ConsoleKeyInfo ck = Console.ReadKey(true);
                if (ck.Key == ConsoleKey.DownArrow)
                {
                    selected++;
                    if (selected > items.Length - 1)
                        selected = 0;
                }
                else if (ck.Key == ConsoleKey.UpArrow)
                {
                    selected--;
                    if (selected < 0)
                        selected = items.Length - 1;
                }
                else if (ck.Key == ConsoleKey.Enter)
                {
                    if (selected == -1)
                        return "";
                    else return items[selected];
                }
                if (selected != last)
                {
                    Console.SetCursorPosition(0, enterCount + last);
                    Console.Write(emptyString(12 + items[last].Length));
                    Console.SetCursorPosition(0, enterCount + last);
                    Console.Write(items[last]);
                    Console.SetCursorPosition(0, enterCount + selected);
                    Console.Write("  -->  " + items[selected] + "  <--");
                }
                last = selected;
            }
        }
        static int entersCount(string v)
        {
            int i = 0;
            foreach (char x in v)
                if (x == '\n')
                    i++;
            return i;
        }
        static string emptyString(int len)
        {
            string s = "";
            for (int i = 0; i < len; i++)
                s += " ";
            return s;
        }
    }
    public class consoleCharacter
    {
        public int Top=0, Left=0;
        public string Character="";
        public consoleCharacter(int top,int left,string character)
        {
            Top = top;
            Left = left;
            Character = character;
        }
    }
}
