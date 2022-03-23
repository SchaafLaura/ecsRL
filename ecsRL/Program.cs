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

        public static Color uiColor = Color.Turquoise;

        private static void Main(string[] args)
        {
            Settings.WindowTitle = "Burden of Dreams";
            Game.Create(SCREEN_WIDTH, SCREEN_HEIGHT, "Cheepicus12.font");
            Game.Instance.OnStart = Init;
            Game.Instance.Run();
            Game.Instance.Dispose();
        }

        private static void Init()
        {
            ecs = new ECS();

            Entity Laura = new Entity
            {
                position = new Point(523, 507),
                name = "Laura"
            };
            Entity Lisa = new Entity
            {
                position = new Point(522, 507),
                name = "Lisa"
            };

            ecs.addEntity(Laura,
                new AIComponent(),
                new RenderComponent(
                    new ColoredGlyph(Color.HotPink, Color.Transparent, '@')));
            ecs.addEntity(Lisa,
                new RenderComponent(
                    new ColoredGlyph(Color.Turquoise, Color.Transparent, '@')),
                new AIComponent());

            rootScreen = new RootScreen();
            Game.Instance.Screen = rootScreen;
            Game.Instance.DestroyDefaultStartingConsole();
        }
    }
}