using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public class HtmlDataGridExporter : ITextSharpDataGridExporter
	{
		public HtmlDataGridExporter(DataGrid dataGrid, HttpContext httpContext)
			: base(dataGrid, httpContext)
		{
			ItemFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
			HeaderFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
			FooterFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.ITALIC);
		}

		protected override void DocWriterGetInstance(Document document, Stream stream)
		{
			HtmlWriter.GetInstance(document, stream);
		}

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
			get { return "text/html"; }
		}

		protected override ITable CreateTable(int columns)
		{
			return new TextSharpSimpleTable(columns);
		}
	}
}
