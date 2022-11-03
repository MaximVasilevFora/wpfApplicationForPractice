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
	/// Interaction logic for AddWindowClub.xaml
	/// </summary>
	public partial class AddWindowClub : Window
	{
		public AddWindowClub()
		{
			InitializeComponent();

			using (var basketballSquadEntities = new BasketballSquadEntities())
			{
				CoachBox.ItemsSource = basketballSquadEntities.Coaches.ToList();
			}
		}

		private void AddClub_Click(object sender, RoutedEventArgs e)
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
				using (var entity = new BasketballSquadEntities())
				{
					var club = new Club();
					club.Name = NameBox.Text;
					club.City = CityBox.Text;
					club.CreateDate = Convert.ToDateTime(CreateBox.Text);
					club.IdCoach = coach.Id;
					club.IdSponsor = 1;

					entity.Clubs.Add(club);
					entity.SaveChanges();

					MessageBox.Show("Клуб был добавлен в список");

					this.Close();
				}
			}
			catch
			{
				MessageBox.Show("Заполните корректно нужные поля");
			}
			
		}
	}
}
