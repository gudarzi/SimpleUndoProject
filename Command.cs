﻿namespace SimpleUndoProject
{
	public abstract class Command
	{
		public abstract void Execute();
		public abstract void Unexecute();
	}
}
