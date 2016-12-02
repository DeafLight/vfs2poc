using System;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IModelObject
    {
        Guid? Id { get; set; }

        /// <summary>
        /// If true, this object is part of a functional module and cannot be deleted
        /// </summary>
        bool? IsFixed { get; set; }
    }
}
