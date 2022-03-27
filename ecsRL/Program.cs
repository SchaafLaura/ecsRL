using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class Program
    {
        public static Player player;
        public static ECS ecs;
        public static Map map;
        public static RootScreen rootScreen;
        
        public const int SCREEN_WIDTH = 160;
        public const int SCREEN_HEIGHT = 85;
        public const int MAP_WIDTH = 1000;
        public const int MAP_HEIGHT = 1000;

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
            // initialize ECS
            initECS();

            // initialize Map
            initMap();

            // initialize Root Screen and it's children
            initScreen();
        }

        private static void initMap()
        {
            map = new Map(MAP_WIDTH, MAP_HEIGHT);

            foreach(Entity entity in ecs.Entities)
            {
                map.entities.Add(entity, entity.position.X, entity.position.Y);
            }
        }

        private static void initScreen()
        {
            MapDisplay mapDisplay = new MapDisplay(map, SCREEN_WIDTH - 41, SCREEN_HEIGHT - 2, new Point(1, 1));
            mapDisplay.centerOnEntity(ecs.getEntity(0));

            LogDisplay logDisplay = new LogDisplay(38, SCREEN_HEIGHT / 2 - 1, new Point(SCREEN_WIDTH - 39, 1));

            InfoDisplay infoDisplay = new InfoDisplay(38, SCREEN_HEIGHT / 2 - 1, new Point(SCREEN_WIDTH - 40 + 1, SCREEN_HEIGHT / 2 + 1));


            ScreenSurface playerInfoDisplay = new ScreenSurface(38, SCREEN_HEIGHT / 2 - 1);
            playerInfoDisplay.Position = new Point(SCREEN_WIDTH - 40 + 1, SCREEN_HEIGHT / 2 + 1);
            playerInfoDisplay.Surface.Fill(Color.Transparent, Color.Brown);

            rootScreen = new RootScreen(mapDisplay, logDisplay, infoDisplay);
            rootScreen.UseMouse = true;

            Game.Instance.Screen = rootScreen;
            Game.Instance.DestroyDefaultStartingConsole();
        }

        private static void initECS()
        {
            ecs = new ECS();

            Creature cat = new Creature
            {
                position = new Point(525, 508),
                name = "Matrix"
            };

            player = new Player
            {
                position = new Point(523, 507),
                name = "Laura"
            };

            Creature Lisa = new Creature
            {
                position = new Point(522, 507),
                name = "Lisa"
            };

            ecs.addEntity(player,
                new AIComponent(),
                new RenderComponent(
                    new ColoredGlyph(Color.HotPink, Color.Transparent, '@')));
            ecs.addEntity(Lisa,
                new RenderComponent(
                    new ColoredGlyph(Color.Turquoise, Color.Transparent, '@')),
                new AIComponent());
            ecs.addEntity(cat,
                new RenderComponent(
                    new ColoredGlyph(Color.DarkGoldenrod, Color.Transparent, 'c')));

        }
    }
}