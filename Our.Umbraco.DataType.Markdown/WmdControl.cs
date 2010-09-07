using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using ClientDependency.Core;
using Our.Umbraco.DataType.Markdown.Extensions;

[assembly: WebResource("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.showdown.js", "application/x-javascript")]
[assembly: WebResource("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.jquery.markedit.js", "application/x-javascript")]
[assembly: WebResource("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.jquery.markedit.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.images.wmd-buttons.png", "image/png")]

namespace Our.Umbraco.DataType.Markdown
{
	public class WmdControl : WebControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WmdControl"/> class.
		/// </summary>
		public WmdControl()
		{
		}

		/// <summary>
		/// Gets or sets the options.
		/// </summary>
		/// <value>The options.</value>
		public Options Options { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text for the TextBoxControl.</value>
		public string Text
		{
			get
			{
				return this.TextBoxControl.Text;
			}

			set
			{
				if (this.TextBoxControl == null)
				{
					this.TextBoxControl = new TextBox();
				}

				this.TextBoxControl.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the TextBox control.
		/// </summary>
		/// <value>The TextBox control.</value>
		protected TextBox TextBoxControl { get; set; }

		/// <summary>
		/// Initialize the control, make sure children are created
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			this.EnsureChildControls();
		}

		/// <summary>
		/// Add the resources (sytles/scripts)
		/// </summary>
		/// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			// check if WMD Editor has been enabled.
			if (this.Options.EnableWmd)
			{
				// adds the client dependencies.
				this.AddResourceToClientDependency("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.jquery.markedit.css", ClientDependencyType.Css);
				this.AddResourceToClientDependency("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.jquery.markedit.js", ClientDependencyType.Javascript);
				this.AddResourceToClientDependency("Our.Umbraco.DataType.Markdown.Resources.MarkEdit.showdown.js", ClientDependencyType.Javascript);
			}
		}

		/// <summary>
		/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
		/// </summary>
		protected override void CreateChildControls()
		{
			base.CreateChildControls();

			this.EnsureChildControls();

			// create the controls
			this.TextBoxControl.ID = this.TextBoxControl.ClientID;
			this.TextBoxControl.TextMode = TextBoxMode.MultiLine;
			this.TextBoxControl.Height = Unit.Pixel(this.Options.Height);
			this.TextBoxControl.Width = Unit.Pixel(this.Options.Width);
			this.TextBoxControl.CssClass = "umbEditorTextFieldMultiple";

			// add the controls
			this.Controls.Add(this.TextBoxControl);
		}

		/// <summary>
		/// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
		/// </summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
		protected override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "MarkdownTextBox");
			writer.AddAttribute(HtmlTextWriterAttribute.Style, string.Concat("width: ", this.Options.Width + 6, "px;"));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);

			this.TextBoxControl.RenderControl(writer);

			writer.RenderEndTag(); // .MarkdownTextBox

			// check if WMD Editor has been enabled.
			if (this.Options.EnableWmd)
			{
				const char QUOTE = '\'';
				////const char COMMA = ',';

				// set the MarkEdit options
				var markeditOptions = new StringBuilder("{");

				// history option
				if (!this.Options.EnableHistory)
				{
					markeditOptions.Append("'history': false, ");
				}

				// preview option
				markeditOptions.Append("'preview': ");
				if (!string.IsNullOrEmpty(this.Options.SelectedPreview))
				{
					markeditOptions
						.Append(QUOTE)
						.Append(this.Options.SelectedPreview)
						.Append(QUOTE);
				}
				else
				{
					markeditOptions.Append("false");
				}

				markeditOptions
					.Append(", 'toolbar': { ")
					.Append("'layout': 'bold italic | link quote code image | numberlist bulletlist heading line");

				// reference the help button
				if (!string.IsNullOrEmpty(this.Options.HelpUrl))
				{
					markeditOptions.Append(" | help");	
				}

				markeditOptions
					.Append("', ")
					.Append("'buttons' : [ ");

				// add the help button
				if (!string.IsNullOrEmpty(this.Options.HelpUrl))
				{
					markeditOptions
						.Append("{ 'id' : 'help', 'css' : 'help', 'tip' : 'Markdown Syntax Help', 'click' : function(){ window.open('")
						.Append(this.Options.HelpUrl)
						.Append("'); }, 'mouseover' : function(){}, 'mouseout' : function(){} }");
				}
				
				markeditOptions
					.Append(" ] }")
					.Append("}");

				// add jquery window load event
				var javascriptMethod = string.Format("jQuery('#{0}').markedit( {1} );", this.TextBoxControl.ClientID, markeditOptions);
				var javascript = string.Concat("<script type='text/javascript'>jQuery(window).load(function(){", javascriptMethod, "});</script>");
				writer.WriteLine(javascript);
			}
		}
	}
}
