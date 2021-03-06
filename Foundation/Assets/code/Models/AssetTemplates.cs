
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

namespace Foundation.Assets
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using global::Sitecore.Data;

    /// <summary>Controls the appearance of the inheriting template in site navigation.</summary>
    ///[RepresentsSitecoreTemplateAttribute("{d63c2f49-aa31-455f-b40f-51bf0826ec40}", "", "Foundation.Assets")]
    [SitecoreType(TemplateId = Constants.RenderingAssets.TemplateIdString)]
	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public partial interface IRenderingAssets : IGlassBase
    {
        
        /// <summary>Represents the InlineScript field (5b3ed234-c51a-46a1-93cb-a42f271f8cbc).</summary>
        [SitecoreField(FieldName = Constants.RenderingAssets.Fields.InlineScript_FieldName)]
        string InlineScript { get; set; }

        /// <summary>Represents the InlineStyling field (1f732ba4-4503-4b66-93b4-fb274e911b49).</summary>
        [SitecoreField(FieldName = Constants.RenderingAssets.Fields.InlineStyling_FieldName)]
        string InlineStyling { get; set; }

        /// <summary>Represents the ScriptFiles field (c90ff7c4-f5f6-4942-bbee-4a5fb259c14d).</summary>
        [SitecoreField(FieldName = Constants.RenderingAssets.Fields.ScriptFiles_FieldName)]
        string ScriptFiles { get; set; }

        /// <summary>Represents the StylingFiles field (2198dd13-010c-4fbe-aec5-54b6d5feb58c).</summary>
        [SitecoreField(FieldName = Constants.RenderingAssets.Fields.StylingFiles_FieldName)]
        string StylingFiles { get; set; }

        /// <summary>Represents the UseInlineScript field (7932b96b-9fde-4e3c-8724-dbf1ba5d2d21).</summary>
        [SitecoreField(FieldName = Constants.RenderingAssets.Fields.UseInlineScript_FieldName)]
        bool UseInlineScript { get; set; }

        /// <summary>Represents the UseInlineStyling field (2f0a5ae9-5033-468d-b61a-c9936baacd9d).</summary>
        [SitecoreField(FieldName = Constants.RenderingAssets.Fields.UseInlineStyling_FieldName)]
        bool UseInlineStyling { get; set; }

    }

}

namespace Foundation.Assets.Constants
{
    using global::Sitecore.Data;
	using System.CodeDom.Compiler;

	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public struct RenderingAssets
    {
        public const string TemplateIdString = "d63c2f49-aa31-455f-b40f-51bf0826ec40";
        public static readonly ID TemplateId = new ID(TemplateIdString);

        
        public struct Fields
        {
        public static readonly ID InlineScript = new ID("5b3ed234-c51a-46a1-93cb-a42f271f8cbc");
        public const string InlineScript_FieldName = "InlineScript";

        public static readonly ID InlineStyling = new ID("1f732ba4-4503-4b66-93b4-fb274e911b49");
        public const string InlineStyling_FieldName = "InlineStyling";

        public static readonly ID ScriptFiles = new ID("c90ff7c4-f5f6-4942-bbee-4a5fb259c14d");
        public const string ScriptFiles_FieldName = "ScriptFiles";

        public static readonly ID StylingFiles = new ID("2198dd13-010c-4fbe-aec5-54b6d5feb58c");
        public const string StylingFiles_FieldName = "StylingFiles";

        public static readonly ID UseInlineScript = new ID("7932b96b-9fde-4e3c-8724-dbf1ba5d2d21");
        public const string UseInlineScript_FieldName = "UseInlineScript";

        public static readonly ID UseInlineStyling = new ID("2f0a5ae9-5033-468d-b61a-c9936baacd9d");
        public const string UseInlineStyling_FieldName = "UseInlineStyling";

        }
    }
}

