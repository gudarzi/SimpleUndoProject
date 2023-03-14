namespace SimpleUndoProject
{
	public class ChangeTextCommand : Command
	{
		private readonly MyTestObject _myTestObject;
		private readonly string _oldText;
		private readonly string _newText;

		public ChangeTextCommand(MyTestObject myTestObject, string newText)
		{
			_myTestObject = myTestObject;
			_oldText = myTestObject.Text;
			_newText = newText;
		}

		public override void Execute()
		{
			_myTestObject.Text = _newText;
		}

		public override void Unexecute()
		{
			_myTestObject.Text = _oldText;
		}
	}
}
