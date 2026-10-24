using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;

namespace WebApp.Extensions
{
    public static class EncryptionConfigurationExt
    {
        public static IConfigurationBuilder AddEncryptedJson(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("File path must be a non-empty string.");
            }

            var source = new EncryptionConfigurationSource
            {
                FileProvider = null,
                Path = path,
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };

            source.ResolveFileProvider();
            builder.Add(source);
            return builder;
        }
    }



    public class EncryptionConfigurationSource : JsonConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            //return base.Build(builder);
            EnsureDefaults(builder);
            return new EncryptionConfigurationProvider(this);
        }
    }

    public class EncryptionConfigurationProvider : JsonConfigurationProvider
    {
        public EncryptionConfigurationProvider(EncryptionConfigurationSource source) : base(source) { }

        public override void Load()
        {
            base.Load();
            //== Do decryption work here
            var aes = new MacroInteraction.Security.AES();
            Data["Jwt:Key"] = aes.Decrypt(Data["Jwt:Key"], MacroInteraction.Security.AES.Strength.Good);
        }
    }
}
