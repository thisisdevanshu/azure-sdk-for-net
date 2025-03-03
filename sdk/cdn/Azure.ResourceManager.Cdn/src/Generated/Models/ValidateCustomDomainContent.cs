// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.Cdn.Models
{
    /// <summary> Input of the custom domain to be validated for DNS mapping. </summary>
    public partial class ValidateCustomDomainContent
    {
        /// <summary> Initializes a new instance of ValidateCustomDomainContent. </summary>
        /// <param name="hostName"> The host name of the custom domain. Must be a domain name. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="hostName"/> is null. </exception>
        public ValidateCustomDomainContent(string hostName)
        {
            if (hostName == null)
            {
                throw new ArgumentNullException(nameof(hostName));
            }

            HostName = hostName;
        }

        /// <summary> The host name of the custom domain. Must be a domain name. </summary>
        public string HostName { get; }
    }
}
