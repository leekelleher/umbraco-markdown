using System;
using System.Xml;

using MarkdownSharp;
using umbraco.cms.businesslogic.datatype;

namespace Our.Umbraco.DataType.Markdown
{
	/// <summary>
	/// Overrides the <see cref="umbraco.cms.businesslogic.datatype.DefaultData"/> object to return the value as HTML.
	/// </summary>
	public class TextData : DefaultData
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlData"/> class.
		/// </summary>
		/// <param name="dataType">Type of the data.</param>
		public TextData(BaseDataType dataType)
			: base(dataType)
		{
		}

		/// <summary>
		/// Transforms the Markdown value to HTML.
		/// </summary>
		/// <param name="data">The data (Markdown) to transform into HTML.</param>
		/// <returns>Returns an HTML representation of the data.</returns>
		public override XmlNode ToXMl(XmlDocument data)
		{
			// check that the value isn't null
			if (this.Value != null && !string.IsNullOrEmpty(this.Value.ToString()))
			{
				// transform the markdown into HTML.
				var markdown = new MarkdownSharp.Markdown();
				string output = markdown.Transform(this.Value.ToString());
				
				// return the transformed HTML (as CDATA)
				return data.CreateCDataSection(output);
			}
			else
			{
				// otherwise render the value as default (in CDATA)
				return base.ToXMl(data);
			}
		}
	}
}