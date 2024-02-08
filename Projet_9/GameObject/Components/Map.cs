/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maths;
using NComponents;
using Types;

namespace Map
{
    public class Map : Component
    {

        private List<CharacterObject> characterObjects;
        private int[,] map;


        public Map(int width, int height)
        {
            map = new int[height, width];
            // Initialiser la carte avec des espaces 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public void SetTile(int x, int y, int tileId)
        {
            if (x >= 0 && x < map.GetLength(1) && y >= 0 && y < map.GetLength(0))
            {
                map[y, x] = tileId;
            }
        }

        public override void Update(float deltaTime)
        {
            
        }
        public override void Render()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
*/