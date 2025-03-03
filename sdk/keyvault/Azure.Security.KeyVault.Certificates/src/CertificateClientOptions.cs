﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.Core;

namespace Azure.Security.KeyVault.Certificates
{
    /// <summary>
    /// Options that allow you to configure the requests sent to Key Vault.
    /// </summary>
    public class CertificateClientOptions : ClientOptions
    {
        /// <summary>
        /// The latest service version supported by this client library.
        /// For more information, see
        /// <see href="https://docs.microsoft.com/rest/api/keyvault/key-vault-versions">Key Vault versions</see>.
        /// </summary>
        internal const ServiceVersion LatestVersion = ServiceVersion.V7_3;

        /// <summary>
        /// The versions of Azure Key Vault supported by this client
        /// library.
        /// </summary>
        public enum ServiceVersion
        {
#pragma warning disable CA1707 // Identifiers should not contain underscores
            /// <summary>
            /// The Key Vault API version 7.0.
            /// </summary>
            V7_0 = 0,

            /// <summary>
            /// The Key Vault API version 7.1.
            /// </summary>
            V7_1 = 1,

            /// <summary>
            /// The Key Vault API version 7.2.
            /// </summary>
            V7_2 = 2,

            /// <summary>
            /// The Key Vault API version 7.3.
            /// </summary>
            V7_3 = 3,
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }

        /// <summary>
        /// Gets the <see cref="ServiceVersion"/> of the service API used when
        /// making requests. For more information, see
        /// <see href="https://docs.microsoft.com/rest/api/keyvault/key-vault-versions">Key Vault versions</see>.
        /// </summary>
        public ServiceVersion Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CertificateClientOptions"/>
        /// class.
        /// </summary>
        /// <param name="version">
        /// The <see cref="ServiceVersion"/> of the service API used when
        /// making requests.
        /// </param>
        public CertificateClientOptions(ServiceVersion version = LatestVersion)
        {
            Version = version;

            this.ConfigureLogging();
        }

        internal string GetVersionString()
        {
            return Version switch
            {
                ServiceVersion.V7_0 => "7.0",
                ServiceVersion.V7_1 => "7.1",
                ServiceVersion.V7_2 => "7.2",
                ServiceVersion.V7_3 => "7.3",
                _ => throw new ArgumentException(Version.ToString()),
            };
        }
    }
}
