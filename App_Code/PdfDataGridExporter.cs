using System.Collections;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GeneralClass
{
	/// <summary>
	/// Initializes ITextSharpDataGridExporter using the <see cref="PdfWriter" />.
	/// </summary>
	/// <author>Ron Grabowski</author>
	public class PdfDataGridExporter : ITextSharpDataGridExporter
	{
		/// <summary>
		/// Creates an exporter that will export to PDF using the default footer of "Page N".
		/// </summary>
		public PdfDataGridExporter(DataGrid dataGrid, HttpContext httpContext)
			: base(dataGrid, httpContext)
		{
			// empty
		}

		/// <summary>
		/// Creates an exporter with the text specified in <paramname ref="headerText" /> using 
		/// the font <see cref="DataGridExporter.ITextSharp.ITextSharpDataGridExporter.HeaderFont" />
		/// and the footer of "Page N".
		/// </summary>
		public PdfDataGridExporter(DataGrid dataGrid, HttpContext httpContext, string headerText)
			: base(dataGrid, httpContext)
		{
			HeaderFooter tempHeader = new HeaderFooter(new Phrase(headerText, HeaderFont), false);
			tempHeader.Alignment = Rectangle.ALIGN_CENTER;
			tempHeader.Border = Rectangle.NO_BORDER;

			header = tempHeader;
		}

		/// <summary>
		/// Creates an exporter that will export to PDF using the footer of "Page N" and a <see cref="HeaderFooter" />
		/// as specified in <paramname ref="header" />.
		/// </summary>
		public PdfDataGridExporter(DataGrid dataGrid, HttpContext httpContext, HeaderFooter header)
			: base(dataGrid, httpContext)
		{
			base.header = header;
		}

		protected override void DocWriterGetInstance(Document document, Stream stream)
		{
			PdfWriter.GetInstance(document, stream);
            
		}

		public override string ContentType
		{
			get { return "application/pdf"; }
		}

		protected override ITable CreateTable(int columns)
		{
			return new TextSharpPdfPTable(this, columns);
		}

		#region TextSharpPdfPTable
		public class TextSharpPdfPTable : DefaultTable
		{
			private PdfPTable pdfPTable;
			
			internal TextSharpPdfPTable(PdfDataGridExporter pdfDataGridExporter, int columns)
			{
				int firstColumn = pdfDataGridExporter.FirstVisibleColumnWithHeaderText();
				int lastColumn = pdfDataGridExporter.LastVisibleColumnWithHeaderText();

				int remainingWidth = 100;
				int noWidthCells = 0;
				ArrayList widths = new ArrayList();
				for (int i = firstColumn; i <= lastColumn; i++)
				{
					if (pdfDataGridExporter.VisibleColumnWithHeaderText(i))
					{
						Unit width = pdfDataGridExporter.DataGrid.Columns[i].ItemStyle.Width;

						if (width.IsEmpty == false && width.Type == UnitType.Percentage)
						{
							remainingWidth -= (int)width.Value;
							widths.Add((float)width.Value);
						}
						else
						{
							noWidthCells++;
							widths.Add(0f);
						}
					}
				}

				pdfPTable = new PdfPTable(columns);

				if (widths.Count > 0)
				{
					int leftoverWidthForNoWidthCell = remainingWidth / noWidthCells;
					for (int i=0;i<widths.Count;i++)
					{
						if ((float)widths[i] == 0)
						{
							widths[i] = (float)leftoverWidthForNoWidthCell;
						}
					}

					pdfPTable.SetWidthPercentage((float[])widths.ToArray(typeof(float)), PageSize.LETTER);
				}

				if (pdfDataGridExporter.DataGrid.ShowHeader)
				{
					pdfPTable.HeaderRows = 1;
				}

				pdfPTable.WidthPercentage = 100;
                
               
			}

			public override ICell CreateCell()
			{
				return new TextSharpPdfPCell(CellPadding, CellBorder, CellBorderWidth);
			}

			public override void AddCell(ICell cell)
			{
				pdfPTable.AddCell((PdfPCell)cell.Cell);
			}

			public override float WidthPercentage
			{
				get { return pdfPTable.WidthPercentage; }
				set { pdfPTable.WidthPercentage = value; }
			}

			public override int RowCount
			{
				get { return pdfPTable.Rows.Count; }
			}

			public override float CellPadding
			{
				get { return cellPadding; }
				set { cellPadding = value; }
			}

			public override int CellBorder
			{
				get { return cellBorder; }
				set { cellBorder = value; }
			}

			public override float CellBorderWidth
			{
				get { return cellBorderWidth; }
				set { cellBorderWidth = value; }
			}

			public override IElement Table
			{
				get { return pdfPTable; }
			}

			#region TextSharpPdfPCell
			public class TextSharpPdfPCell : ICell
			{
				private PdfPCell pdfPCell;

				internal TextSharpPdfPCell(float padding, int border, float borderWidth)
				{
					pdfPCell = new PdfPCell();
					pdfPCell.Padding = padding;
					pdfPCell.Border = border;
					pdfPCell.BorderWidth = borderWidth;
				}

				public void AddElement(IElement element)
				{
					pdfPCell.AddElement(element);
				}

				public float GrayFill
				{
					get { return pdfPCell.GrayFill; }
					set { pdfPCell.GrayFill = value; }
				}

				public int HorizontalAlignment
				{
					get { return pdfPCell.HorizontalAlignment; }
					set { pdfPCell.HorizontalAlignment = value; }
				}

				public int VerticalAlignment
				{
					get { return pdfPCell.VerticalAlignment; }
					set { pdfPCell.VerticalAlignment = value; }
				}

				public IElement Cell
				{
					get { return this.pdfPCell; }
				}
			}
			#endregion
		}
		#endregion
	}
}
