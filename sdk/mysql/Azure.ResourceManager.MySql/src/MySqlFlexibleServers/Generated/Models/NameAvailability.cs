// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.MySql.FlexibleServers.Models
{
    /// <summary> Represents a resource name availability. </summary>
    public partial class NameAvailability
    {
        /// <summary> Initializes a new instance of NameAvailability. </summary>
        internal NameAvailability()
        {
        }

        /// <summary> Initializes a new instance of NameAvailability. </summary>
        /// <param name="message"> Error Message. </param>
        /// <param name="nameAvailable"> Indicates whether the resource name is available. </param>
        /// <param name="reason"> Reason for name being unavailable. </param>
        internal NameAvailability(string message, bool? nameAvailable, string reason)
        {
            Message = message;
            NameAvailable = nameAvailable;
            Reason = reason;
        }

        /// <summary> Error Message. </summary>
        public string Message { get; }
        /// <summary> Indicates whether the resource name is available. </summary>
        public bool? NameAvailable { get; }
        /// <summary> Reason for name being unavailable. </summary>
        public string Reason { get; }
    }
}
