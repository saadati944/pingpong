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

            /*drawBox();
            writeMiddleofLine(0, "< 1 : 0 >");
            Console.ReadKey();*/
        }
        static void Draw()
        {

        }
        static void assignCharacters()
        {
            _ball = new consoleCharacter(0, 0, "╔═╗\n╚═╝");
            _player_1 = new consoleCharacter(1, 1, "╔╗\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╚╝");
            _player_2 = new consoleCharacter(0, 0, "╔╗\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╠╣\n╚╝");
        }
        static void drawCharacter(consoleCharacter ch)
        {
            Console.SetCursorPosition(ch.Left, ch.Top);
            int i = 0;
            foreach(char x in ch.Character)
            {
                if (x == '\n')
                {
                    ++i;
                    Console.SetCursorPosition(ch.Left, ch.Top + i);
                }
                else Console.Write(x);
            }
        }
        static void drawBox()
        {
            int w = Console.WindowWidth, h = Console.WindowHeight;
            Console.SetCursorPosition(0, 0);
            Console.Write(boxedges[0]);
            for (int i = 0; i < w - 2; i++) Console.Write(boxlines[1]);
            Console.Write(boxedges[1]);
            for (int i = 1; i < h - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(boxlines[0]);
                Console.SetCursorPosition(w-1, i);
                Console.Write(boxlines[0]);
            }
            Console.SetCursorPosition(0, h - 1);
            Console.Write(boxedges[2]);
            for (int i = 0; i < w - 2; i++) Console.Write(boxlines[1]);
            Console.Write(boxedges[3]);
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
