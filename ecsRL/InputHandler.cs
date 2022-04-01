using System.Linq;
using SadConsole.Input;

namespace ecsRL
{
    public class InputHandler
    {
        Action unfinished = null;

        public InputHandler() {}

        public void handleInput(Keyboard keyboard)
        {
            Keys key = keyboard.KeysPressed.First().Key;

            if(unfinished == null)
            {
                if(Equals(key, Keys.H))
                    unfinished = new HugAction(Program.player.ID);
                else if(Equals(key, Keys.Left))
                    unfinished = new MovementAction(Program.player.ID, MovementAction.W);
                else if(Equals(key, Keys.Right))
                    unfinished = new MovementAction(Program.player.ID, MovementAction.E);
                else if(Equals(key, Keys.Up))
                    unfinished = new MovementAction(Program.player.ID, MovementAction.N);
                else if(Equals(key, Keys.Down))
                    unfinished = new MovementAction(Program.player.ID, MovementAction.S);
            }
            else
            {
                if(!unfinished.tryTakeInput(key))
                    if(Equals(key, Keys.Escape))
                        return;
                    else
                        unfinished = null;
            }

            if(unfinished == null || !unfinished.isPerformable())
                return;

            Program.player.actions.Push(unfinished);
            unfinished = null;
        }
    }
}
