using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tartarus
{
    public class Renderer
    {
        public Scene Scene { get; private set; }
        public Camera Camera { get; private set; }

        public BlendState BlendState;
        public SamplerState SamplerState;
        public DepthStencilState DepthStencilState;
        public RasterizerState RasterizerState;
        public Effect Effect;

        public Renderer(Scene scene)
        {
            Scene = scene;
            Camera = new Camera(this);

            BlendState = BlendState.NonPremultiplied;
            SamplerState = SamplerState.PointClamp;
            DepthStencilState = DepthStencilState.None;
            RasterizerState = RasterizerState.CullNone;
            Effect = null;
        }

        public void Render()
        {
            if (Scene == null)
            {
                Logger.Log("Scene is null. Cannot draw.");
                return;
            }

            TartarusGame.Instance.GraphicsDevice.Clear(Scene.FillColor);

            Drawing.SpriteBatch.Begin(SpriteSortMode.Deferred,
                                      BlendState,
                                      SamplerState,
                                      DepthStencilState,
                                      RasterizerState,
                                      Effect,
                                      Camera.Matrix);

            Scene.BeforeRender();
            Scene.Render();
            Scene.AfterRender();

            Drawing.SpriteBatch.End();
            
        }





    }
}
