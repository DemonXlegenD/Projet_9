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

       
    }
}
*/