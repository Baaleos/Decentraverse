﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Decentraverse {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Decentraverse.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 0x2e0352612a9e025d768a224510987ad3e012ff3b.
        /// </summary>
        internal static string ContractAddress {
            get {
                return ResourceManager.GetString("ContractAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [
        ///	{
        ///		&quot;constant&quot;: true,
        ///		&quot;inputs&quot;: [
        ///			{
        ///				&quot;name&quot;: &quot;interfaceId&quot;,
        ///				&quot;type&quot;: &quot;bytes4&quot;
        ///			}
        ///		],
        ///		&quot;name&quot;: &quot;supportsInterface&quot;,
        ///		&quot;outputs&quot;: [
        ///			{
        ///				&quot;name&quot;: &quot;&quot;,
        ///				&quot;type&quot;: &quot;bool&quot;
        ///			}
        ///		],
        ///		&quot;payable&quot;: false,
        ///		&quot;stateMutability&quot;: &quot;view&quot;,
        ///		&quot;type&quot;: &quot;function&quot;
        ///	},
        ///	{
        ///		&quot;constant&quot;: true,
        ///		&quot;inputs&quot;: [
        ///			{
        ///				&quot;name&quot;: &quot;tokenId&quot;,
        ///				&quot;type&quot;: &quot;uint256&quot;
        ///			}
        ///		],
        ///		&quot;name&quot;: &quot;getApproved&quot;,
        ///		&quot;outputs&quot;: [
        ///			{
        ///				&quot;name&quot;: &quot;&quot;,
        ///				&quot;type&quot;: &quot;address&quot;
        ///			}
        ///		],
        ///		&quot;payable&quot;: fa [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string DecentraverseABI {
            get {
                return ResourceManager.GetString("DecentraverseABI", resourceCulture);
            }
        }
    }
}
