using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BasketballCompetition.Windows
{
	/// <summary>
	/// Interaction logic for EditWindowClub.xaml
	/// </summary>
	public partial class EditWindowClub : Window
	{
		private Club _club;
		private Pages.Club _clubPage;
		private Object _object;
		private int index = 0;
		
		public EditWindowClub(object clubObject, Pages.Club club)
		{
			InitializeComponent();

			_clubPage = club;
			_object = clubObject;

			using (var basketballSquadEntities = new BasketballSquadEntities())
			{
				CoachBox.ItemsSource = basketballSquadEntities.Coaches.ToList();
			}

			InitWindowClub(clubObject);
		}

		public void InitWindowClub(object objectItem)
		{
			_club = (objectItem as Button).DataContext as Club;
			NameBox.Text = _club.Name;
			CreateBox.Text = _club.CreateDate.ToString();
			CityBox.Text = _club.City;

			using (var entity = new BasketballSquadEntities())
			{
				foreach (var item in entity.Coaches)
				{
					if (_club.Coach.FCs == item.FCs)
					{
						CoachBox.SelectedIndex = index;
					}

					index++;
				}
			}
		}

		private void RemoveClub_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult messageBoxResult =  MessageBox.Show("Вы действительно хотите удалить клуб?",
				"RemoveClub",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question,
				MessageBoxResult.No);

			if (messageBoxResult == MessageBoxResult.Yes)
			{
				using (var context = new BasketballSquadEntities())
				{
					var item = context.Clubs.Find(_club.Id);
					context.Clubs.Remove(item);
					context.SaveChanges();
				}
			}
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult messageBoxResult = MessageBox.Show("Вы действительно хотите изменить данные о клубе?",
				"EditClub",
				MessageBoxButton.YesNo,
				MessageBoxImage.Question,
				MessageBoxResult.No);

			if (messageBoxResult == MessageBoxResult.Yes)
			{
				if (String.IsNullOrEmpty(NameBox.Text))
				{
					MessageBox.Show("Поле наименование должно быть заполнено");
					return;
				}

				if (String.IsNullOrEmpty(CreateBox.Text))
				{
					MessageBox.Show("Поле дата создания должно быть заполнено");
					return;
				}

				if (String.IsNullOrEmpty(CityBox.Text))
				{
					MessageBox.Show("Поле город должно быть заполнено");
					return;
				}

				var coach = (Coach)CoachBox.SelectedItem;

				if (coach == null)
				{
					MessageBox.Show("Выберите тренера");
					return;
				}

				try
				{
					using (var context = new BasketballSquadEntities())
					{
						context.Clubs.Find(_club.Id).Name = NameBox.Text;
						context.Clubs.Find(_club.Id).CreateDate = Convert.ToDateTime(CreateBox.Text);
						context.Clubs.Find(_club.Id).City = CityBox.Text;

						context.Clubs.Find(_club.Id).IdCoach = coach.Id;

						context.SaveChanges();

						MessageBox.Show("Данные успешно отредактированы");

						_clubPage.RefreshDataGrid();

						this.Close();
					}
				}
				catch
				{
					MessageBox.Show("Данные заполнены некорректно");
					InitWindowClub(_object);
				}
			}
		}
	}
}
