using iTextSharp.text;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public class TextSharpSimpleTable : DefaultTable
	{
		private SimpleTable simpleTable;
		private int columns;
		private int currentColumn = 1;
		private int rowCount = 1;
		private SimpleCell currentRow = new SimpleCell(SimpleCell.ROW);

		public TextSharpSimpleTable(int columns)
		{
			this.columns = columns;

			simpleTable = new SimpleTable();
			simpleTable.Cellpadding = 4;
			simpleTable.Widthpercentage = 100;
		}

		public override ICell CreateCell()
		{
			return new TextSharpSimpleCell(CellPadding, CellBorder, CellBorderWidth);
		}

		public override void AddCell(ICell cell)
		{
			currentColumn++;
			currentRow.AddElement(cell.Cell);

			if (currentColumn > columns)
			{
				this.rowCount++;
				currentColumn = 2;
				this.simpleTable.AddElement(currentRow);
				currentRow = new SimpleCell(SimpleCell.ROW);
			}
		}

		public override float WidthPercentage
		{
			get { return this.simpleTable.Widthpercentage; }
			set { this.simpleTable.Widthpercentage = value; }
		}

		public override float CellPadding
		{
			get { return this.simpleTable.Cellpadding; }
			set { this.simpleTable.Cellpadding = value; }
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

		public override int RowCount
		{
			get { return this.rowCount; }
		}

		public override IElement Table
		{
			get 
			{ 
				return this.simpleTable;
			}
		}

		#region TextSharpSimpleCell
		public class TextSharpSimpleCell : ICell
		{
			private SimpleCell simpleCell;

			internal TextSharpSimpleCell(float padding, int border, float borderWidth)
			{
				simpleCell = new SimpleCell(SimpleCell.CELL);
				simpleCell.Padding = padding;
				simpleCell.Border = border;
				simpleCell.BorderWidth = borderWidth;
			}

			public void AddElement(IElement element)
			{
				simpleCell.AddElement(element);
			}

			public float GrayFill
			{
				get { return simpleCell.GrayFill; }
				set { simpleCell.GrayFill = value; }
			}

			public int HorizontalAlignment
			{
				get { return simpleCell.HorizontalAlignment; }
				set { simpleCell.HorizontalAlignment = value; }
			}

			public int VerticalAlignment
			{
				get { return simpleCell.VerticalAlignment; }
				set { simpleCell.VerticalAlignment = value; }
			}

			public IElement Cell
			{
				get { return simpleCell; }
			}
		}
		#endregion
	}
}
