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
        public static Font SmallFont { get; private set; }
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

        private Test Test = new Test();
        private Demo Demo = new Demo();

        private TestScene t;


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
            Input.Initialize();
            Drawing.Initialize();

            SmallFont = new Font("small_font.png", 4, 6);

            //Demo.Initialize();
            t = new TestScene();
            Scene = t;

            base.Initialize();
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
            {
                Scene.Update();
            }

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
            GraphicsDevice.Clear(BGColor);

            //Drawing.SpriteBatch.Begin(SpriteSortMode.Deferred, 
            //                          BlendState.AlphaBlend,
            //                          SamplerState.PointClamp, 
            //                          null, null, null, Overlord.Matrix);

            //Demo.Draw();

            //Drawing.SpriteBatch.End();

            if (Scene != null)
                Scene.Draw();

            base.Draw(gameTime);
        }

        protected virtual void OnSceneTransition(Scene from, Scene to)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }






    }
}
