﻿namespace ecsRL
{
    public class ActionResult
    {
        public Action alternative;  // action to perform instead
        public bool succeeded;

        public static ActionResult success = new ActionResult(true);
        public static ActionResult failure = new ActionResult(false);
        public ActionResult(bool succeeded)
        {
            this.succeeded = succeeded;
            this.alternative = null;
        }

        public ActionResult(Action alternative)
        {
            this.alternative= alternative;
            this.succeeded = true;
        }
    }
}
