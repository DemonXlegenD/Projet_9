using System;
using System.Collections.Generic;
using NComponents;

namespace Map
{
    public class Map : Component
    {

        private List<Dictionary<int, char>> _characterObjects;
        private int[,] _tileMap;


        public Map(int width, int height)
        {
            _tileMap = new int[height, width];
            // Initialiser la carte avec des espaces 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _tileMap[i, j] = 0;
                }
            }
        }

        public void SetTile(int x, int y, int tileId)
        {
            if (x >= 0 && x < _tileMap.GetLength(1) && y >= 0 && y < _tileMap.GetLength(0))
            {
                _tileMap[y, x] = tileId;
            }
        }

        public override void Update(float deltaTime)
        {
            
        }
        public override void Render()
        {
            for (int i = 0; i < _tileMap.GetLength(0); i++)
            {
                for (int j = 0; j < _tileMap.GetLength(1); j++)
                {
                    Console.Write(_tileMap[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
