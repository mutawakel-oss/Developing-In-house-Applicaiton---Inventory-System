using iTextSharp.text;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public interface ITable
	{
		ICell CreateCell();
		void AddCell(ICell cell);
		float WidthPercentage { get; set; }
		float CellPadding { set; get; }
		int CellBorder { get; set; }
		int RowCount { get; }
		float CellBorderWidth { get; set; }
		IElement Table { get; }
	}
}
