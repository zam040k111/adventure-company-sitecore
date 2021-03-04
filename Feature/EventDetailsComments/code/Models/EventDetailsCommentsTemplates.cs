
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

namespace Feature.EventDetailsComments
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using Glass.Mapper.Sc.Configuration.Attributes;
    using Glass.Mapper.Sc.Fields;
    using global::Sitecore.Data;

    /// <summary>Controls the appearance of the inheriting template in site navigation.</summary>
    ///[RepresentsSitecoreTemplateAttribute("{9051605c-8813-4cca-b906-240e2711b4db}", "", "Feature.EventDetailsComments")]
    [SitecoreType(TemplateId = Constants.Comment.TemplateIdString)]
	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public partial interface IComment : IGlassBase
    {
        
        /// <summary>Represents the Author name placeholder field (bc64b55c-f223-478f-a7de-1a0a72e86c78).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.AuthorNamePlaceholder_FieldName)]
        string AuthorNamePlaceholder { get; set; }

        /// <summary>Represents the Comment author name length field (05b3fac8-63b9-456c-9575-3a0a2e053c5d).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentAuthorNameLength_FieldName)]
        string CommentAuthorNameLength { get; set; }

        /// <summary>Represents the Comment author name required field (617ddccc-7639-4640-ba51-660a49cf28f7).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentAuthorNameRequired_FieldName)]
        string CommentAuthorNameRequired { get; set; }

        /// <summary>Represents the Comment button text field (eeda4234-ba3f-4a87-bb61-3a59ac92b8dd).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentButtonText_FieldName)]
        string CommentButtonText { get; set; }

        /// <summary>Represents the Comment section title field (b6747a61-e99d-4c10-b5ff-7ad2ca98b89b).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentSectionTitle_FieldName)]
        string CommentSectionTitle { get; set; }

        /// <summary>Represents the Comment text length field (d4c828d6-c146-4b03-a221-3e440f72662c).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentTextLength_FieldName)]
        string CommentTextLength { get; set; }

        /// <summary>Represents the Comment text placeholder field (14a4d04b-0f38-41bd-aacf-b37b6cce10dc).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentTextPlaceholder_FieldName)]
        string CommentTextPlaceholder { get; set; }

        /// <summary>Represents the Comment text required field (effaea63-e25a-43aa-8c64-b569860daac9).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.CommentTextRequired_FieldName)]
        string CommentTextRequired { get; set; }

        /// <summary>Represents the Displayed comment count field (6619ae22-5050-4ca8-a6de-39027f040c08).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.DisplayedCommentCount_FieldName)]
        int DisplayedCommentCount { get; set; }

        /// <summary>Represents the Successful adding comment message field (d89452dc-d0df-4555-abfd-594430da9a63).</summary>
        [SitecoreField(FieldName = Constants.Comment.Fields.SuccessfulAddingCommentMessage_FieldName)]
        string SuccessfulAddingCommentMessage { get; set; }

    }

}

namespace Feature.EventDetailsComments.Constants
{
    using global::Sitecore.Data;
	using System.CodeDom.Compiler;

	[GeneratedCode("Leprechaun", "1.0.0.0")]
    public struct Comment
    {
        public const string TemplateIdString = "9051605c-8813-4cca-b906-240e2711b4db";
        public static readonly ID TemplateId = new ID(TemplateIdString);

        
        public struct Fields
        {
        public static readonly ID AuthorNamePlaceholder = new ID("bc64b55c-f223-478f-a7de-1a0a72e86c78");
        public const string AuthorNamePlaceholder_FieldName = "Author name placeholder";

        public static readonly ID CommentAuthorNameLength = new ID("05b3fac8-63b9-456c-9575-3a0a2e053c5d");
        public const string CommentAuthorNameLength_FieldName = "Comment author name length";

        public static readonly ID CommentAuthorNameRequired = new ID("617ddccc-7639-4640-ba51-660a49cf28f7");
        public const string CommentAuthorNameRequired_FieldName = "Comment author name required";

        public static readonly ID CommentButtonText = new ID("eeda4234-ba3f-4a87-bb61-3a59ac92b8dd");
        public const string CommentButtonText_FieldName = "Comment button text";

        public static readonly ID CommentSectionTitle = new ID("b6747a61-e99d-4c10-b5ff-7ad2ca98b89b");
        public const string CommentSectionTitle_FieldName = "Comment section title";

        public static readonly ID CommentTextLength = new ID("d4c828d6-c146-4b03-a221-3e440f72662c");
        public const string CommentTextLength_FieldName = "Comment text length";

        public static readonly ID CommentTextPlaceholder = new ID("14a4d04b-0f38-41bd-aacf-b37b6cce10dc");
        public const string CommentTextPlaceholder_FieldName = "Comment text placeholder";

        public static readonly ID CommentTextRequired = new ID("effaea63-e25a-43aa-8c64-b569860daac9");
        public const string CommentTextRequired_FieldName = "Comment text required";

        public static readonly ID DisplayedCommentCount = new ID("6619ae22-5050-4ca8-a6de-39027f040c08");
        public const string DisplayedCommentCount_FieldName = "Displayed comment count";

        public static readonly ID SuccessfulAddingCommentMessage = new ID("d89452dc-d0df-4555-abfd-594430da9a63");
        public const string SuccessfulAddingCommentMessage_FieldName = "Successful adding comment message";

        }
    }
}

