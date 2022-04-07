using System;
using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class AnimationDisplay : ScreenObject
    {
        List<Animation> animations;

        public AnimationDisplay()
        {
            animations = new List<Animation>();
        }

        public void tryAddAnimationAtGamePosition(CellSurface[] frames, Point gamePosition)
        {
            if(isOnScreen(gamePosition))
            {
                Animation animation = new Animation(frames, gamePosition);
                animations.Add(animation);
                Children.Add(animation);
            }
        }

        public override void Update(TimeSpan delta)
        {
            for(int i = 0; i < animations.Count; i++)
            {
                if(!animations[i].isPlaying)
                {
                    Children.Remove(animations[i]);
                    animations[i] = null;
                    animations.RemoveAt(i);
                }
                else
                {
                    animations[i].updatePosition();
                }
            }
            base.Update(delta);
        }

        public bool isOnScreen(Point gamePosition)
        {
            return Program.rootScreen._mapDisplay.isOnScreen(gamePosition);
        }


        private class Animation : ScreenObject
        {
            AnimatedScreenSurface animation;
            Point gamePosition;
            public Animation(CellSurface[] frames, Point gamePosition)
            {
                this.gamePosition = gamePosition;
                animation = new AnimatedScreenSurface("animation", frames[0].Width, frames[0].Height);
                for(int i = 0; i < frames.Length; i++)
                {
                    var frame = animation.CreateFrame();
                    CellSurfaceEditor.Copy(frames[i], frame);
                }
                animation.AnimationDuration = frames.Length;
                animation.Repeat = false;
                updatePosition();
                animation.Start();
                Children.Add(animation);
            }

            public bool isPlaying { 
                get
                {
                    return animation.IsPlaying;
                }
                private set { }
            }

            public void updatePosition()
            {
                animation.Position = Program.rootScreen._mapDisplay.gameCoordsToSurfaceCoords(gamePosition) + new Point(2, 1);

            }
        }

    }
}
