using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class Tileset
    {
        public GTexture Parent { get; private set; }
        private GTexture[,] textures;
        public int TextureWidth { get; private set; }
        public int TextureHeight { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GTexture this[int x, int y] => textures[x, y];
        public GTexture this[int index] => textures[index % Width, index / Width];

        public Tileset(GTexture parent, int width, int height)
        {
            Setup(parent, width, height);
        }

        public Tileset(string filename, int width, int height)
        {
            Setup(new GTexture(filename), width, height);
        }

        private void Setup(GTexture parent, int textureWidth, int textureHeight)
        {
            Parent = parent;
            TextureWidth = textureWidth;
            TextureHeight = textureHeight;
            Width = parent.Width / textureWidth;
            Height = parent.Height / textureHeight;

            textures = new GTexture[Width, Height];


            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    GTexture temp = new GTexture(parent, j * TextureWidth, i * TextureHeight, TextureWidth, TextureHeight);
                    
                    textures[j, i] = temp;
                }
            }
        }

        



    }
}
