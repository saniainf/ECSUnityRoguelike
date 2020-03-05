using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Rooms
    {
        public string[][] RoomsArray = new string[][] {

            new string[]{
            "############",
            "#@.A.....#.#",
            "#...##...#.#",
            "#....#...#.#",
            "#X...#...#.#",
            "##...#.....#",
            "###......#.#",
            "  #......#.#",
            "  #......#.#",
            "  ##########"},

            new string[]{
            "################",
            "#@.A.........#.#",
            "#.....###....#.#",
            "#........#...#.#",
            "#X.....##....#.#",
            "##.......#.....#",
            "###...###....#.#",
            "  #..........#.#",
            "  #..#.......#.#",
            "  ##############"},

            new string[]{
            "###############",
            "#@.A........#.#",
            "#....#..#...#.#",
            "#....#..#...#.#",
            "#X...####...#.#",
            "##......#.....#",
            "###.....#...#.#",
            "  #.........#.#",
            "  #..#......#.#",
            "  #############"}

        };

        private Dictionary<char, string> ids = new Dictionary<char, string> {
            {'.', "Dirt"},
            {'#', "Ruins"},
            {'@', "Player"},
            {'A', "AcidPuddle"},
            {'X', "ExitPoint" }};


        public string GetNameID(char c)
        {
            ids.TryGetValue(c, out string value);
            return value;
        }
    }
}
