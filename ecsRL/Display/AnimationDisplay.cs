using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SadConsole;
using SadRogue.Primitives;

namespace ecsRL
{
    public class AnimationDisplay : ScreenObject
    {
        List<AnimatedScreenSurface> animationSurfaces;

        public AnimationDisplay()
        {
            animationSurfaces = new List<AnimatedScreenSurface>();
        }

        public void tryAddAnimationAtGamePosition(CellSurface[] frames, Point gamePosition)
        {
            if(isOnScreen(gamePosition))
            {
                AnimatedScreenSurface animation = new AnimatedScreenSurface("animation", frames[0].Width, frames[0].Height);
                for(int i = 0; i < frames.Length; i++)
                {
                    var frame = animation.CreateFrame();
                    CellSurfaceEditor.Copy(frames[i], frame);
                }
                animation.AnimationDuration = frames.Length;
                animation.Repeat = false;
                animation.Position = Program.rootScreen._mapDisplay.gameCoordsToSurfaceCoords(gamePosition) + new Point(2, 1);
                animation.Start();
                animationSurfaces.Add(animation);
                Children.Add(animation);
            }
            else
            {

            }
        }

        public override void Update(TimeSpan delta)
        {
            for(int i = 0; i < animationSurfaces.Count; i++)
            {
                if(!animationSurfaces[i].IsPlaying)
                {
                    Children.Remove(animationSurfaces[i]);
                    animationSurfaces.RemoveAt(i);
                }
            }
            base.Update(delta);
        }

        public bool isOnScreen(Point gamePosition)
        {
            return Program.rootScreen._mapDisplay.isOnScreen(gamePosition);
        }

    }
}
