﻿using Esri.ArcGISRuntime.Portal;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Diplom.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
	{ 

		bool isBusy = false;
		public bool IsBusy
		{
			get => isBusy; 
			set => SetProperty(ref isBusy, value);
		}

		string title = string.Empty;
		public string Title
		{
			get => title; 
			set => SetProperty(ref title, value); 
		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);

			return true;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;

			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}