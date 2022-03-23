﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SadConsole.UI;
using SadRogue.Primitives;

namespace ecsRL
{
    public class MapDisplay : ScreenObject
    {
        public Map map;
        ScreenSurface surface;
        Point mapViewPosition;
        int viewWidth;
        int viewHeight;

        public MapDisplay(int mapWidth, int mapHeight, int viewWidth, int viewHeight, Point mapViewPosition, Point screenPosition)
        {
            map = new Map(mapWidth, mapHeight);
            this.mapViewPosition = mapViewPosition;
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            surface = new ScreenSurface(viewWidth, viewHeight);
            surface.Position = screenPosition;
            init();
            
        }

        public MapDisplay(Map map, int viewWidth, int viewHeight, Point screenPosition)
        {
            this.map = map;
            surface = new ScreenSurface(viewWidth, viewHeight);
            surface.Position = screenPosition;
            init();
        }

        private void init()
        {
            Color[] colors = new[] { Color.Turquoise, Color.HotPink };
            float[] colorStops = new[] { 0f, 1f };

            Algorithms.GradientFill(surface.FontSize,
                                    surface.Surface.Area.Center,
                                    surface.Surface.Height / 3,
                                    0,
                                    surface.Surface.Area,
                                    new Gradient(colors, colorStops),
                                    (x, y, color) => surface.Surface[x, y].Foreground = color);

            surface.Surface.DrawBox(
                surface.Surface.Area,
                ShapeParameters.CreateStyledBox(
                    ICellSurface.ConnectedLineThin,
                    new ColoredGlyph(),
                    true,
                    true,
                    true));

            surface.Surface.Print(1, 0, "Map");

            Children.Add(surface);
        }

        public void display()
        {
            displayTiles();
            displayItems();
        }

        public void drawGlyph(int x, int y, ColoredGlyph glyph)
        {
            surface.Surface.SetGlyph(x, y, glyph);
        }

        private void displayTiles()
        {
            ;
            for(int i = mapViewPosition.X - viewWidth / 2; i < mapViewPosition.X + viewWidth / 2 - 1; i++)
            {
                for(int j = mapViewPosition.Y - viewHeight / 2; j < mapViewPosition.Y + viewHeight / 2 - 1; j++)
                {
                    Tile tile = map.tiles[i, j];
                    int x = i - (mapViewPosition.X - viewWidth / 2) + 1;
                    int y = j - (mapViewPosition.Y - viewHeight / 2) + 1;
                    drawGlyph(x, y, tile.glyph);
                }
            }
        }

        private void displayItems()
        {

        }

        public override void Update(TimeSpan delta)
        {
            display();
            base.Update(delta);
        }
    }
}
