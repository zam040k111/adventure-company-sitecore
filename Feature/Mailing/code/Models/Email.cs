using System;
using System.Collections.Generic;
using Foundation.EmailProvider;
using Sitecore.Data;
using Sitecore.Globalization;

namespace Adventure.Feature.Mailing.Models
{
	public class Email : IEmail
	{
		public Guid Id { get; set; }
		
		public IEnumerable<Guid> BaseTemplateIds { get; set; }
		
		public string ContentPath { get; set; }
		
		public string DisplayName { get; set; }
		
		public string FullPath { get; set; }
		
		public ItemUri ItemUri { get; set; }
		
		public string Key { get; set; }
		
		public Language Language { get; set; }
		
		public string MediaUrl { get; set; }
		
		public string Name { get; set; }
		
		public Language OriginalLanguage { get; set; }
		
		public Guid OriginatorId { get; set; }
		
		public string Path { get; set; }
		
		public Guid TemplateId { get; set; }
		
		public string TemplateName { get; set; }
		
		public string Url { get; set; }
		
		public int Version { get; set; }
		
		public string Body { get; set; }
		
		public string Header { get; set; }
		
		public string Styles { get; set; }
		
		public string Subject { get; set; }
	}
}