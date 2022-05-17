using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using iTextSharp.text;
using Image=System.Web.UI.WebControls.Image;
using Table=iTextSharp.text.Table;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public abstract class ITextSharpDataGridExporter : AbstractDataGridExporter
	{
		/// <summary>
		/// Raised before the <see cref="CellType" /> for cells being added to the 
		/// <see cref="iTextSharp.text.Table" /> changes.
		/// </summary>
		/// <remarks>
		/// Useful for setting default cell properties 
		/// (i.e. <see cref="iTextSharp.text.Table.DefaultCellBackgroundColor" />) for groups of cells.
		/// </remarks>
		/// <example>
		///	<code>
		///	private void exporter_CellTypeChange(object source, ITextSharpDataGridExporter.TableEventArgs e)
		///	{
		///		// you must cast ITable to the appropriate type depending on your exporter
		///		PdfPTable pdfPTable = (PdfPTable)e.Table;
		///	
		///		if (e.CellType == ITextSharpDataGridExporter.CellType.Header)
		///		{
		///			pdfPTable.DefaultCell.BackgroundColor = Color.LIGHT_GRAY;
		///		}
		///		else if (e.CellType == ITextSharpDataGridExporter.CellType.Item)
		///		{
		///			pdfPTable.DefaultCell.BackgroundColor = null;
		///		}
		///	}
		///	</code>
		/// </example>
		public event TableEventHandler CellTypeChange;

		/// <summary>
		/// Raised before a <see cref="Cell" /> is added to the <see cref="Table" />.
		/// </summary>
		public event CellEventHandler CellAdd;

		/// <summary>
		/// Raised after the <see cref="iTextSharp.text.Document" /> has been opened.
		/// </summary>
		/// <remarks>
		/// Useful for inserting additional information into the <see cref="iTextSharp.text.Document" />
		/// before the <see cref="DataGrid" /> is exported.
		/// </remarks>
		public event DocumentEventHandler DocumentOpened;

		/// <summary>
		/// Raised before the <see cref="iTextSharp.text.Document" /> is closed.
		/// </summary>
		/// <remarks>
		/// Useful for appending additional information into the <see cref="iTextSharp.text.Document" />
		/// after the <see cref="DataGrid" /> has been exported.
		/// </remarks>
		public event DocumentEventHandler DocumentClose;

		private Document document;
		private HttpContext httpContext;
		private Font itemFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
		private Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD);
		private Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.ITALIC);
		
		/// <summary>
		/// Must be set in constructor so it can be set on every page.
		/// </summary>
		protected HeaderFooter header = null;

		/// <summary>
		/// Must be set in constructor so it can be set on every page.
		/// </summary>
		protected HeaderFooter footer = null;

		protected ITextSharpDataGridExporter(DataGrid dataGrid, HttpContext httpContext)
			: base(dataGrid)
		{
			this.httpContext = httpContext;
			document = new Document();

			footer = new HeaderFooter(new Phrase("Page: ", ItemFont), true);
			footer.Alignment = Rectangle.ALIGN_CENTER;
			footer.Border = Rectangle.NO_BORDER;
		}

		protected ITextSharpDataGridExporter(DataGrid dataGrid, HttpContext httpContext, HeaderFooter header, HeaderFooter footer)
			: base(dataGrid)
		{
			this.httpContext = httpContext;
			this.header = header;
			this.footer = footer;
		}

		protected abstract void DocWriterGetInstance(Document document, Stream stream);

		protected abstract ITable CreateTable(int columns);

		public override void Export(Stream stream)
		{
			int firstColumn = FirstVisibleColumnWithHeaderText();
			int lastColumn = LastVisibleColumnWithHeaderText();

			DocWriterGetInstance(document, stream);
			
			document.Header = header;
			document.Footer = footer;

			document.Open();
			OnDocumentOpened(document);

			ITable table = CreateTable(visibleColumnsCache.Count);
			
			if (DataGrid.ShowHeader)
			{
				addRow(table, firstColumn, lastColumn, GetHeader(), CellType.Header, HeaderFont);
			}

			foreach (DataGridItem dataGridItem in DataGrid.Items)
			{
				addRow(table, firstColumn, lastColumn, dataGridItem, CellType.Item, ItemFont);
			}

			if (DataGrid.ShowFooter)
			{
				addRow(table, firstColumn, lastColumn, GetFooter(), CellType.Footer, FooterFont);
			}

			document.Add(table.Table);

			OnDocumentClose(document);
			document.Close();
		}

		private void addRow(ITable table, int firstColumn, int lastColumn, DataGridItem dataGridItem, CellType cellType, Font font)
		{
			OnCellTypeChange(table, cellType);
			for (int i = firstColumn; i <= lastColumn; i++)
			{
				if (VisibleColumnWithHeaderText(i))
				{
					ICell cell = table.CreateCell();
					if (cellType == CellType.Header)
					{
						cell.GrayFill = .95f;
					}
					cell.HorizontalAlignment = convertToCellHorizontalAlignment(cellType, i);

					string tableCellText = RemoveGeneratedHtmlWhiteSpace(dataGridItem.Cells[i].Text);
					if (tableCellText.Length == 0)
					{
						addElements(cell, dataGridItem.Cells[i], font);
					}
					else
					{
						addElement(cell, tableCellText, font);
					}
					OnCellAdd(table, cellType, cell, table.RowCount, i);
					table.AddCell(cell);
				}
			}
		}

		private void addElement(ICell cell, string text, Font font)
		{
			Paragraph paragraph = new Paragraph();
			paragraph.Leading = ((font.Size * 1.5f) * .95f);
			paragraph.Alignment = cell.HorizontalAlignment;
			addText(paragraph, text, font);
			cell.AddElement(paragraph);
		}

		private void addElements(ICell cell, TableCell tableCell, Font font)
		{
			Paragraph paragraph = new Paragraph();
			paragraph.Leading = ((font.Size * 1.5f) * .95f);
			paragraph.Alignment = cell.HorizontalAlignment;

			foreach (Control control in tableCell.Controls)
			{
				if (control is Image)
				{
					Image image = (Image)control;

					if (image.ImageUrl.ToLower().StartsWith("http") == false)
					{
						iTextSharp.text.Image iTextImage = iTextSharp.text.Image.GetInstance(httpContext.Server.MapPath(image.ImageUrl));
						iTextImage.ScalePercent(60); // ???
						addImage(paragraph, iTextImage);
						addText(paragraph, "  ", font); // spacer ???
					}
					else
					{
						if (image.AlternateText.Length > 0)
						{
							addText(paragraph, image.AlternateText, font);
						}
					}
				}
				else if (control is HtmlImage)
				{
					HtmlImage htmlImage = (HtmlImage)control;

					if (htmlImage.Src.ToLower().StartsWith("http") == false)
					{
						iTextSharp.text.Image iTextImage = iTextSharp.text.Image.GetInstance(httpContext.Server.MapPath(htmlImage.Src));
						iTextImage.ScalePercent(60); // ???
						addImage(paragraph, iTextImage);
						addText(paragraph, "  ", font); // spacer ???
					}
					else
					{
						if (htmlImage.Alt.Length > 0)
						{
							addText(paragraph, htmlImage.Alt, font);
						}
					}
				}
				else
				{
					string controlText = GetControlText(control);

					if (controlText.Length > 0 && controlText.StartsWith(Environment.NewLine) == false)
					{
						addText(paragraph, controlText, font);
					}
				}
			}

			cell.AddElement(paragraph);
		}

		private void addText(Paragraph paragraph, string text, Font font)
		{
			paragraph.Add(new Chunk(text, font));
		}

		private void addImage(Paragraph paragraph, iTextSharp.text.Image image)
		{
			paragraph.Add(new Chunk(image, 0, 0));
		}
		
		private int convertToCellHorizontalAlignment(CellType cellType, int columnIndex)
		{
			int horizontalAlignment = Cell.ALIGN_UNDEFINED;

			HorizontalAlign webControlHorizontalAlign = HorizontalAlign.NotSet;

			if (cellType == CellType.Header)
			{
				#region HeaderStyle

				if (DataGrid.Columns[columnIndex].HeaderStyle.HorizontalAlign != HorizontalAlign.NotSet)
				{
					webControlHorizontalAlign = DataGrid.Columns[columnIndex].HeaderStyle.HorizontalAlign;
				}
				else if (DataGrid.HeaderStyle.HorizontalAlign != HorizontalAlign.NotSet)
				{
					webControlHorizontalAlign = DataGrid.HeaderStyle.HorizontalAlign;
				}

				#endregion
			}
			else if (cellType == CellType.Item)
			{
				#region ItemStyle

				if (DataGrid.Columns[columnIndex].ItemStyle.HorizontalAlign != HorizontalAlign.NotSet)
				{
					webControlHorizontalAlign = DataGrid.Columns[columnIndex].ItemStyle.HorizontalAlign;
				}
				else if (DataGrid.ItemStyle.HorizontalAlign != HorizontalAlign.NotSet)
				{
					webControlHorizontalAlign = DataGrid.ItemStyle.HorizontalAlign;
				}

				#endregion
			}
			else if (cellType == CellType.Footer)
			{
				#region FooterStyle

				if (DataGrid.Columns[columnIndex].FooterStyle.HorizontalAlign != HorizontalAlign.NotSet)
				{
					webControlHorizontalAlign = DataGrid.Columns[columnIndex].FooterStyle.HorizontalAlign;
				}
				else if (DataGrid.FooterStyle.HorizontalAlign != HorizontalAlign.NotSet)
				{
					webControlHorizontalAlign = DataGrid.FooterStyle.HorizontalAlign;
				}

				#endregion
			}

			if (webControlHorizontalAlign != HorizontalAlign.NotSet)
			{
				switch (webControlHorizontalAlign)
				{
					case HorizontalAlign.Center:
						horizontalAlignment = Element.ALIGN_CENTER;
						break;
					case HorizontalAlign.Left:
						horizontalAlignment = Element.ALIGN_LEFT;
						break;
					case HorizontalAlign.Right:
						horizontalAlignment = Element.ALIGN_RIGHT;
						break;
				}
			}

			return horizontalAlignment;
		}

		#region Raise Events

		protected void OnCellAdd(ITable table, CellType cellType, ICell cell, int rowIndex, int columnIndex)
		{
			if (CellAdd != null)
			{
				CellAdd(this, new CellEventArgs(table, cellType, cell, rowIndex, columnIndex));
			}
		}

		protected void OnCellTypeChange(ITable table, CellType cellType)
		{
			if (CellTypeChange != null)
			{
				CellTypeChange(this, new TableEventArgs(table, cellType));
			}
		}

		protected void OnDocumentOpened(Document d)
		{
			if (DocumentOpened != null)
			{
				DocumentOpened(this, new DocumentEventArgs(d));
			}
		}

		protected void OnDocumentClose(Document d)
		{
			if (DocumentClose != null)
			{
				DocumentClose(this, new DocumentEventArgs(d));
			}
		}

		#endregion

		#region EventHandlers

		public delegate void DocumentEventHandler(object source, DocumentEventArgs e);

		public class DocumentEventArgs : EventArgs
		{
			private Document document;

			public DocumentEventArgs(Document document)
			{
				this.document = document;
			}

			public Document Document
			{
				get { return document; }
			}
		}

		public delegate void TableEventHandler(object source, TableEventArgs e);

		public class TableEventArgs : EventArgs
		{
			private ITable table;
			private CellType cellType;

			public TableEventArgs(ITable table, CellType cellType)
			{
				this.table = table;
				this.cellType = cellType;
			}

			public ITable Table
			{
				get { return table; }
			}

			public CellType CellType
			{
				get { return cellType; }
			}
		}

		public delegate void CellEventHandler(object source, CellEventArgs e);

		public class CellEventArgs : TableEventArgs
		{
			private ICell cell;
			private int rowIndex;
			private int columnIndex;

			public CellEventArgs(ITable table, CellType cellType, ICell cell, int rowIndex, int columnIndex)
				: base(table, cellType)
			{
				this.cell = cell;
				this.rowIndex = rowIndex;
				this.columnIndex = columnIndex;
			}

			public ICell Cell
			{
				get { return cell; }
			}

			public int RowIndex
			{
				get { return rowIndex; }
			}

			public int ColumnIndex
			{
				get { return columnIndex; }
			}
		}

		public enum CellType
		{
			Header,
			Item,
			Footer
		}

		#endregion

		#region Public Properties

		public Document Document
		{
			get { return document; }
		}

		public Font ItemFont
		{
			get { return itemFont; }
			set { itemFont = value; }
		}

		public Font HeaderFont
		{
			get { return headerFont; }
			set { headerFont = value; }
		}

		public Font FooterFont
		{
			get { return footerFont; }
			set { footerFont = value; }
		}

		#endregion
	}
}