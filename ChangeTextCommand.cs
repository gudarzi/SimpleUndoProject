namespace SimpleUndoProject
{
	public class ChangeTextCommand : Command
	{
		private MyTestObject _myTestObject;
		private string _oldText;
		private string _newText;

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
