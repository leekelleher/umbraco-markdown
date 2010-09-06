﻿using System;
using System.Web.UI;

using ClientDependency.Core.Controls;
using ClientDependency.Core;

namespace Our.Umbraco.DataType.Markdown.Extensions
{
	/// <summary>
	/// Extension methods for embedded resources
	/// </summary>
	public static class ResourceExtensions
	{
		/// <summary>
		/// Adds an embedded resource to the ClientDependency output by name
		/// </summary>
		/// <param name="ctl">The control.</param>
		/// <param name="resourceName">Name of the resource.</param>
		/// <param name="type">The type.</param>
		public static void AddResourceToClientDependency(this Control ctl, string resourceName, ClientDependencyType type)
		{
			//get the urls for the embedded resources           
			var resourceUrl = ctl.Page.ClientScript.GetWebResourceUrl(ctl.GetType(), resourceName);

			//This only works in v4 currently or until i release CD version 1.2, so in the meantime, we'll use the below method
			//add the resources to client dependency
			//ClientDependencyLoader.Instance.RegisterDependency(resourceUrl, type);

			switch (type)
			{
				case ClientDependencyType.Css:
					ctl.Page.Header.Controls.Add(new LiteralControl("<link type='text/css' rel='stylesheet' href='" + resourceUrl + "'/>"));
					break;

				case ClientDependencyType.Javascript:
					ctl.Page.ClientScript.RegisterClientScriptResource(typeof(ResourceExtensions), resourceName);
					break;

				default:
					break;
			}
		}
	}
}
