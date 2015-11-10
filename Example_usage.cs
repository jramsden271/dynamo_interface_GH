using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using JR.Extensions.Windows;

namespace SCA.test_interface
{
    public class Example_usage : GH_Component
    {

        /// <summary>
        /// Initializes a new instance of the WindMapInterpolator class.
        /// </summary>
        public Example_usage()
            : base("test4", "test4",
                "Interpolates one data mesh upon another",
                "SmartSpace", "SCA")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

        }


        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{46786d7d-01e1-4850-aff1-dd62263e3996}"); }
        }


        public override void CreateAttributes()
        {
            this.m_attributes = new test_interface.ImageFromPathAttrib(this);
        }


    }
}
