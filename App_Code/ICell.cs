using iTextSharp.text;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public interface ICell
	{
		void AddElement(IElement element);
		float GrayFill { get; set; }
		int HorizontalAlignment { get; set; }
		int VerticalAlignment { get; set; }
		IElement Cell { get; }
	}
}
