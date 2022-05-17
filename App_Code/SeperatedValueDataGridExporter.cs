using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public class SeperatedValueDataGridExporter : AbstractDataGridExporter
	{
		private string seperator = null;
		private string escapedSeperator = null;

		/// <summary>
		///	Creates an exporter with <see cref="Seperator" /> set to "," and 
		///	<see cref="EscapedSeperator" /> set to "_".
		/// </summary>
		/// <param name="dataGrid">The <see cref="DataGrid" /> to export.</param>
		public SeperatedValueDataGridExporter(DataGrid dataGrid)
			: base(dataGrid)
		{
			seperator = ",";
			escapedSeperator = "_";
		}

		/// <summary>
		/// Creates an exporter that exports <pararef name="dataGrid" /> to a text file seperated by
		/// <parameref name="seperator" />.
		/// </summary>
		/// <param name="dataGrid"></param>
		/// <param name="seperator"></param>
		/// <param name="escapedSeperator"></param>
		public SeperatedValueDataGridExporter(DataGrid dataGrid, string seperator, string escapedSeperator)
			: base(dataGrid)
		{
			this.seperator = seperator;
			this.escapedSeperator = escapedSeperator;
		}

		/// <summary>
		/// Exports <see cref="DataGrid" /> to a <see cref="string" />.
		/// </summary>
		/// <returns></returns>
		public string Export()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Export(memoryStream);
				return Encoding.ASCII.GetString(memoryStream.ToArray());
			}
		}

		public override string ContentType
		{
			get { return "text/plain"; }
		}

		/// <summary>
		/// Exports <see cref="DataGrid" /> to the <see cref="Stream" /> specified by <paramref name="stream" />.
		/// </summary>
		/// <param name="stream"></param>
		public override void Export(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				int firstColumn = FirstVisibleColumnWithHeaderText();
				int lastColumn = LastVisibleColumnWithHeaderText();

				if (DataGrid.ShowHeader)
				{
					addRow(sw, firstColumn, lastColumn, GetHeader());
				}

				foreach (DataGridItem dataGridItem in DataGrid.Items)
				{
					addRow(sw, firstColumn, lastColumn, dataGridItem);
				}

				if (DataGrid.ShowFooter)
				{
					addRow(sw, firstColumn, lastColumn, GetFooter());
				}
			}
		}

		private void addRow(StreamWriter writer, int firstColumn, int lastColumn, DataGridItem dataGridItem)
		{
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				if (VisibleColumnWithHeaderText(i))
				{
					string cellText = dataGridItem.Cells[i].Text;

					if (cellText.Length == 0)
					{
						cellText = GetControlText(dataGridItem.Cells[i]);
					}

					writer.Write(SeperatedValueEncode(cellText));

					if (i != lastColumn)
					{
						writer.Write(Seperator);
					}
				}
			}

			writer.WriteLine();
		}

		/// <summary>
		/// Encodes text for inclusion in a seperated value file. Similiar to 
		/// Server.UrlEncode and Server.HtmlEncode.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public virtual string SeperatedValueEncode(string s)
		{
			if (s.Length > 0 && s.IndexOf(Seperator) >= 0)
			{
				return s.Replace(Seperator, EscapedSeperator);
			}
			else
			{
				return s;
			}
		}

		/// <summary>
		/// The string that seperates items.
		/// </summary>
		public string Seperator
		{
			get { return seperator; }
		}

		/// <summary>
		/// The string that will appear if an item contains the <see cref="Seperator" /> string.
		/// </summary>
		public string EscapedSeperator
		{
			get { return escapedSeperator; }
		}
	}
}
