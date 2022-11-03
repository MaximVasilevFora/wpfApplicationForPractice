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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasketballCompetition.Pages
{
	/// <summary>
	/// Interaction logic for Club.xaml
	/// </summary>
	public partial class Club : UserControl
	{
		public BasketballSquadEntities basketballSquadEntities = new BasketballSquadEntities();
		public Windows.EditWindowClub editWindowClub;
		public Windows.BasketballTournament basketballTournament = Windows.BasketballTournament.basketballTournament;

		public Club()
		{
			InitializeComponent();

			DataGridClubs.ItemsSource = basketballSquadEntities.Clubs.ToList();
		}

		public void RefreshDataGrid()
		{
			basketballTournament.InitPage(@"Pages/Club.xaml");
			DataGridClubs.Items.Refresh();
		}

		private void EditColumn_Click(object sender, RoutedEventArgs e)
		{
			editWindowClub = new Windows.EditWindowClub(sender, this);
			editWindowClub.Show();
		}
	}
}
