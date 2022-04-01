using System.Collections.Generic;
using SadConsole;
using SadRogue.Primitives;
using System;

namespace ecsRL
{
    public class Log
    {
        private class LogItem : IEquatable<LogItem>
        {
            public ColoredString message;

            public LogItem()
            {
                ColoredString part1 = new ColoredString("Test ", Color.Cyan, Color.Transparent);
                ColoredString part2 = new ColoredString("Message ", Color.White, Color.Transparent);
                ColoredString part3 = new ColoredString(":)", Color.HotPink, Color.Transparent);
                message = (part1 + part2 + part3);
            }

            public LogItem(ColoredString message)
            {
                this.message = message;
            }

            public ColoredString toColoredString()
            {
                return message;
            }

            public bool Equals(LogItem other)
            {
                return message.Equals(other.message);
            }
        }

        List<LogItem> items;
        public int numberOfItems;

        public Log()
        {
            items = new List<LogItem>();

            items.Add(new LogItem(new ColoredString("Hello World", Color.Cyan, Color.Transparent)));
            items.Add(new LogItem(new ColoredString("How are you?", Color.HotPink, Color.Transparent)));
            items.Add(new LogItem(new ColoredString("I'm good, how are you?", Color.Cyan, Color.Transparent)));
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());
            items.Add(new LogItem());

            numberOfItems = 12;
        }

        public void log(ColoredString message)
        {
            items.Add(new LogItem(message));
            numberOfItems++;
        }

        public ColoredString get(int i)
        {
            return items[i].toColoredString();
        }

    }
}
