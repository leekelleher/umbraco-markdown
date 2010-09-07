using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using ClientDependency.Core.Controls;

using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;

namespace Our.Umbraco.DataType.Markdown
{
	public class DataEditor : AbstractDataEditor
	{
		/// <summary>
		/// The WMD/Markdown control.
		/// </summary>
		private WmdControl m_Control = new WmdControl();

		/// <summary>
		/// The Data object for the data-type.
		/// </summary>
		private IData m_Data;
		
		/// <summary>
		/// The PreValue Editor for the data-type.
		/// </summary>
		private PrevalueEditor m_PreValueEditor;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DataEditor"/> class.
		/// </summary>
		public DataEditor()
			: base()
		{
			// set the render control as the placeholder
			this.RenderControl = this.m_Control;

			// assign the initialise event for the placeholder
			this.m_Control.Init += new EventHandler(this.m_Control_Init);

			// assign the save event for the data-type/editor
			this.DataEditorControl.OnSave += new AbstractDataEditorControl.SaveEventHandler(this.DataEditorControl_OnSave);
		}

		/// <summary>
		/// Gets the id of the data-type.
		/// </summary>
		/// <value>The id of the data-type.</value>
		public override Guid Id
		{
			get
			{
				return new Guid("7E909384-2543-4AEB-ACEA-5E256F68C480");
			}
		}

		/// <summary>
		/// Gets the name of the data type.
		/// </summary>
		/// <value>The name of the data type.</value>
		public override string DataTypeName
		{
			get
			{
				return "Markdown Editor";
			}
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>The data.</value>
		public override IData Data
		{
			get
			{
				if (this.m_Data == null)
				{
					this.m_Data = new XmlData(this);
				}

				return this.m_Data;
			}
		}

		/// <summary>
		/// Gets the prevalue editor.
		/// </summary>
		/// <value>The prevalue editor.</value>
		public override IDataPrevalue PrevalueEditor
		{
			get
			{
				if (this.m_PreValueEditor == null)
				{
					this.m_PreValueEditor = new PrevalueEditor(this, DBTypes.Ntext);
				}

				return this.m_PreValueEditor;
			}
		}

		/// <summary>
		/// Handles the Init event of the m_Placeholder control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void m_Control_Init(object sender, EventArgs e)
		{
			// get the options from the Prevalue Editor.
			this.m_Control.Options = ((PrevalueEditor)this.PrevalueEditor).GetPreValueOptions<Options>();

			// set the value of the control
			if (this.Data.Value != null)
			{
				this.m_Control.Text = this.Data.Value.ToString();
			}
			else
			{
				this.m_Control.Text = String.Empty;
			}
		}

		/// <summary>
		/// Saves the editor control value.
		/// </summary>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void DataEditorControl_OnSave(EventArgs e)
		{
			// save the value of the control
			this.Data.Value = this.m_Control.Text;
		}
	}
}