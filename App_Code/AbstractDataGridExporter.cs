using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GeneralClass
{
	/// <summary>
	/// Provides base functionality for exporting a <see cref="System.Web.UI.WebControls.DataGrid" /> to another format.
	/// </summary>
	/// <author>Ron Grabowski</author>
	public abstract class AbstractDataGridExporter
	{
		private DataGrid dataGrid;

		/// <summary>
		/// Initialized when <see cref="VisibleColumnWithHeaderText" /> is called for the first time. It 
		/// contains integers that represent valid column indexes.
		/// </summary>
		protected IDictionary visibleColumnsCache;

		/// <summary>
		/// Creates an exporter using the supplied <paramref name="dataGrid" />.
		/// </summary>
		/// <param name="dataGrid">The <see cref="DataGrid" /> to export.</param>
		public AbstractDataGridExporter(DataGrid dataGrid)
		{
			this.dataGrid = dataGrid;
		}

		/// <summary>
		/// Exports the <see cref="DataGrid" /> to the specified <paramref name="fileName" />.
		/// </summary>
		public virtual void Export(string fileName)
		{
			using (FileStream fileStream = File.Create(fileName))
			{
				Export(fileStream);
			}
		}

		/// <summary>
		/// Concrete classes must implement this method such that the <see cref="DataGrid" /> is written
		/// to the supplied <paramref name="stream" />.
		/// </summary>
		/// <param name="stream"></param>
		public abstract void Export(Stream stream);

		public abstract string ContentType { get; }

		/// <summary>
		/// Starting with the <see cref="DataGridColumn" /> at position 0, locates the 
		/// first <see cref="DataGridColumn" /> that is <see cref="DataGridColumn.Visible" />
		/// and has a non-empty <see cref="DataGridColumn.HeaderText" /> property.
		/// </summary>
		/// <returns>
		/// Returns 0 if all columns have their <see cref="DataGridColumn.Visible" /> property set
		/// to false or have empty <see cref="DataGridColumn.HeaderText" /> properties.
		/// </returns>
		protected virtual int FirstVisibleColumnWithHeaderText()
		{
			for (int i = 0; i < dataGrid.Columns.Count; i++)
			{
				if (VisibleColumnWithHeaderText(i))
				{
					return i;
				}
			}

			return 0;
		}

		/// <summary>
		/// Starting with the last <see cref="DataGridColumn" />, locates the first <see cref="DataGridColumn" />
		/// that is <see cref="DataGridColumn.Visible" /> and has a non-empty 
		/// <see cref="DataGridColumn.HeaderText" /> property.
		/// </summary>
		/// <returns>
		/// Returns the index of the last column if all columns have their
		/// <see cref="DataGridColumn.Visible" /> property set to false or have 
		/// empty <see cref="DataGridColumn.HeaderText" /> properties.
		/// </returns>
		protected virtual int LastVisibleColumnWithHeaderText()
		{
			for (int i = dataGrid.Columns.Count - 1; i >= 0; i--)
			{
				if (VisibleColumnWithHeaderText(i))
				{
					return i;
				}
			}

			return dataGrid.Columns.Count - 1;
		}

		/// <summary>
		/// Determines if the specified <see cref="DataGridColumn" /> is <see cref="DataGridColumn.Visible" />
		/// and has a non-empty <see cref="DataGridColumn.HeaderText" /> property.
		/// </summary>
		/// <remarks>
		/// Non-empty <see cref="DataGridColumn.HeaderText" /> is defined as having a length
		/// grater than 1 and some value besides "&amp;nbsp;".
		/// </remarks>
		/// <param name="columnIndex">The index into the Columns collection.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown when <paramref name="columnIndex" /> is less than 0 or greater than the column index
		/// count.
		/// </exception>
		protected virtual bool VisibleColumnWithHeaderText(int columnIndex)
		{
			if (columnIndex < 0 || columnIndex > DataGrid.Columns.Count - 1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}

			if (visibleColumnsCache == null)
			{
				visibleColumnsCache = new HybridDictionary(DataGrid.Columns.Count);

				for (int i = 0; i < DataGrid.Columns.Count; i++)
				{
					DataGridColumn column = DataGrid.Columns[i];

					if (column.Visible)
					{
						string headerText = column.HeaderText;
						if (headerText.Length > 1 && headerText != "&nbsp;")
						{
							visibleColumnsCache.Add(i, null);
						}
					}
				}
			}

			return visibleColumnsCache.Contains(columnIndex);
		}

		#region GetControlText
		/// <summary>
		/// Determines the appropriate base type of <paramref name="control" /> and calls the 
		/// corresponding method (<see cref="GetLiteralControlText" />, <see cref="GetLiteralText" />,
		/// etc.). If no suitable control handler is found, <see cref="GetUnknownControlText" /> is called.
		/// </summary>
		/// <param name="control"></param>
		/// <returns>The text of <paramref name="control" />.</returns>
		protected virtual string GetControlText(Control control)
		{
			string controlText;

			if (control is LiteralControl)
			{
				controlText = GetLiteralControlText((LiteralControl)control);
			}
			else if (control is Literal)
			{
				controlText = GetLiteralText((Literal)control);
			}
			else if (control is Label)
			{
				controlText = GetLabelText((Label)control);
			}
			else if (control is HyperLink)
			{
				controlText = GetHyperLinkText((HyperLink)control);
			}
			else if (control is ListControl)
			{
				controlText = GetListControlText((ListControl)control);
			}
			else if (control is LinkButton)
			{
				controlText = GetLinkButtonText((LinkButton)control);
			}
			else if (control is TextBox)
			{
				controlText = GetTextBoxText((TextBox)control);
			}
			else if (control is HtmlAnchor)
			{
				controlText = GetHtmlAnchorText((HtmlAnchor)control);
			}
			else if (control is Image)
			{
				controlText = GetImageText((Image)control);
			}
			else if (control is HtmlImage)
			{
				controlText = GetHtmlImageText((HtmlImage)control);
			}
			else
			{
				controlText = GetUnknownControlText(control);
			}

			if (control.HasControls())
			{
				controlText += GetControlCollectionText(control.Controls);
			}

			return RemoveGeneratedHtmlWhiteSpace(controlText);
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="literalControl" />.<see cref="LiteralControl.Text" /></returns>
		protected virtual string GetLiteralControlText(LiteralControl literalControl)
		{
			return literalControl.Text;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="literal" />.<see cref="Literal.Text" /></returns>
		protected virtual string GetLiteralText(Literal literal)
		{
			return literal.Text;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="label" />.<see cref="Label.Text" /></returns>
		protected virtual string GetLabelText(Label label)
		{
			return label.Text;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="hyperLink" />.<see cref="HyperLink.Text" /></returns>
		protected virtual string GetHyperLinkText(HyperLink hyperLink)
		{
			return hyperLink.Text;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="listControl" />.<see cref="ListControl.SelectedValue" /></returns>
		protected virtual string GetListControlText(ListControl listControl)
		{
			return listControl.SelectedValue;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="linkButton" />.<see cref="LinkButton.Text" /></returns>
		protected virtual string GetLinkButtonText(LinkButton linkButton)
		{
			return linkButton.Text;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="textBox" />.<see cref="TextBox.Text" /></returns>
		protected virtual string GetTextBoxText(TextBox textBox)
		{
			return textBox.Text;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		protected virtual string GetHtmlAnchorText(HtmlAnchor htmlAnchor)
		{
			return htmlAnchor.InnerText;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		protected virtual string GetImageText(Image image)
		{
			if (image.AlternateText.Length == 0)
			{
				return image.ToolTip;
			}
			else
			{
				return image.AlternateText;
			}
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><paramref name="htmlImage" />.<see cref="HtmlImage.Alt" /></returns>
		protected virtual string GetHtmlImageText(HtmlImage htmlImage)
		{
			return htmlImage.Alt;
		}

		/// <summary>
		/// Converts the value of the specified control into a <see cref="String" />.
		/// </summary>
		/// <returns><see cref="String.Empty" /></returns>
		protected virtual string GetUnknownControlText(Control control)
		{
			return string.Empty;
		}
		#endregion

		/// <summary>
		/// Retrieves the rendered text from the specified cell.
		/// </summary>
		/// <remarks>
		/// ASP.Net generated whitespace is removed by calling the
		/// <see cref="RemoveGeneratedHtmlWhiteSpace" /> function before
		/// the text is returned.
		/// </remarks>
		/// <returns>
		/// An empty string (<see cref="String.Empty" />) will be returned if the 
		/// cell does not have any content.
		/// </returns>
		protected virtual string GetControlCollectionText(ControlCollection controlCollection)
		{
			string controlCollectionText = string.Empty;

			foreach (Control control in controlCollection)
			{
				string controlText = GetControlText(control);

				if (controlText.Length > 0 && controlText.StartsWith(Environment.NewLine) == false)
				{
					controlCollectionText += controlText;
				}
			}

			return RemoveGeneratedHtmlWhiteSpace(controlCollectionText);
		}

		/// <summary>
		/// Removes ASP.Net generated whitespace characters
		/// (<see cref="Environment.NewLine" />, tab character, &amp;nbsp;) from <paramref name="s" />.
		/// </summary>
		protected virtual string RemoveGeneratedHtmlWhiteSpace(string s)
		{
			if (s.Length > 0)
			{
				return s.Trim(new char[] {'\r', '\n', '\t'}).Replace("&nbsp;", string.Empty);
			}
			else
			{
				return s;
			}
		}

		/// <summary>
		/// Retrieves the <see cref="DataGridItem" /> marked with an
		/// <see cref="DataGridItem.ItemType" /> of <see cref="ListItemType.Header" />.
		/// </summary>
		/// <returns>
		/// Returns null if <see cref="System.Web.UI.WebControls.DataGrid.ShowHeader" /> is false.
		/// </returns>
		protected virtual DataGridItem GetHeader()
		{
			DataGridItem header = null;

			if (DataGrid.ShowHeader)
			{
				DataGridItem dataGridItem = (DataGridItem) DataGrid.Controls[0].Controls[0];

				if (dataGridItem != null && dataGridItem.ItemType == ListItemType.Header)
				{
					header = dataGridItem;
				}
			}

			return header;
		}

		/// <summary>
		/// Retrieves the <see cref="DataGridItem" /> marked with an
		/// <see cref="DataGridItem.ItemType" /> of <see cref="ListItemType.Footer" />.
		/// </summary>
		/// <returns>
		/// Returns null if <see cref="System.Web.UI.WebControls.DataGrid.ShowFooter" /> is false.
		/// </returns>
		protected virtual DataGridItem GetFooter()
		{
			DataGridItem footer = null;

			if (DataGrid.ShowFooter)
			{
				// the very last control is sometimes a Pager
				for (int i = DataGrid.Controls[0].Controls.Count - 1; i >= 0; i--)
				{
					DataGridItem dataGridItem = (DataGridItem) DataGrid.Controls[0].Controls[i];

					if (dataGridItem.ItemType == ListItemType.Footer)
					{
						footer = dataGridItem;
						break;
					}
				}
			}

			return footer;
		}

		/// <summary>
		/// The <see cref="System.Web.UI.WebControls.DataGrid" /> to export.
		/// </summary>
		public DataGrid DataGrid
		{
			get { return dataGrid; }
		}
	}
}
