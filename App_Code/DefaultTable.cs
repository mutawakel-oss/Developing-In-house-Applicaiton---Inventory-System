using iTextSharp.text;

namespace GeneralClass
{
	/// <summary>
	/// Provides the following default values as protected members:
	/// <list>
	///		<item><term><see cref="WidthPercentage" /></term><description>100</description></item>
	///		<item><term><see cref="CellPadding" /></term><description>4</description></item>
	///		<item><term><see cref="CellBorder" /></term><description>Rectangle.BOX</description></item>
	///		<item><term><see cref="CellBorderWidth" /></term><description>0.5f</description></item>
	/// </list>
	/// Concrete classes must implement all memebers of <see cref="ITable" />.
	/// </summary>
	/// <author>Ron Grabowski</author>
	public abstract class DefaultTable : ITable
	{
		protected float widthPercentage = 100;
		protected float cellPadding = 4;
		protected int cellBorder = Rectangle.BOX;
		protected float cellBorderWidth = 0.5f;

		public abstract ICell CreateCell();
		public abstract void AddCell(ICell cell);
		public abstract float WidthPercentage { get; set; }
		public abstract float CellPadding { get; set; }
		public abstract int CellBorder { get; set; }
		public abstract int RowCount { get; }
		public abstract float CellBorderWidth { get; set; }
		public abstract IElement Table { get; }
	}
}
