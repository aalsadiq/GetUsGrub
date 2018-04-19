using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CSULB.GetUsGrub.Models
{
    /// <summary>
    /// The <c>UniformResourceIdentifiers</c> class.
    /// Contains URI constants and list of grouped URIs.
    /// All URI constant values should be lower cased.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 04/17/2018
    /// </para>
    /// </summary>
    public class UniformResourceIdentifiers
    {
        // Absolute URI paths for Single Sign On
        public const string SSO_REGISTRATION = "/sso/registration";
        public const string SSO_LOGIN = "/sso/login";

        public readonly IEnumerable<string> UrisToSkipAuthn;

        public UniformResourceIdentifiers()
        {
            UrisToSkipAuthn = new Collection<string>()
            {
                SSO_REGISTRATION,
                SSO_LOGIN
            };
        }
    }
}
