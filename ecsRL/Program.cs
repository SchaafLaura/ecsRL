using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class Program
    {
        public static ECS ecs;
        public static RootScreen rootScreen;
        public const int SCREEN_WIDTH = 160;
        public const int SCREEN_HEIGHT = 85;

        private static void Main(string[] args)
        {
            Settings.WindowTitle = "SadConsole Game";
            Settings.ResizeMode = Settings.WindowResizeOptions.Stretch; // might be a terrible idea, but a quick fix
            Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT, "Cheepicus12.font");
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

            //ecs.deleteEntity(actor1.id);
        }
    }
}