using System;
using System.Collections.Generic;

namespace _1586_Monochrome_Tile_AIZU_ONLINE_JUDGE
{
    class Inputs
    {
        public int W;
        public int H;
        public int N;

        public List<int> ax = new List<int>();
        public List<int> ay = new List<int>();

        public List<int> bx = new List<int>();
        public List<int> by = new List<int>(); 

    }

    enum TILE_COLOR
    {
        WHITE = 0,
        BLACK
    }

    class Program
    {
        static void Main(string[] args)
        {
            var inputs = getInputs();
            var blackTileNum = calculateBlackTileNum(inputs);
            outputResults(blackTileNum);
        }

        static Inputs getInputs()
        {
            var inputs = new Inputs();
            var inputString = Console.ReadLine();
            var stringArray = inputString.Split(" ");

            inputs.W = int.Parse(stringArray[0]);
            inputs.H = int.Parse(stringArray[1]);
            inputs.N = int.Parse(Console.ReadLine());

            for(var i = 0; i < inputs.N; i++)
            {
                inputString = Console.ReadLine();
                stringArray = inputString.Split(" ");
                inputs.ax.Add(int.Parse(stringArray[0]));
                inputs.ay.Add(int.Parse(stringArray[1]));
                inputs.bx.Add(int.Parse(stringArray[2]));
                inputs.by.Add(int.Parse(stringArray[3]));
            }

            return inputs;
        }

        static IEnumerable<int> calculateBlackTileNum(Inputs inputs)
        {
            // 初期化すべきか
            var WH = new TILE_COLOR[inputs.W, inputs.H];
            List<int> blackTileNum = new List<int>();

            for(var i = 0; i < inputs.N; i++)
            {
                WH = changeTileColor(WH, inputs, i);
                blackTileNum.Add(countBlackTileNum(WH));
            }

            return blackTileNum;
        }

        static TILE_COLOR[,] changeTileColor(TILE_COLOR[,] WH, Inputs inputs, int i)
        {
            // [axi, ayi]から[bxi, byi]の間に黒タイルが無いかを確認
            // row, columnは現在の行・列を示す
            for(var row = (inputs.ax[i] - 1); row <= (inputs.bx[i] - 1); row++)
            {
                for(var column = (inputs.ay[i] - 1); column <= (inputs.by[i] - 1); column++)
                {
                    if(WH[row, column] == TILE_COLOR.BLACK)
                    {
                        // returnを複数持つコードは可読性観点でどうだろうか？
                        return WH;
                    }
                }
            }

            // 上と下でそれぞれ別関数作っても良いかもしれない

            // 上記確認をパスした場合、塗り替え作業実施
            for(var row = (inputs.ax[i] - 1); row <= (inputs.bx[i] - 1); row++)
            {
                for(var column = (inputs.ay[i] - 1); column <= (inputs.by[i] - 1); column++)
                {
                    WH[row, column] = TILE_COLOR.BLACK;
                }
            }

            return WH;
        }

        static int countBlackTileNum(TILE_COLOR[,] WH)
        {
            var blackTileNum = 0;
            foreach(var tile in WH)
            {
                if(tile == TILE_COLOR.BLACK)
                {
                    blackTileNum++;
                }
            }

            return blackTileNum;
        }

        static void outputResults(IEnumerable<int> blackTileNum)
        {
            foreach(var elem in blackTileNum)
            {
                System.Console.WriteLine(elem);
            }
        }
    }
}