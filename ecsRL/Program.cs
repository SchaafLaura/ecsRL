﻿using SadConsole;
using SadRogue.Primitives;
using System;
using GoRogue;
using GoRogue.Pathing;

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
        public static FastAStar fastAStar;
        
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
            ecs = new ECS();
            map = new Map(MAP_WIDTH, MAP_HEIGHT);
            fastAStar = new FastAStar(map, GoRogue.Distance.MANHATTAN);

            // initialize ECS
            initECS();

            // initialize Map
            initMap();

            // initialize Root Screen and it's children
            initScreen();
        }

        private static void initMap()
        {
            // actors got generated in initECS() and now get added to the map for displaying and stuff
            foreach(Actor actor in ecs.Actors)
                map.actors.Add(actor, actor.position.X, actor.position.Y);

            // just a test for now
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
            mapDisplay.centerOnEntity(ecs.getActor(0)); // centering on player, which always has ID 0

            // ingame log
            log = new Log();
            LogDisplay logDisplay = new LogDisplay(log, 38, SCREEN_HEIGHT / 2 - 1, new Point(SCREEN_WIDTH - 39, 1));

            // ingame debug information display
            InfoDisplay infoDisplay = new InfoDisplay(38, SCREEN_HEIGHT / 2 - 1, new Point(SCREEN_WIDTH - 40 + 1, SCREEN_HEIGHT / 2 + 1));

            // handles creating player actions (for now)
            inputHandler = new InputHandler();

            rootScreen = new RootScreen(mapDisplay, logDisplay, infoDisplay);

            Game.Instance.Screen = rootScreen;
            Game.Instance.DestroyDefaultStartingConsole();
        }

        private static void initECS()
        {
            // creating ECS, then a bunch of actors and add them to the ECS

            

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

            for(int i = 0; i < 5000; i++)
            {
                Random rng = new Random();
                Creature creature = new Creature
                {
                    position = new Point(rng.Next(20, MAP_WIDTH - 20), rng.Next(20, MAP_HEIGHT - 20)),
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