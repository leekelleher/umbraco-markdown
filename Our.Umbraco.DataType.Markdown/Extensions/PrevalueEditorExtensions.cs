using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Our.Umbraco.DataType.Markdown.Extensions
{
	/// <summary>
	/// Extension methods for the Prevalue Editor
	/// </summary>
	public static class PrevalueEditorExtensions
	{
		/// <summary>
		/// Adds a new row to the Prevalue Editor.
		/// </summary>
		/// <param name="writer">The HtmlTextWriter.</param>
		/// <param name="label">The label for the field.</param>
		/// <param name="controls">The controls for the field.</param>
		public static void AddPrevalueRow(this HtmlTextWriter writer, string label, params Control[] controls)
		{
			writer.AddPrevalueRow(label, string.Empty, controls);
		}

		/// <summary>
		/// Adds a new row to the Prevalue Editor, (with an optional description).
		/// </summary>
		/// <param name="writer">The HtmlTextWriter.</param>
		/// <param name="label">The label for the field.</param>
		/// <param name="description">The description for the field.</param>
		/// <param name="controls">The controls for the field.</param>
		public static void AddPrevalueRow(this HtmlTextWriter writer, string label, string description, params Control[] controls)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "row clearfix");
			writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'row'

			writer.AddAttribute(HtmlTextWriterAttribute.Class, "label");
			writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'label'

			Label lbl = new Label() { Text = label }; // TODO: Is it possible to include 'AssociatedControlID'?
			lbl.RenderControl(writer);

			writer.RenderEndTag(); // end 'label'

			writer.AddAttribute(HtmlTextWriterAttribute.Class, "field");
			writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'field'

			foreach (Control control in controls)
			{
				control.RenderControl(writer);

			}

			writer.RenderEndTag(); // end 'field'

			if (!string.IsNullOrEmpty(description))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "description");
				writer.RenderBeginTag(HtmlTextWriterTag.Div); // start 'description'

				Label desc = new Label() { Text = description };
				desc.RenderControl(writer);

				writer.RenderEndTag(); // end 'description'
			}

			writer.RenderEndTag(); // end 'row'
		}
	}
}
