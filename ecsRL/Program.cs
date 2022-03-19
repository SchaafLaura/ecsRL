using SadConsole;
using SadRogue.Primitives;
using System;
using Console = SadConsole.Console;

namespace ecsRL
{
    
    public class Program
    {
        public static ECS ecs;
        public static RootScreen rootScreen;
        public const int SCREEN_WIDTH = 240;
        public const int SCREEN_HEIGHT = 64;

        private static void Main(string[] args)
        {
            Settings.WindowTitle = "SadConsole Game";
            Settings.UseDefaultExtendedFont = true;
            Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT);

            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            ecs = new ECS();

            rootScreen = new RootScreen();
            Game.Instance.Screen = rootScreen;
            Game.Instance.DestroyDefaultStartingConsole();



            Entity actor1 = new Entity
            {
                position = new Point(12, 7)
            };

            ecs.addEntity(actor1,
                new RenderComponent(
                    new ColoredGlyph(Color.HotPink, Color.Transparent, '@')),
                new AIComponent());


            Entity actor2 = new Entity
            {
                position = new Point(20, 16)
            };

            ecs.addEntity(actor2,
                new RenderComponent(
                    new ColoredGlyph(Color.AnsiBlue, Color.Transparent, '@')),
                new AIComponent());


        }
    }
}