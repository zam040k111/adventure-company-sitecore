using Adventure.Foundation.Common.ValidationServices;

namespace Adventure.Foundation.Assets.Models
{
    public class Asset
    {
        public Asset(AssetType type, string content)
        {
            Throw.IfNullOrEmptyString(content, nameof(content));

            Type = type;
            Content = content;
        }

        public string Content { get; }

        public AssetType Type { get; set; }
    }
}