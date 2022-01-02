using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Reflection;
using System.Runtime;

namespace Tartarus
{
    public class TartarusGame : Game
    {
        private GraphicsDeviceManager graphics;

        public static Color BGColor = Color.Maroon;
        public static TartarusGame Instance { get; private set; }
        public static double RawTotalTime { get; private set; }
        public static double RawDeltaTime { get; private set; }
        public static float DeltaTime { get; private set; }
        public static string AssemblyLocationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string ResourceFolderPath = Path.Combine(AssemblyLocationPath, "Resources");
        
        public int ScreenWidth
        {
            get => graphics.PreferredBackBufferWidth;
            set
            {
                graphics.PreferredBackBufferWidth = value;
                graphics.ApplyChanges();
            }
        }
        public int ScreenHeight
        {
            get => graphics.PreferredBackBufferHeight;
            set
            {
                graphics.PreferredBackBufferHeight = value;
                graphics.ApplyChanges();
            }
        }

        public Scene Scene
        {
            get => scene;
            set => nextScene = value;
        }
        private Scene scene;
        private Scene nextScene;

        public TartarusGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Instance = this;
            
            ScreenWidth = Overlord.WindowWidth;
            ScreenHeight = Overlord.WindowHeight;

            DeltaTime = 0f;
            RawDeltaTime = 0;
            RawTotalTime = 0;

            IsMouseVisible = true;

            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;
        }

        protected override void Initialize()
        {
            base.Initialize();
            Input.Initialize();
            Drawing.Initialize();

            SceneManager.Initialize();

            Scene = SceneManager.Default;
        }

        protected override void LoadContent() { }
        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            RawDeltaTime = gameTime.ElapsedGameTime.TotalSeconds;
            RawTotalTime += RawDeltaTime;
            DeltaTime = (float)RawDeltaTime;

            Input.Update();

            if (Input.Check(Keys.Escape))
                Exit();

            //Demo.Update();

            if (Scene != null)
                Scene.Update();

            if (scene != nextScene)
            {
                var lastScene = scene;
                if (scene != null)
                    scene.End();
                scene = nextScene;
                OnSceneTransition(lastScene, nextScene);
                if (scene != null)
                    scene.Begin();
            }

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(BGColor);

            if (Scene != null)
                Scene.Renderer.Render();

            base.Draw(gameTime);
        }

        protected virtual void OnSceneTransition(Scene from, Scene to)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }






    }
}
