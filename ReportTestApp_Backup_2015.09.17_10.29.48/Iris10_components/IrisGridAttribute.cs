using System;

namespace IrisWeb.Code.Data.Attributes
{
    class IrisGridAttribute : Attribute
    {
        public string ReadPath { get; set; }
        public string CreatePath { get; set; }
        public string UpdatePath { get; set; }
        public string DestroyPath { get; set; }

        /// <summary>
        /// Configure the data parameters for this model when using it to provide data for a grid
        /// control for the user interface.
        /// </summary>
        /// <param name="readPath">The controller/action or URL for the read action</param>
        /// <param name="createPath">The controller/action or URL for the create action</param>
        /// <param name="updatePath">The controller/action or URL for the update action</param>
        /// <param name="destroyPath">The controller/action or URL for the delete action</param>
        public IrisGridAttribute(string readPath, string createPath, string updatePath, string destroyPath)
        {
            ReadPath = readPath;
            CreatePath = createPath;
            UpdatePath = updatePath;
            DestroyPath = destroyPath;
        }
    }
}
