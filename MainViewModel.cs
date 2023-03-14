using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace SimpleUndoProject.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private Stack<Command> _undoStack = new Stack<Command>();
		private Stack<Command> _redoStack = new Stack<Command>();

		private string _text;

		private MyTestObject myTestObject;

		public MainViewModel()
		{
			_text = "Initial Text";
			myTestObject = new MyTestObject() { Text = _text };
		}

		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
				{
					_text = value;
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

		public ICommand ChangeTextCommand
		{
			get { return new RelayCommand(ChangeText); }
		}

		private void ChangeText()
		{
			Command command = new ChangeTextCommand(myTestObject, _text);
			command.Execute();
			_undoStack.Push(command);
			_redoStack.Clear();

			NotifyPropertyChanged(nameof(MyObjectText));
		}

		public ICommand UndoCommand
		{
			get { return new RelayCommand(Undo, CanUndo); }
		}

		private bool CanUndo()
		{
			return _undoStack.Count > 0;
		}

		private void Undo()
		{
			Command command = _undoStack.Pop();
			command.Unexecute();
			_redoStack.Push(command);

			NotifyPropertyChanged(nameof(MyObjectText));
		}

		public ICommand RedoCommand
		{
			get { return new RelayCommand(Redo, CanRedo); }
		}

		private bool CanRedo()
		{
			return _redoStack.Count > 0;
		}

		private void Redo()
		{
			Command command = _redoStack.Pop();
			command.Execute();
			_undoStack.Push(command);

			NotifyPropertyChanged(nameof(MyObjectText));
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}
