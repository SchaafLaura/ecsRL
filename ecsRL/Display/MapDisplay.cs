﻿using System;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class MapDisplay : ScreenObject
    {
        public Map map;
        public ScreenSurface surface;
        Point mapViewPosition;
        public int viewWidth;
        public int viewHeight;

        public MapDisplay(Map map, int viewWidth, int viewHeight, Point screenPosition)
        {
            this.map = map;
            this.viewWidth = viewWidth;
            this.viewHeight = viewHeight;
            surface = new ScreenSurface(viewWidth, viewHeight);
            surface.Position = screenPosition;
            init();
        }

        public bool isOnScreen(Point gamePosition)
        {
            return
                gamePosition.X > mapViewPosition.X - viewWidth / 2 &&
                gamePosition.X < mapViewPosition.X + viewWidth / 2 &&
                gamePosition.Y > mapViewPosition.Y - viewHeight / 2 &&
                gamePosition.Y < mapViewPosition.Y + viewHeight / 2;
        }

        public void display()
        {
            // loop through visible tiles
            for(int i = 1; i < viewWidth - 1; i++)
            {
                for(int j = 1; j < viewHeight - 1; j++)
                {
                    Point surfacePos = new Point(i, j);
                    Point gamePos = surfaceCoordsToGameCoords(surfacePos);

                    // display underlaying tile
                    Tile tile = map.tiles[gamePos.X, gamePos.Y];
                    surface.Surface.SetCellAppearance(surfacePos.X, surfacePos.Y, tile.glyph);

                    // display item(s) at that position
                    var items = map.items.GetItems(gamePos.X, gamePos.Y);
                    foreach(Item item in items)
                    {
                        surface.Surface.SetCellAppearance(surfacePos.X, surfacePos.Y, item.renderComponent.glyph);
                    }

                    // display actor at that position
                    Actor actor = map.actors.GetItem(gamePos.X, gamePos.Y);
                    if(actor != null)
                        surface.Surface.SetCellAppearance(surfacePos.X, surfacePos.Y, actor.renderComponent.glyph);
                }
            }
        }

        public override void Update(TimeSpan delta)
        {
            display();
            base.Update(delta);
        }

        public Point gameCoordsToSurfaceCoords(Point gameCoords)
        {
            return new Point(gameCoords.X - (mapViewPosition.X - viewWidth / 2), gameCoords.Y - (mapViewPosition.Y - viewHeight / 2));
        }

        public Point surfaceCoordsToGameCoords(Point surfaceCoords)
        {
            return new Point(surfaceCoords.X + (mapViewPosition.X - viewWidth / 2) + Position.X - 1, surfaceCoords.Y + (mapViewPosition.Y - viewHeight / 2) + Position.Y - 1);
        }

        public Point screenCoordsToGameCoords(Point screenCoords)
        {
            return surfaceCoordsToGameCoords(screenCoords - surface.Position);
        }

        public void centerOnEntity(Entity entity)
        {
            mapViewPosition = new Point(
                entity.position.X,
                entity.position.Y);
        }

        // draws the border around the map
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

    }
}
