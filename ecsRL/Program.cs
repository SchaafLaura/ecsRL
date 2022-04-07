using SadConsole;
using SadRogue.Primitives;
using System;
using GoRogue;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ecsRL
{
    public class Program
    {
        public static Player player;
        public static ECS ecs;
        public static Map map;
        public static Log log;
        public static RootScreen rootScreen;
        public static InputHandler inputHandler;
        
        public const int SCREEN_WIDTH = 160;
        public const int SCREEN_HEIGHT = 85;
        public const int MAP_WIDTH = 700;
        public const int MAP_HEIGHT = 700;

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

            foreach(Actor actor in ecs.Actors)
                map.actors.Add(actor, actor.position.X, actor.position.Y);

            Item testItem = new Item()
            {
                name = "headless Teddybear",
                renderComponent = new RenderComponent(new ColoredGlyph(Color.Brown, Color.Transparent, 5)),
                description = "a sad sight",
                position = new Point(530, 530)
            };

            map.items.Add(testItem, new Coord(testItem.position.X, testItem.position.Y));
        }

        private static void initScreen()
        {
            MapDisplay mapDisplay = new MapDisplay(map, SCREEN_WIDTH - 41, SCREEN_HEIGHT - 2, new Point(1, 1));
            mapDisplay.centerOnEntity(ecs.getActor(0));

            log = new Log();
            LogDisplay logDisplay = new LogDisplay(log, 38, SCREEN_HEIGHT / 2 - 1, new Point(SCREEN_WIDTH - 39, 1));

            InfoDisplay infoDisplay = new InfoDisplay(38, SCREEN_HEIGHT / 2 - 1, new Point(SCREEN_WIDTH - 40 + 1, SCREEN_HEIGHT / 2 + 1));

            inputHandler = new InputHandler();

            rootScreen = new RootScreen(mapDisplay, logDisplay, infoDisplay);

            Game.Instance.Screen = rootScreen;
            Game.Instance.DestroyDefaultStartingConsole();
        }

        private static void initECS()
        {
            ecs = new ECS();

            Creature cat = new Creature
            {
                position = new Point(525, 508),
                name = "Matrix",
                speed = 50,
                renderComponent = new RenderComponent(
                    new ColoredGlyph(Color.DarkGoldenrod, Color.Transparent, 'c'))
            };

            player = new Player
            {
                position = new Point(523, 507),
                name = "Laura",
                speed = 100,
                renderComponent = new RenderComponent(
                    new ColoredGlyph(Color.HotPink, Color.Transparent, '@'))
            };

            Creature Lisa = new Creature
            {
                position = new Point(522, 507),
                name = "Lisa",
                speed = 50, 
                renderComponent = new RenderComponent(
                    new ColoredGlyph(Color.Turquoise, Color.Transparent, '@')),
                description = "super cute and valid"
            };

            ecs.addActor(player,
                new AIComponent());

            ecs.addActor(Lisa,
                new AIComponent());

            ecs.addActor(cat);

            for(int i = 0; i < 10000; i++)
            {
                Random rng = new Random();
                Creature creature = new Creature
                {
                    position = new Point(rng.Next(0, MAP_WIDTH), rng.Next(0, MAP_HEIGHT)),
                    name = "randomCreature " + i,
                    speed = rng.Next(5, 80),
                    currentEnergy = 100,
                    renderComponent = new RenderComponent(
                        new ColoredGlyph(Color.DarkGoldenrod, Color.Transparent, 'C'))
                };
                ecs.addActor(creature);
            }
        }
    }
}