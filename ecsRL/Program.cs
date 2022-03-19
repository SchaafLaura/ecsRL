using SadConsole;
using SadRogue.Primitives;

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

            ecs.deleteEntity(actor1.id);
        }
    }
}