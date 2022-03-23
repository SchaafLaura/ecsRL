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
            //Settings.ResizeMode = Settings.WindowResizeOptions.Stretch; // might be a terrible idea, but a quick fix
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


            Entity human = new Entity
            {
                position = new Point(23, 7),
                name = "Laura"
            };

            Entity cat = new Entity
            {
                position = new Point(6, 10),
                name = "Lisa"
            };

            ecs.addEntity(cat,
                new AIComponent(),
                new RenderComponent(
                    new ColoredGlyph(Color.Turquoise, Color.Transparent, '@')));

            ecs.addEntity(human,
                new RenderComponent(
                    new ColoredGlyph(Color.HotPink, Color.Transparent, '@')),
                new AIComponent());

            //ecs.deleteEntity(actor1.id);
        }
    }
}