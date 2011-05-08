using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;

using MarkdownSharp;
using Our.Umbraco.DataType.Markdown.Extensions;

namespace Our.Umbraco.DataType.Markdown
{
	public class XsltExtensions
	{
		public static string Transform(string input)
		{
			var markdown = new MarkdownSharp.Markdown();
			return markdown.Transform(input);
		}
		
		public static string HtmlToMarkdown(string input)
		{
			string output = string.Empty;

			if (!string.IsNullOrEmpty(input))
			{
				string html = string.Concat("<html>", input, "</html>");

				var xml = new XmlDocument();
				xml.LoadXml(html);

				var xslt = new XslCompiledTransform();
				xslt.Load(typeof(HtmlToMarkdown));

				using (var writer = new StringWriter())
				{
					xslt.Transform(xml, null, writer);
					return writer.ToString();
				}
			}

			return output;
		}
	}
}
