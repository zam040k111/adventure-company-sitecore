using System;
using System.Collections.Generic;
using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;
using Sitecore.Globalization;

namespace Adventure.Foundation.ORM.Models
{
	/// <summary>
	/// Base interface for sitecore classes
	/// </summary>
	public interface IGlassBase
	{
		/// <summary>
		/// The item's Id.
		/// </summary>
		[SitecoreId]
		Guid Id { get; set; }

		/// <summary>
		/// Gets the Base Template Ids.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.BaseTemplateIds)]
		IEnumerable<Guid> BaseTemplateIds { get; set; }

		/// <summary>
		/// The item's content path.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.ContentPath)]
		string ContentPath { get; set; }

		/// <summary>
		/// The item's display name.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.DisplayName)]
		string DisplayName { get; set; }

		/// <summary>
		/// The item's full path.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.FullPath)]
		string FullPath { get; set; }

		/// <summary>
		/// The item's ItemUri.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.ItemUri)]
		ItemUri ItemUri { get; set; }

		/// <summary>
		/// The item's key.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.Key)]
		string Key { get; set; }

		/// <summary>
		/// The items language.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.Language)]
		Language Language { get; set; }

		/// <summary>
		/// The item's media URL.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.MediaUrl)]
		string MediaUrl { get; set; }

		/// <summary>
		/// The item's Name.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.Name)]
		string Name { set; get; }

		/// <summary>
		/// Gets the original language.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.OriginalLanguage)]
		Language OriginalLanguage { get; set; }

		/// <summary>
		/// Get the originator Id.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.OriginatorId)]
		Guid OriginatorId { get; set; }

		/// <summary>
		/// The item's path.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.Path)]
		string Path { get; set; }

		/// <summary>
		/// The item's template Id.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.TemplateId)]
		Guid TemplateId { get; set; }

		/// <summary>
		/// The item's template name.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.TemplateName)]
		string TemplateName { get; set; }

		/// <summary>
		/// The item's URL.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.Url)]
		string Url { get; set; }

		/// <summary>
		/// The item's version.
		/// </summary>
		[SitecoreInfo(SitecoreInfoType.Version)]
		int Version { get; set; }
	}
}