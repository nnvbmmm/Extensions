// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace ConsoleApplication
{
    public class EnvironmentSecretManager : DefaultKeyVaultSecretManager
    {
        private readonly string _environmentPrefix;

        public EnvironmentSecretManager(string environment)
        {
            _environmentPrefix = environment + "-";
        }

        public override bool Load(SecretItem secret)
        {
            return HasEnvironmentPrefix(secret.Identifier.Name);
        }

        public override string GetKey(SecretBundle secret)
        {
            var keyName = base.GetKey(secret);

            return HasEnvironmentPrefix(keyName)
                ? keyName.Substring(_environmentPrefix.Length)
                : keyName;
        }

        private bool HasEnvironmentPrefix(string name)
        {
            return name.StartsWith(_environmentPrefix);
        }
    }
}
