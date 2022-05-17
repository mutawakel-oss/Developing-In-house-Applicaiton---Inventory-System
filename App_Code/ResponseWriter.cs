using System;
using System.Web;

namespace GeneralClass
{
	/// <author>Ron Grabowski</author>
	public class ResponseWriter
	{
		private ResponseWriter()
		{
			// empty
		}

		public static void Write(HttpResponse response, string fileName, AbstractDataGridExporter dataGridExporter)
		{
			response.Clear();
			response.ContentType = dataGridExporter.ContentType;
			response.AddHeader("Content-Disposition", String.Format("inline;filename={0}", fileName));
			dataGridExporter.Export(response.OutputStream);
			response.End();
		}
	}
}
