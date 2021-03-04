
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

namespace Feature.MultiLanguage
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using global::Sitecore.Data;

    /// <summary>Controls the appearance of the inheriting template in site navigation.</summary>
    ///[RepresentsSitecoreTemplateAttribute("{9a38ad4c-ea37-4058-9693-c4be170981ba}", "", "Feature.MultiLanguage")]
    [SitecoreType(TemplateId = Constants.LanguageSelectorSettings.TemplateIdString)]
	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public partial interface ILanguageSelectorSettings : IGlassBase
    {
        
        /// <summary>Represents the Image file field (56c743eb-3fbe-421d-a963-1007e37b9f82).</summary>
        [SitecoreField(FieldName = Constants.LanguageSelectorSettings.Fields.ImageFile_FieldName)]
        Image ImageFile { get; set; }

    }

}

namespace Feature.MultiLanguage.Constants
{
    using global::Sitecore.Data;
	using System.CodeDom.Compiler;

	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public struct LanguageSelectorSettings
    {
        public const string TemplateIdString = "9a38ad4c-ea37-4058-9693-c4be170981ba";
        public static readonly ID TemplateId = new ID(TemplateIdString);

        
        public struct Fields
        {
        public static readonly ID ImageFile = new ID("56c743eb-3fbe-421d-a963-1007e37b9f82");
        public const string ImageFile_FieldName = "Image file";

        }
    }
}

