using Adventure.Foundation.Common.ValidationServices;

namespace Adventure.Foundation.Assets.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    public class AssetRepository
    {
        private static AssetRepository _current;
        private readonly List<Asset> _items = new List<Asset>();
        public static AssetRepository Current => _current ?? (_current = new AssetRepository());
        internal IEnumerable<Asset> Items => _items;

        internal void Clear()
        {
            _items.Clear();
        }

        public Asset Add(Asset asset)
        {
            Throw.IfNull(asset, nameof(asset));

            if (asset.Content != null)
            {
                if (_items.Any(x => x.Content != null && x.Content == asset.Content))
                {
                    return null;
                }
            }

            _items.Add(asset);

            return asset;
        }

        public Asset AddScriptFile(string file)
        {
            Throw.IfNullOrEmptyString(file, nameof(file));

            return AddAsset(file, AssetType.JavaScript);
        }

        private Asset AddAsset(string content, AssetType assetType)
        {
            var asset = CreateAsset(content, assetType);
            
            return asset == null ? null : Add(asset);
        }

        private Asset CreateAsset(string content, AssetType assetType)
        {
            var cleanContent = CleanContent(content);
            if (cleanContent == null)
            {
	            return null;
            }

            var asset = new Asset(assetType, cleanContent);
            
            return asset;
        }

        private string CleanContent(string content)
        {
            content = content?.Trim();
            
            return string.IsNullOrWhiteSpace(content) ? null : content;
        }

        public Asset AddStylingFile(string file)
        {
	        Throw.IfNullOrEmptyString(file, nameof(file));

            return AddAsset(file, AssetType.Css);
        }
    }
}