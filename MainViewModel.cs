using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleUndoProject.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private Stack<Command> undoStack = new Stack<Command>();
		private Stack<Command> redoStack = new Stack<Command>();

		private MyTestObject myTestObject;


		public MainViewModel()
		{
			text = "Initial Text";
			myTestObject = new MyTestObject() { Text = text };
		}

		#region Commands
		public ICommand ChangeTextCommand
		{
			get { return new RelayCommand(ChangeText); }
		}

		public ICommand UndoCommand
		{
			get { return new RelayCommand(Undo, CanUndo); }
		}

		public ICommand RedoCommand
		{
			get { return new RelayCommand(Redo, CanRedo); }
		}
		#endregion

		#region Props
		private string text;
		public string Text
		{
			get { return text; }
			set
			{
				if (text != value)
				{
					text = value;
					NotifyPropertyChanged(nameof(Text));
				}
			}
		}

		public string MyObjectText
		{
			get { return myTestObject.Text; }
			set
			{
				if (myTestObject.Text != value)
				{
					myTestObject.Text = value;
					NotifyPropertyChanged(nameof(MyObjectText));
				}
			}
		}
		#endregion

		private void ChangeText()
		{
			Command command = new ChangeTextCommand(myTestObject, text);
			command.Execute();
			undoStack.Push(command);
			redoStack.Clear();

			NotifyPropertyChanged(nameof(MyObjectText));
		}

		private bool CanUndo()
		{
			return undoStack.Count > 0;
		}

		private bool CanRedo()
		{
			return redoStack.Count > 0;
		}

		private void Undo()
		{
			Command command = undoStack.Pop();
			command.Unexecute();
			redoStack.Push(command);

			NotifyPropertyChanged(nameof(MyObjectText));
		}
		
		private void Redo()
		{
			Command command = redoStack.Pop();
			command.Execute();
			undoStack.Push(command);

			NotifyPropertyChanged(nameof(MyObjectText));
		}

		#region INPC Implementation
		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion

	}
}
