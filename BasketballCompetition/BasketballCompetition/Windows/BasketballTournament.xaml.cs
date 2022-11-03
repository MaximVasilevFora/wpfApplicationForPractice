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
using Word = Microsoft.Office.Interop.Word;

namespace BasketballCompetition.Windows
{
	/// <summary>
	/// Interaction logic for BasketballTournament.xaml
	/// </summary>
	public partial class BasketballTournament : Window
	{
		public static BasketballTournament basketballTournament;
		public Windows.AddWindowClub addWindowClub;

		public BasketballTournament()
		{
			InitializeComponent();
			InitPage(@"Pages/Club.xaml");
			basketballTournament = this;
		}

		public void InitPage(string uri)
		{
			FrameOfDbTable.NavigationService.Navigate(new Uri(uri, UriKind.Relative));
		}

		private void Clubs_Click(object sender, RoutedEventArgs e)
		{
			InitPage(@"Pages/Club.xaml");
		}

		private void Players_Click(object sender, RoutedEventArgs e)
		{
			InitPage(@"Pages/Player.xaml");
		}

		private void PlayersHistory_Click(object sender, RoutedEventArgs e)
		{
			InitPage(@"Pages/PlayerHistory.xaml");
		}

		private void Coachs_Click(object sender, RoutedEventArgs e)
		{
			InitPage(@"Pages/Coach.xaml");
		}

		private void Transports_Click(object sender, RoutedEventArgs e)
		{
			InitPage(@"Pages/Transport.xaml");
		}

		private void TrainingHalls_Click(object sender, RoutedEventArgs e)
		{
			InitPage(@"Pages/TrainingHall.xaml");
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Add_Click(object sender, RoutedEventArgs e)
		{
			addWindowClub = new Windows.AddWindowClub();
			addWindowClub.Show();
		}

		/// <summary>
		/// Экспортировать данные в Word
		/// </summary>
		private void Export_Click(object sender, RoutedEventArgs e)
		{
			var clubs = new List<Club>();

			using (var entity = new BasketballSquadEntities())
			{
				clubs = entity.Clubs.ToList();

				var app = new Word.Application();
				Word.Document document = app.Documents.Add();

				Word.Paragraph tableParagraph = document.Paragraphs.Add();
				Word.Range tableRange = tableParagraph.Range;

				Word.Table clubTable = document.Tables.Add(tableRange, clubs.Count() + 1, 6);

				clubTable.Borders.InsideLineStyle =
					clubTable.Borders.InsideLineStyle =
					Word.WdLineStyle.wdLineStyleSingle;

				clubTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

				Word.Range cellRange = clubTable.Cell(1, 1).Range;
				cellRange.Text = "Идентификатор";

				cellRange = clubTable.Cell(1, 2).Range;
				cellRange.Text = "Наименование";

				cellRange = clubTable.Cell(1, 3).Range;
				cellRange.Text = "Дата создания";

				cellRange = clubTable.Cell(1, 4).Range;
				cellRange.Text = "Город";

				cellRange = clubTable.Cell(1, 5).Range;
				cellRange.Text = "Идентификатор спонсора";

				cellRange = clubTable.Cell(1, 6).Range;
				cellRange.Text = "Идентификатор тренера";

				clubTable.Rows[1].Range.Bold = 1;
				clubTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

				int index = 1;

				foreach (var currentClub in clubs)
				{
					cellRange = clubTable.Cell(index + 1, 1).Range;
					cellRange.Text = currentClub.Id.ToString();
					cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

					cellRange = clubTable.Cell(index + 1, 2).Range;
					cellRange.Text = currentClub.Name.ToString();
					cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

					cellRange = clubTable.Cell(index + 1, 3).Range;
					cellRange.Text = currentClub.CreateDate.ToString();
					cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

					cellRange = clubTable.Cell(index + 1, 4).Range;
					cellRange.Text = currentClub.City.ToString();
					cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

					cellRange = clubTable.Cell(index + 1, 5).Range;
					cellRange.Text = currentClub.IdSponsor.ToString();
					cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

					cellRange = clubTable.Cell(index + 1, 6).Range;
					cellRange.Text = currentClub.IdCoach.ToString();
					cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

					index++;
				}

				app.Visible = true;
				document.SaveAs2(@"D:\outputFileWord.docx");
				document.SaveAs2(@"D:\outputFileWord.pdf", Word.WdExportFormat.wdExportFormatPDF);
			}
		}
	}
}
