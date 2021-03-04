
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// ReSharper disable All

using Adventure.Foundation.ORM.Models;

namespace Feature.EventSearchComponent
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using global::Sitecore.Data;

    /// <summary>Controls the appearance of the inheriting template in site navigation.</summary>
    ///[RepresentsSitecoreTemplateAttribute("{b95b386b-882e-4e49-85aa-d33dcc76ac32}", "", "Feature.EventSearchComponent")]
    [SitecoreType(TemplateId = Constants.EventSearchSettings.TemplateIdString)]
	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public partial interface IEventSearchSettings : IGlassBase
    {
        
        /// <summary>Represents the PageSizeOptions field (15121c03-4a0a-4d65-b239-2590b726a094).</summary>
        [SitecoreField(FieldName = Constants.EventSearchSettings.Fields.PageSizeOptions_FieldName)]
        IEnumerable<IPageSizeOption> PageSizeOptions { get; set; }

    }

}

namespace Feature.EventSearchComponent.Constants
{
    using global::Sitecore.Data;
	using System.CodeDom.Compiler;

	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public struct EventSearchSettings
    {
        public const string TemplateIdString = "b95b386b-882e-4e49-85aa-d33dcc76ac32";
        public static readonly ID TemplateId = new ID(TemplateIdString);

        
        public struct Fields
        {
        public static readonly ID PageSizeOptions = new ID("15121c03-4a0a-4d65-b239-2590b726a094");
        public const string PageSizeOptions_FieldName = "PageSizeOptions";

        }
    }
}
namespace Feature.EventSearchComponent
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using global::Sitecore.Data;

    /// <summary>Controls the appearance of the inheriting template in site navigation.</summary>
    ///[RepresentsSitecoreTemplateAttribute("{25fa188e-e0cc-48d0-8458-92c245c3a86d}", "", "Feature.EventSearchComponent")]
    [SitecoreType(TemplateId = Constants.PageSizeOption.TemplateIdString)]
	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public partial interface IPageSizeOption : IGlassBase
    {
        
        /// <summary>Represents the Value field (844f7a35-22a3-4589-9435-9caab0140dbb).</summary>
        [SitecoreField(FieldName = Constants.PageSizeOption.Fields.Value_FieldName)]
        int Value { get; set; }

    }

}

namespace Feature.EventSearchComponent.Constants
{
    using global::Sitecore.Data;
	using System.CodeDom.Compiler;

	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public struct PageSizeOption
    {
        public const string TemplateIdString = "25fa188e-e0cc-48d0-8458-92c245c3a86d";
        public static readonly ID TemplateId = new ID(TemplateIdString);

        
        public struct Fields
        {
        public static readonly ID Value = new ID("844f7a35-22a3-4589-9435-9caab0140dbb");
        public const string Value_FieldName = "Value";

        }
    }
}
