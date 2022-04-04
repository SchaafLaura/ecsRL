using System.Collections.Generic;
using System.Linq;
using SadConsole.Input;

namespace ecsRL
{
    public class InputHandler
    {
        Action unfinished = null;
        Dictionary<Keys, Action> keyToActionMap;

        public InputHandler() 
        {
            initKeyToActionMap();
        }

        public void handleInput(Keyboard keyboard)
        {
            Keys key = keyboard.KeysPressed.First().Key;

            if(unfinished == null)
            {
                if(keyToActionMap.ContainsKey(key))
                    unfinished = keyToActionMap[key].clone();
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

        public void initKeyToActionMap()
        {
            keyToActionMap = new Dictionary<Keys, Action>();

            keyToActionMap[Keys.Up] = new MovementAction(Program.player.ID, MovementAction.N);
            keyToActionMap[Keys.Down] = new MovementAction(Program.player.ID, MovementAction.S);
            keyToActionMap[Keys.Left] = new MovementAction(Program.player.ID, MovementAction.W);
            keyToActionMap[Keys.Right] = new MovementAction(Program.player.ID, MovementAction.E);

            keyToActionMap[Keys.H] = new HugAction(Program.player.ID);

            keyToActionMap[Keys.A] = new AttackAction(Program.player.ID);

            keyToActionMap[Keys.W] = new MovementAction(Program.player.ID, MovementAction.O);
        }
    }
}
