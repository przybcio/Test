using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using StockGuiWpf.Abstract;
using StockGuiWpf.Infrastructure;
using StockGuiWpf.Model;

namespace StockGuiWpf
{
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		System.Timers.Timer timer;
		private ObservableCollection<StockRate> items;
		private NotifyCollectionChangedWrapper<StockRate> itemsWrapper;
		public NotifyCollectionChangedWrapper<StockRate> ItemsWrapper
		{
			get
			{
				return itemsWrapper;
			}
			set
			{
				itemsWrapper = value;
				RaisePropertyChangedEvent("ItemsWrapper");
			}
		}



		public MainWindow()
		{
			InitializeComponent();
		}

		private IRatesRepository repository;

		private void uxMainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			//repository = new FakeRatesRepository();
			repository = new StooqService();
			items = new ObservableCollection<StockRate>();
			ItemsWrapper = new NotifyCollectionChangedWrapper<StockRate>(items);

			// Create a timer that adds an element to the list every two seconds *on seperate threads*
			timer = new System.Timers.Timer(30000);
			timer.Elapsed +=
				new System.Timers.ElapsedEventHandler
					(
					(timerSender, timerArgs) =>
						ThreadPool.QueueUserWorkItem
							(
							new WaitCallback
								(
								AddRate
						//(obj) => ItemsWrapper.Add(repository.GetRate())
								)
							)
					);
			timer.Start();
		}

		private void AddRate(object state)
		{
			if (ItemsWrapper.Count > 20)
				ItemsWrapper.RemoveAt(0);
			ItemsWrapper.Add(repository.GetRate());
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChangedEvent(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}
