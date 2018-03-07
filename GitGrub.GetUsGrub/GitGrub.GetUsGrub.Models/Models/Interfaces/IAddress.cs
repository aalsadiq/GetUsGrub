using System.ComponentModel.DataAnnotations;

namespace GitGrub.GetUsGrub.Models
{
    /// <summary>
    /// The IAddress interface.
    /// A contract with autoproperty for Address class.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public interface IAddress
    {
        [Required]
        Address Address { get; set; }
    }
}
