using System;

namespace vfs2poc.Configuration.Interfaces
{
    /// <summary>
    /// Global main class.
    /// Ensures that every object has an Id and can be set to fixed if part of a functional module.
    /// </summary>
    public interface IModelObject
    {
        /// <summary>
        /// The object Id
        /// </summary>
        Guid? Id { get; set; }

        /// <summary>
        /// If true, this object is part of a functional module and cannot be deleted
        /// </summary>
        bool? IsFixed { get; set; }
    }
}
