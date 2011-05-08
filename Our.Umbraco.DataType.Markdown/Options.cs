using System;
using System.ComponentModel;

namespace Our.Umbraco.DataType.Markdown
{
	/// <summary>
	/// The options for the Markdown data-type.
	/// </summary>
	public class Options
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Options"/> class.
		/// </summary>
		public Options()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Options"/> class.
		/// </summary>
		/// <param name="loadDefaults">if set to <c>true</c> [load defaults].</param>
		public Options(bool loadDefaults)
		{
			if (loadDefaults)
			{
				this.EnableHistory = true;
				this.EnableWmd = true;
				this.Height = 400;
				this.HelpUrl = "http://daringfireball.net/projects/markdown/syntax";
				this.OutputFormat = OutputFormats.HTML;
				this.SelectedPreview = "toolbar";
				this.Width = 525;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [enable history].
		/// </summary>
		/// <value><c>true</c> if [enable history]; otherwise, <c>false</c>.</value>
		[DefaultValue(true)]
		public bool EnableHistory { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [enable WMD].
		/// </summary>
		/// <value><c>true</c> if [enable WMD]; otherwise, <c>false</c>.</value>
		[DefaultValue(true)]
		public bool EnableWmd { get; set; }

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		[DefaultValue(400)]
		public int Height { get; set; }

		/// <summary>
		/// Gets or sets the help URL.
		/// </summary>
		/// <value>The help URL.</value>
		[DefaultValue("http://daringfireball.net/projects/markdown/syntax")]
		public string HelpUrl { get; set; }

		/// <summary>
		/// The output formats for the data-type.
		/// </summary>
		public enum OutputFormats
		{
			/// <summary>
			/// Outputs as HTML.
			/// </summary>
			HTML = 0,

			/// <summary>
			/// Outputs as XML.
			/// </summary>
			XML = 1,

			/// <summary>
			/// Outputs as raw Markdown.
			/// </summary>
			Markdown = 2
		}

		/// <summary>
		/// Gets or sets the output format.
		/// </summary>
		/// <value>The output format.</value>
		[DefaultValue(OutputFormats.HTML)]
		public OutputFormats OutputFormat { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [save as XML].
		/// </summary>
		/// <value><c>true</c> if [save as XML]; otherwise, <c>false</c>.</value>
		[DefaultValue(false), Obsolete("The SaveAsXml property is no longer used, please use the OutputFormat property.")]
		public bool SaveAsXml { get; set; }

		/// <summary>
		/// Gets or sets the selected preview.
		/// </summary>
		/// <value>The selected preview.</value>
		[DefaultValue("toolbar")]
		public string SelectedPreview { get; set; }

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		[DefaultValue(525)]
		public int Width { get; set; }
	}
}
