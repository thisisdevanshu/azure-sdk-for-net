// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.ResourceManager.ContainerService.Models
{
    /// <summary> The default is &apos;Ephemeral&apos; if the VM supports it and has a cache disk larger than the requested OSDiskSizeGB. Otherwise, defaults to &apos;Managed&apos;. May not be changed after creation. For more information see [Ephemeral OS](https://docs.microsoft.com/azure/aks/cluster-configuration#ephemeral-os). </summary>
    public readonly partial struct OSDiskType : IEquatable<OSDiskType>
    {
        private readonly string _value;

        /// <summary> Initializes a new instance of <see cref="OSDiskType"/>. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public OSDiskType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string ManagedValue = "Managed";
        private const string EphemeralValue = "Ephemeral";

        /// <summary> Azure replicates the operating system disk for a virtual machine to Azure storage to avoid data loss should the VM need to be relocated to another host. Since containers aren&apos;t designed to have local state persisted, this behavior offers limited value while providing some drawbacks, including slower node provisioning and higher read/write latency. </summary>
        public static OSDiskType Managed { get; } = new OSDiskType(ManagedValue);
        /// <summary> Ephemeral OS disks are stored only on the host machine, just like a temporary disk. This provides lower read/write latency, along with faster node scaling and cluster upgrades. </summary>
        public static OSDiskType Ephemeral { get; } = new OSDiskType(EphemeralValue);
        /// <summary> Determines if two <see cref="OSDiskType"/> values are the same. </summary>
        public static bool operator ==(OSDiskType left, OSDiskType right) => left.Equals(right);
        /// <summary> Determines if two <see cref="OSDiskType"/> values are not the same. </summary>
        public static bool operator !=(OSDiskType left, OSDiskType right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="OSDiskType"/>. </summary>
        public static implicit operator OSDiskType(string value) => new OSDiskType(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is OSDiskType other && Equals(other);
        /// <inheritdoc />
        public bool Equals(OSDiskType other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
