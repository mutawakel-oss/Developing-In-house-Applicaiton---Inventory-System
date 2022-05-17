using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text.rtf;

namespace GeneralClass
{
	/// <summary>
	/// Initializes ITextSharpDataGridExporter using the <see cref="RtfWriter" />.
	/// </summary>
	/// <author>Ron Grabowski</author>
	public class RtfDataGridExporter : ITextSharpDataGridExporter
	{
		public RtfDataGridExporter(DataGrid dataGrid, HttpContext httpContext)
			: base(dataGrid, httpContext)
		{
			// empty
		}

		protected override void DocWriterGetInstance(iTextSharp.text.Document document, Stream stream)
		{
			RtfWriter2.GetInstance(document, stream);
		}

		public override string ContentType
		{
			get { return "application/rtf"; }
		}

		protected override ITable CreateTable(int columns)
		{
			return new TextSharpSimpleTable(columns);
		}
	}
}